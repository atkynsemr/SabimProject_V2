using Sabim.Domain.DTOs.UnvanDtos;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Repository.Contracts
{
    public interface IUnvanRepository :IRepositoryBase<Unvan>
    {
        Task<List<ResultUnvanWithPersonelCountDto>> GetAllUnvanWithPersonelCountAsync(bool trackChanges);
    }
}
