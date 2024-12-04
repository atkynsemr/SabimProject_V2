using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.AppUserDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using System.Security.Claims;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class AppUserRepository : RepositoryBase<AppUser>, IAppUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppUserRepository(SabimDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IHttpContextAccessor httpContextAccessor, RoleManager<AppRole> roleManager) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
        }
        public async Task<AppUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
        public async Task<bool> IsUserLockedOutAsync(AppUser user)
        {
            return await _userManager.IsLockedOutAsync(user);
        }
        public async Task<(bool Success, string Message, AppUser? User)> SignInAsync(LoginAppUserDto loginDto)
        {
            // UserName ile kullanıcıyı bul
            var user = await _userManager.Users.Include(u => u.Durum).FirstOrDefaultAsync(u => u.UserName == loginDto.UserName);
            if (user == null)
            {
                return (false, SignInMessages.UserNotFound,null);
            }
            if (user.DurumId != 1)
            {
                return (false, user.Durum.DurumAdi.ToString(),null);
            }
            // Şifreyi doğrula
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
            {
                return (false, SignInMessages.InvalidPassword,null);
            }
            // Kullanıcıyı oturum açtır
            var signInResult = await _signInManager.PasswordSignInAsync(user, loginDto.Password, loginDto.RememberMe, lockoutOnFailure: false);
            if (!signInResult.Succeeded)
            {
                return (false, SignInMessages.SignInFailed,null);
            }
            // Kullanıcının rollerini al
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count == 0)
            {
                return (false, SignInMessages.RoleNotAssigned,null);
            }
            // RoleManager üzerinden role claim'lerini al
            //var roleClaims = new List<Claim>();
            //foreach (var role in roles)
            //{
            //    var appRole = await _roleManager.FindByNameAsync(role);
            //    if (appRole != null)
            //    {
            //        var claimss = await _roleManager.GetClaimsAsync(appRole);
            //        roleClaims.AddRange(claimss.Select(c => new Claim(c.Type, c.Value)));
            //    }
            //}
            //// Kullanıcı bilgileri ile claim'leri oluştur
            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //    new Claim(ClaimTypes.Name, user.UserName),
            //    new Claim(ClaimTypes.Email, user.Email)
            //};
            //// Rolleri claim olarak ekle
            //claims.AddRange(roleClaims);
            //var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //// AuthProperties oluştur
            //var authProperties = new AuthenticationProperties
            //{
            //    IsPersistent = loginDto.RememberMe,
            //    ExpiresUtc = loginDto.RememberMe ? DateTimeOffset.UtcNow.AddDays(5) : DateTimeOffset.UtcNow.AddMinutes(5)
            //};
            return (true, SignInMessages.Succesed,user);
        }
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
       
    }
}
