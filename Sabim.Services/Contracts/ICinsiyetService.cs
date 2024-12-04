using Sabim.Domain.DTOs.CinsiyetDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.Contracts
{
    public interface ICinsiyetService : IGenericService<Cinsiyet>
    {
        Task<List<ResultCinsiyetWithPersonelCountDto>> TGetAllCinsiyetWithPersonelCountAsync(bool trackChanges);
    }
}
