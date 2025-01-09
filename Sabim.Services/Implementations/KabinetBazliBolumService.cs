using Sabim.Domain.DTOs.KabinetBazliBolumDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class KabinetBazliBolumService : GenericService<KabinetBazliBolum>, IKabinetBazliBolumService
    {
        private readonly IRepositoryManager _repositoryManager;
        public KabinetBazliBolumService(IRepositoryBase<KabinetBazliBolum> repository, IRepositoryManager repositoryManager) : base(repository)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<List<ResultKabinetBazliBolumWithKisimCountDto>> TGetAllKabinetBazliBolumWithKisimCountAsync(bool trackChanges)
        {
            var kabinetBazliBolums = await _repositoryManager.KabinetBazliBolum.GetAllKabinetBazliBolumWithKisimCountAsync(trackChanges);
            return kabinetBazliBolums;
        }
    }
}
