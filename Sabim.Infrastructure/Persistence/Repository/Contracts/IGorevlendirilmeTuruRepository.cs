using Sabim.Domain.DTOs.GorevlendirilmeTuruDtos;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Repository.Contracts
{
    public interface IGorevlendirilmeTuruRepository :IRepositoryBase<GorevlendirilmeTuru>
    {
        Task<List<ResultGorevlendirilmeTuruWithPersonelCountDto>> GetAllGorevlendirilmeTurWithPersonelCountAsync(bool trackChanges);
    }
}
