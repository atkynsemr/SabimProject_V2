using Microsoft.AspNetCore.Authentication;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.AppUserDtos;
using Sabim.Domain.Entities;
using System.Security.Claims;

namespace Sabim.Services.Contracts
{
    public interface IAppUserService : IGenericService<AppUser>
    {
        Task<AppUser> TFindByEmailAsync(string email);
        Task<bool> TCheckPasswordAsync(AppUser user, string password);
        Task<bool> TIsUserLockedOutAsync(AppUser user);
        Task<(bool Success, string Message, AppUser User)> TSignInAsync(LoginAppUserDto loginDto);
        Task TSignOutAsync();
    }
}
