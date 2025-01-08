using Sabim.Domain.DTOs.BirimDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.Contracts
{
    public interface IBirimService : IGenericService<Birim>
    {
        Task<List<ResultBirimWithKisimCountDto>> TGetAllBirimWithKisimCountAsync(bool trackChanges);
    }
}
