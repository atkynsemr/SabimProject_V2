using Sabim.Domain.DTOs.BirimDtos;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Repository.Contracts
{
    public interface IBirimRepository : IRepositoryBase<Birim>
    {
        Task<List<ResultBirimWithKisimCountDto>> GetAllBirimWithKisimCountAsync(bool trackChanges);
    }
}
