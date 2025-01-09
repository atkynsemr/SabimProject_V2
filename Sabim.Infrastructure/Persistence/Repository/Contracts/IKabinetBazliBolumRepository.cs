using Sabim.Domain.DTOs.KabinetBazliBolumDtos;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Repository.Contracts
{
    public interface IKabinetBazliBolumRepository : IRepositoryBase<KabinetBazliBolum>
    {
        Task<List<ResultKabinetBazliBolumWithKisimCountDto>> GetAllKabinetBazliBolumWithKisimCountAsync(bool trackChanges);
    }
}
