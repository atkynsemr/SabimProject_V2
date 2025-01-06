using Sabim.Domain.DTOs.KurumDtos;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Repository.Contracts
{
    public interface IKurumRepository : IRepositoryBase<Kurum>
    {
        Task<List<ResultKurumWithPersonelCountDto>> GetAllKurumWithPersonelCountAsync(bool trackChanges);
        bool IsKurumExists(string kurumAdi, short sehirId, short? excludeId = null);
    }
}
