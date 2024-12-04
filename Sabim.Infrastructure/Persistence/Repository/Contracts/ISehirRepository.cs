using Sabim.Domain.DTOs.SehirDtos;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Repository.Contracts
{
    public interface ISehirRepository :IRepositoryBase<Sehir>
    {
        Task<List<ResultSehirWithKurumCountDto>> GetAllSehirWithKurumCountAsync(bool trackChanges);
    }
}
