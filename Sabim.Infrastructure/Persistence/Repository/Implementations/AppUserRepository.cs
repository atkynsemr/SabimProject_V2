using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.AppUserDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class AppUserRepository : RepositoryBase<AppUser>, IAppUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AppUserRepository(SabimDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IHttpContextAccessor httpContextAccessor, RoleManager<AppRole> roleManager) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<AppUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<AppUser> FindByUserIdAsync(int id)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
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
                return (false, $"Hesap Durumu: {user.Durum.DurumAdi.ToString()}. Hata olduğunu düşünüyorsanız Bilgi İşlem Birimi ile irtibat kurunuz.", null);
            }
            // Şifreyi doğrula
            //var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            //if (!isPasswordValid)
            //{
            //    return (false, SignInMessages.InvalidPassword,null);
            //}
            // Kullanıcıyı oturum açtır
            var signInResult = await _signInManager.PasswordSignInAsync(user, loginDto.Password, loginDto.RememberMe, lockoutOnFailure: false);
            if (!signInResult.Succeeded)
            {
                //return (false, SignInMessages.SignInFailed,null);
                return (false, SignInMessages.InvalidPassword, null);
            }
            // Kullanıcının rollerini al
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count == 0)
            {
                return (false, SignInMessages.RoleNotAssigned,null);
            }
            return (true, SignInMessages.Succesed,user);
        }
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<string> SavePasswordResetTokenAsync(AppUser user, string provider, string name)
        {
            var resetCode = new Random().Next(100000, 999999).ToString();
            await _userManager.SetAuthenticationTokenAsync(user, provider, name, resetCode);
            return resetCode;

        }
        public async Task<IdentityResult> ResetUserPasswordAsync(AppUser user, string token, string newPassword)
        {
            var resetPassword = await _userManager.ResetPasswordAsync(user,token,newPassword);
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword);
            var result = await _userManager.UpdateAsync(user);
            // Eğer şifre sıfırlama başarılıysa token'ı kaldır
            if (result.Succeeded)
            {
                await _userManager.RemoveAuthenticationTokenAsync(user, "PasswordReset", "ResetCode");
            }
            return result;
        }
    }
}
