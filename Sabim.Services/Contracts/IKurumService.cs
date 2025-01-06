using Sabim.Domain.DTOs.KurumDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.Contracts
{
    public interface IKurumService : IGenericService<Kurum>
    {
        Task<List<ResultKurumWithPersonelCountDto>> TGetAllKurumWithPersonelCountAsync(bool trackChanges);
        bool TIsKurumExists(string kurumAdi, short sehirId, short? excludeId = null);
    }
}
