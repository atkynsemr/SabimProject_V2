using Sabim.Domain.DTOs.SehirDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.Contracts
{
    public interface ISehirService :IGenericService<Sehir>
    {
        Task<List<ResultSehirWithKurumCountDto>> TGetAllSehirWithKurumCountAsync(bool trackChanges);
    }
}
