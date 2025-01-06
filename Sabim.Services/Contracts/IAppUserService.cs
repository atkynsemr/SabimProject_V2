using Microsoft.AspNetCore.Identity;
using Sabim.Domain.DTOs.AppUserDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.Contracts
{
    public interface IAppUserService : IGenericService<AppUser>
    {
        Task<AppUser> TFindByEmailAsync(string email);
        Task<AppUser> TFindByUserIdAsync(int id);
        Task<bool> TCheckPasswordAsync(AppUser user, string password);
        Task<bool> TIsUserLockedOutAsync(AppUser user);
        Task<(bool Success, string Message, AppUser User)> TSignInAsync(LoginAppUserDto loginDto);
        Task<string> TSavePasswordResetTokenAsync(AppUser user, string provider, string name);
        Task<bool> TIsValidTokenAsync(int userId, string token);
        Task<IdentityResult> TResetUserPasswordAsync(AppUser user, string token, string newPassword);
        Task<bool> TRemoveResetTokenAsync(AppUser user);
        Task TSignOutAsync();
    }
}
