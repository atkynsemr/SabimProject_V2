using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Sabim.Domain.Entities;
using System.Security.Claims;

namespace Sabim.Web.Helpers.MethodHelper
{
    public class AuthenticationHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        // Constructor ile DI container'dan bağımlılıkları alıyoruz
        public AuthenticationHelper(IHttpContextAccessor httpContextAccessor,
                                    UserManager<AppUser> userManager,
                                    RoleManager<AppRole> roleManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Asenkron metod: Kullanıcıyı giriş yaptır
        public async Task SignInAsync(AppUser user, bool rememberMe)
        {
            // Kullanıcının rollerini al
            var roles = await _userManager.GetRolesAsync(user);

            // Rol claim'lerini oluştur
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                var appRole = await _roleManager.FindByNameAsync(role);
                if (appRole != null)
                {
                    var claimss = await _roleManager.GetClaimsAsync(appRole);
                    roleClaims.AddRange(claimss.Select(c => new Claim(c.Type, c.Value)));
                }
            }

            // Kullanıcı bilgileri ile claim'leri oluştur
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, roles.FirstOrDefault())
            };

            // Rolleri claim olarak ekle
            claims.AddRange(roleClaims);

            if (roleClaims.Any())
            {
                _httpContextAccessor.HttpContext.Session.SetString("RoleClaims", JsonConvert.SerializeObject(claims));
            }

            // ClaimsIdentity oluştur
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // AuthenticationProperties oluştur
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                ExpiresUtc = rememberMe ? DateTimeOffset.UtcNow.AddDays(2) : (DateTimeOffset?)null
            };

            // Identity.Application çerezi için giriş
            await _httpContextAccessor.HttpContext.SignInAsync(
                IdentityConstants.ApplicationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            // Kullanıcıyı oturum açtır
            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}