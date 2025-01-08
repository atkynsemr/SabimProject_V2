using Sabim.Domain.DTOs.BolumDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.Contracts
{
    public interface IBolumService : IGenericService<Bolum>
    {
        Task<List<ResultBolumWithBirimCountDto>> TGetAllBolumWithBirimCountAsync(bool trackChanges);        
    }
}
