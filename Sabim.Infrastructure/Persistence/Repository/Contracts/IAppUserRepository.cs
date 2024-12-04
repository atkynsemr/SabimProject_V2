using Sabim.Domain.DTOs.AppUserDtos;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Repository.Contracts
{
    public interface IAppUserRepository : IRepositoryBase<AppUser>
    {
            Task<AppUser> FindByEmailAsync(string email);
            Task<bool> CheckPasswordAsync(AppUser user, string password);
            Task<bool> IsUserLockedOutAsync(AppUser user);
            Task<(bool Success, string Message, AppUser User)> SignInAsync(LoginAppUserDto loginDto);
            Task SignOutAsync();
    }
}
