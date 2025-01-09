using Sabim.Domain.DTOs.KisimDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.Contracts
{
    public interface IKisimService :IGenericService<Kisim>
    {
        Task<List<ResultKisimWithPersonelCountDto>> TGetAllKisimWithPersonelCountAsync(bool trackChanges);
        bool TIsKisimExists(string kisimAdi, short birimId, short? excludeId = null);
    }
}
