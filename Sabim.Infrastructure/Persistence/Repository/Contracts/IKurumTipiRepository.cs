using Sabim.Domain.DTOs.KurumTipiDtos;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Repository.Contracts
{
    public interface IKurumTipiRepository :IRepositoryBase<KurumTipi>
    {
        Task<List<ResultKurumTipiWithKurumCountDto>> GetAllKurumTipiWithKurumCountAsync(bool trackChanges);
    }
}
