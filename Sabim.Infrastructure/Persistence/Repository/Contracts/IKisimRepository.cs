using Sabim.Domain.DTOs.KisimDtos;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Repository.Contracts
{
    public interface IKisimRepository :IRepositoryBase<Kisim>
    {
        Task<List<ResultKisimWithPersonelCountDto>> GetAllKisimWithPersonelCountAsync(bool trackChanges);
        bool IsKisimExists(string kisimAdi, short birimId, short? excludeId = null);
    }
}
