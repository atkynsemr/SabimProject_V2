using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace Sabim.Web.Filters
{
    public class ClaimBasedAuthorizationHandler : AuthorizationHandler<ClaimBasedAuthorizationAttribute>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimBasedAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimBasedAuthorizationAttribute requirement)
        {
            // HttpContext ve Session kontrolü
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext?.Session == null)
            {
                context.Fail(); // Session erişilemiyorsa yetkilendirme başarısız
                return Task.CompletedTask;
            }

            // Session'dan RoleClaims verilerini al
            var roleClaimsJson = httpContext.Session.GetString("RoleClaims");
            if (string.IsNullOrEmpty(roleClaimsJson))
            {
                context.Fail(); // RoleClaims yoksa yetkilendirme başarısız
                return Task.CompletedTask;
            }

            // Session'daki RoleClaims'i deserialize et
            var roleClaims = JsonConvert.DeserializeObject<List<dynamic>>(roleClaimsJson);
            if (roleClaims == null || !roleClaims.Any())
            {
                context.Fail(); // Deserialize edilemeyen veya boş RoleClaims için yetkilendirme başarısız
                return Task.CompletedTask;
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
            return Task.CompletedTask;
        }
    }
}
