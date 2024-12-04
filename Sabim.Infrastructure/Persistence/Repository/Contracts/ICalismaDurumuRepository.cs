using Sabim.Domain.DTOs.CalismaDurumuDtos;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Repository.Contracts
{
    public interface ICalismaDurumuRepository : IRepositoryBase<CalismaDurumu>
    {
        Task<List<ResultCalismaDurumuWithPersonelCountDto>> GetAllCalismaDurumuWithPersonelCountAsync(bool trackChanges);
    }
}
