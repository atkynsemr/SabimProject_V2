using Sabim.Domain.DTOs.KabinetBazliBolumDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.Contracts
{
    public interface IKabinetBazliBolumService :IGenericService<KabinetBazliBolum>
    {
        Task<List<ResultKabinetBazliBolumWithKisimCountDto>> TGetAllKabinetBazliBolumWithKisimCountAsync(bool trackChanges);
    }
}
