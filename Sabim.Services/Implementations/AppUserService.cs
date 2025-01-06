using Microsoft.AspNetCore.Identity;
using Sabim.Domain.DTOs.AppUserDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class AppUserService : GenericService<AppUser>, IAppUserService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly UserManager<AppUser> _userManager;
        public AppUserService(IRepositoryBase<AppUser> repository, IRepositoryManager repositoryManager, UserManager<AppUser> userManager) : base(repository)
        {
            _repositoryManager = repositoryManager;
            _userManager = userManager;
        }
        // FindByEmailAsync methodu
        public async Task<AppUser> TFindByEmailAsync(string email)
        {
            return await _repositoryManager.AppUser.FindByEmailAsync(email);
        }
        // FindByUserIdAsync methodu
        public async Task<AppUser> TFindByUserIdAsync(int id)
        {
            return await _repositoryManager.AppUser.FindByUserIdAsync(id);
        }
        // CheckPasswordAsync methodu
        public async Task<bool> TCheckPasswordAsync(AppUser user, string password)
        {
            return await _repositoryManager.AppUser.CheckPasswordAsync(user, password);
        }
        // IsUserLockedOutAsync methodu
        public async Task<bool> TIsUserLockedOutAsync(AppUser user)
        {
            return await _repositoryManager.AppUser.IsUserLockedOutAsync(user);
        }
        // SignInAsync methodu
        public async Task<(bool Success, string Message, AppUser User)> TSignInAsync(LoginAppUserDto loginDto)
        {
            return await _repositoryManager.AppUser.SignInAsync(loginDto);
        }
        // SignOutAsync methodu
        public async Task TSignOutAsync()
        {
            await _repositoryManager.AppUser.SignOutAsync();
        }
        public async Task<string> TSavePasswordResetTokenAsync(AppUser user, string provider, string name)
        {
            var resetCode = await _repositoryManager.AppUser.SavePasswordResetTokenAsync(user, provider, name);
            return resetCode;
        }
        public async Task<bool> TIsValidTokenAsync(int userId, string token)
        {
            // Kullanıcıyı kullanıcı ID'sine göre bul
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return false; // Kullanıcı bulunamadıysa false döneriz
            }

            // AspNetUserTokens tablosundan token'ı al
            var storedToken = await _userManager.GetAuthenticationTokenAsync(user, "PasswordReset", "ResetCode");

            // Token'ı kontrol et
            if (storedToken != null && storedToken == token)
            {
                return true; // Token geçerli
            }
            return false; // Token geçersiz
        }
        // Şifre sıfırlama
        public async Task<IdentityResult> TResetUserPasswordAsync(AppUser user, string token, string newPassword)
        {
            return await _repositoryManager.AppUser.ResetUserPasswordAsync(user, token, newPassword);
        }
        // Token silme
        public async Task<bool> TRemoveResetTokenAsync(AppUser user)
        {
            var result = await _userManager.RemoveAuthenticationTokenAsync(user, "PasswordReset", "ResetCode");
            return result.Succeeded;
        }
    }
}
