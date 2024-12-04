using Sabim.Domain.DTOs.CinsiyetDtos;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Repository.Contracts
{
    public interface ICinsiyetRepository :IRepositoryBase<Cinsiyet>
    {
        Task<List<ResultCinsiyetWithPersonelCountDto>> GetAllCinsiyetWithPersonelCountAsync(bool trackChanges);
    }
}
