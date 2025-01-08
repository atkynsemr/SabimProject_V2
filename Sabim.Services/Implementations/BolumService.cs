using Sabim.Domain.DTOs.BolumDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Infrastructure.Persistence.Repository.Implementations;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class BolumService : GenericService<Bolum>, IBolumService
    {
        private readonly IRepositoryManager _repositoryManager;
        public BolumService(IRepositoryBase<Bolum> repository, IRepositoryManager repositoryManager) : base(repository)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<List<ResultBolumWithBirimCountDto>> TGetAllBolumWithBirimCountAsync(bool trackChanges)
        {
            var bolums = await _repositoryManager.Bolum.GetAllBolumWithBirimCountAsync(trackChanges);
            return bolums;
        }
    }
}
