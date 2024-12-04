using Sabim.Domain.DTOs.KadroTuruDtos;
using Sabim.Domain.DTOs.KurumDtos;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Repository.Contracts
{
    public interface IKadroTuruRepository: IRepositoryBase<KadroTuru>
    {
        Task<List<ResultKadroTuruWithPersonelCountDto>> GetAllKadroTuruWithPersonelCountAsync(bool trankChanges);
    }
}
