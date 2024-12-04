using Sabim.Domain.DTOs.GorevlendirilmeTuruDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.Contracts
{
    public interface IGorevlendirilmeTuruService: IGenericService<GorevlendirilmeTuru>
    {
        Task<List<ResultGorevlendirilmeTuruWithPersonelCountDto>> TGetAllGorevlendirilmeTurWithPersonelCountAsync(bool trackChanges);
    }
}
