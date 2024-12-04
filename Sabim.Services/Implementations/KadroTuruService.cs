using Sabim.Domain.DTOs.KadroTuruDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class KadroTuruService : GenericService<KadroTuru>, IKadroTuruService
    {
        private readonly IRepositoryManager _repositoryManager;
        public KadroTuruService(IRepositoryBase<KadroTuru> repository, IRepositoryManager repositoryManager) : base(repository)
        {
            _repositoryManager= repositoryManager;
        }
        public async Task<List<ResultKadroTuruWithPersonelCountDto>> TGetAllKadroTuruWithPersonelCountAsync(bool trankChanges)
        {
            var kadroTurleri = await _repositoryManager.KadroTuru.GetAllKadroTuruWithPersonelCountAsync(trankChanges);
            return kadroTurleri;
        }
    }
}
