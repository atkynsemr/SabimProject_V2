using Sabim.Domain.DTOs.GorevlendirilmeTuruDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class GorevlendirilmeTuruService : GenericService<GorevlendirilmeTuru>, IGorevlendirilmeTuruService
    {
        private readonly IRepositoryManager _repositoryManager;
        public GorevlendirilmeTuruService(IRepositoryBase<GorevlendirilmeTuru> repository, IRepositoryManager repositoryManager) : base(repository)
        {
            _repositoryManager = repositoryManager;
        }
        public async Task<List<ResultGorevlendirilmeTuruWithPersonelCountDto>> TGetAllGorevlendirilmeTurWithPersonelCountAsync(bool trackChanges)
        {
            var gorevlendirilmeTurus = await _repositoryManager.GorevlendirilmeTuru.GetAllGorevlendirilmeTurWithPersonelCountAsync(trackChanges);
            return gorevlendirilmeTurus;
        }
    }
}
