using Sabim.Domain.DTOs.KanGrubuDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class KanGrubuService : GenericService<KanGrubu>, IKanGrubuService
    {
        private readonly IRepositoryManager _repositoryManager;
        public KanGrubuService(IRepositoryBase<KanGrubu> repository, IRepositoryManager repositoryManager) : base(repository)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<List<ResultKanGrubuWithPersonelCountDto>> TGetAllKanGrubuWithPersonelCountAsync(bool trackChanges)
        {
            var kanGrubus= await _repositoryManager.KanGrubu.GetAllKanGrubuWithPersonelCountAsync(trackChanges);
            return kanGrubus;
        }
    }
}
