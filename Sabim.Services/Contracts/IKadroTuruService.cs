using Sabim.Domain.DTOs.KadroTuruDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.Contracts
{
    public interface IKadroTuruService: IGenericService<KadroTuru>
    {
        Task<List<ResultKadroTuruWithPersonelCountDto>> TGetAllKadroTuruWithPersonelCountAsync(bool trankChanges);
    }
}
