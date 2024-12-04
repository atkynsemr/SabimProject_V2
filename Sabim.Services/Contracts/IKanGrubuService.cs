using Sabim.Domain.DTOs.KanGrubuDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.Contracts
{
    public interface IKanGrubuService:IGenericService<KanGrubu>
    {
        Task<List<ResultKanGrubuWithPersonelCountDto>> TGetAllKanGrubuWithPersonelCountAsync(bool trackChanges);
    }
}
