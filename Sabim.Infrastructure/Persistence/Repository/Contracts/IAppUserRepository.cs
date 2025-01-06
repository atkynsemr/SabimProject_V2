using Microsoft.AspNetCore.Identity;
using Sabim.Domain.DTOs.AppUserDtos;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Repository.Contracts
{
    public interface IAppUserRepository : IRepositoryBase<AppUser>
    {
            Task<AppUser> FindByEmailAsync(string email);
            Task<AppUser> FindByUserIdAsync(int id);
            Task<bool> CheckPasswordAsync(AppUser user, string password);
            Task<bool> IsUserLockedOutAsync(AppUser user);
            Task<(bool Success, string Message, AppUser User)> SignInAsync(LoginAppUserDto loginDto);
            Task<string> SavePasswordResetTokenAsync(AppUser user, string provider, string name);
            Task<IdentityResult> ResetUserPasswordAsync(AppUser user, string token, string newPassword);
            Task SignOutAsync();
    }
}
