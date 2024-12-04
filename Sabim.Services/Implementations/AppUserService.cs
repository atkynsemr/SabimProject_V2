using Microsoft.AspNetCore.Authentication;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.AppUserDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;
using System.Security.Claims;

namespace Sabim.Services.Implementations
{
    public class AppUserService : GenericService<AppUser>, IAppUserService
    {
        private readonly IRepositoryManager _repositoryManager;
        public AppUserService(IRepositoryBase<AppUser> repository, IRepositoryManager repositoryManager) : base(repository)
        {
            _repositoryManager = repositoryManager;
        }
        // FindByEmailAsync methodu
        public async Task<AppUser> TFindByEmailAsync(string email)
        {
            return await _repositoryManager.AppUser.FindByEmailAsync(email);
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
    }
}
