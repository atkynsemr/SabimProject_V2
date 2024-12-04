using Sabim.Domain.DTOs.KurumTipiDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.Contracts
{
    public interface IKurumTipiService : IGenericService<KurumTipi>
    {
        Task<List<ResultKurumTipiWithKurumCountDto>> TGetAllKurumTipiWithKurumCountAsync(bool trackChanges);
    }
}
