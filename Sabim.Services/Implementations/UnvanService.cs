using Sabim.Domain.DTOs.UnvanDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class UnvanService : GenericService<Unvan>, IUnvanService
    {
        private readonly IRepositoryManager _repositoryManager;
     
        public UnvanService(IRepositoryBase<Unvan> repository, IRepositoryManager repositoryManager) : base(repository)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<List<ResultUnvanWithPersonelCountDto>> TGetAllUnvanWithPersonelCountAsync(bool trackChanges)
        {
           var unvans= await _repositoryManager.Unvan.GetAllUnvanWithPersonelCountAsync(trackChanges);
           return unvans;
        }
    }
}
