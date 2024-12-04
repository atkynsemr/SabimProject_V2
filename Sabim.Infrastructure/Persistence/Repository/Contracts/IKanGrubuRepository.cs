using Sabim.Domain.DTOs.KanGrubuDtos;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Repository.Contracts
{
    public interface IKanGrubuRepository :IRepositoryBase<KanGrubu>
    {
        Task<List<ResultKanGrubuWithPersonelCountDto>> GetAllKanGrubuWithPersonelCountAsync(bool trackChanges);
    }
}
