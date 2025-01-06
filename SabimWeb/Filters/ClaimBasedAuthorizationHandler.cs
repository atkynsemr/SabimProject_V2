using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using NuGet.Packaging;
using Sabim.Domain.Entities;
using System.Security.Claims;

namespace Sabim.Web.Filters
{
    public class ClaimBasedAuthorizationHandler : AuthorizationHandler<ClaimBasedAuthorizationAttribute>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public ClaimBasedAuthorizationHandler(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _userManager = userManager;
            _roleManager = roleManager;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimBasedAuthorizationAttribute requirement)
        {
            ClaimsPrincipal claimUser = _httpContextAccessor.HttpContext.User;
            // HttpContext ve Session kontrolü
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext?.Session.Keys.Count() == 0)
            {
                if (claimUser == null)
                {
                    context.Fail(); // Session erişilemiyorsa yetkilendirme başarısız
                }
                else
                {
                    var userId = claimUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (!string.IsNullOrEmpty(userId))
                    {
                        // Kullanıcıyı al
                        var user = await _userManager.FindByIdAsync(userId);
                        if (user != null)
                        {
                            // Kullanıcının rollerini al
                            var roles = await _userManager.GetRolesAsync(user);
                            // Rolleri virgülle ayrılmış şekilde Session'a ekle
                            if (roles.Any())
                            {
                                httpContext.Session.SetString("UserRoles", string.Join(",", roles));
                            }
                            // RoleClaims'i al ve Session'a ekle
                            // Rol claim'lerini oluştur
                            var roleClaimss = new List<Claim>();
                            foreach (var role in roles)
                            {
                                var appRole = await _roleManager.FindByNameAsync(role);
                                if (appRole != null)
                                {
                                    var claimss = await _roleManager.GetClaimsAsync(appRole);
                                    roleClaimss.AddRange(claimss.Select(c => new Claim(c.Type, c.Value)));
                                    // Rolleri claim olarak ekle
                                    claimss.AddRange(roleClaimss);
                                    _httpContextAccessor.HttpContext.Session.SetString("RoleClaims", JsonConvert.SerializeObject(claimss));
                                }
                            }
                        }
                        else
                        {
                            context.Fail();
                        }
                    }
                }
            }
            // Session'dan RoleClaims verilerini al
            var roleClaimsJson = httpContext.Session.GetString("RoleClaims");
            if (string.IsNullOrEmpty(roleClaimsJson))
            {
                context.Fail(); // RoleClaims yoksa yetkilendirme başarısız
                return;
            }
            // Session'daki RoleClaims'i deserialize et
            var roleClaims = JsonConvert.DeserializeObject<List<dynamic>>(roleClaimsJson);
            if (roleClaims == null || !roleClaims.Any())
            {
                context.Fail(); // Deserialize edilemeyen veya boş RoleClaims için yetkilendirme başarısız
                return;
            }
            // Requirement ile eşleşen claim var mı kontrol et
            var claimExists = roleClaims.Any(c =>
                c.Type == requirement.ClaimType &&
                c.Value == requirement.ClaimValue);
            if (claimExists)
            {
                context.Succeed(requirement); // Yetkilendirme başarılı
            }
            else
            {
                context.Fail();
            }
            return;
        }
    }
}
