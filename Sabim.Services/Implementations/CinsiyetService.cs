using Sabim.Domain.DTOs.CinsiyetDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class CinsiyetService : GenericService<Cinsiyet>, ICinsiyetService
    {
        private readonly IRepositoryManager _repositoryManager;
        public CinsiyetService(IRepositoryBase<Cinsiyet> repository, IRepositoryManager repositoryManager) : base(repository)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<List<ResultCinsiyetWithPersonelCountDto>> TGetAllCinsiyetWithPersonelCountAsync(bool trackChanges)
        {
            var cinsiyets = await _repositoryManager.Cinsiyet.GetAllCinsiyetWithPersonelCountAsync(trackChanges);
            return cinsiyets;
        }
    }
}
