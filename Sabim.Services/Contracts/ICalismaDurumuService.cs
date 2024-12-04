using Sabim.Domain.DTOs.CalismaDurumuDtos;
using Sabim.Domain.DTOs.UnvanDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.Contracts
{
    public interface ICalismaDurumuService : IGenericService<CalismaDurumu>
    {
        Task<List<ResultCalismaDurumuWithPersonelCountDto>> TGetAllCalismaDurumuWithPersonelCountAsync(bool trackChanges);
    }
}
