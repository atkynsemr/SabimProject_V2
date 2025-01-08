using Sabim.Domain.DTOs.BolumDtos;
using Sabim.Domain.DTOs.KurumDtos;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Repository.Contracts
{
    public interface IBolumRepository :IRepositoryBase<Bolum>
    {
        Task<List<ResultBolumWithBirimCountDto>> GetAllBolumWithBirimCountAsync(bool trackChanges);
    }
}
