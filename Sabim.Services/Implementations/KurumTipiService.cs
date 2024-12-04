using Sabim.Domain.DTOs.KurumTipiDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class KurumTipiService : GenericService<KurumTipi>, IKurumTipiService
    {
        private readonly IRepositoryManager _repositoryManager;
        public KurumTipiService(IRepositoryBase<KurumTipi> repository, IRepositoryManager repositoryManager) : base(repository)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<List<ResultKurumTipiWithKurumCountDto>> TGetAllKurumTipiWithKurumCountAsync(bool trackChanges)
        {
            return await _repositoryManager.KurumTipi.GetAllKurumTipiWithKurumCountAsync(trackChanges);
        }
    }
}
