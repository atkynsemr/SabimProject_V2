using Sabim.Domain.DTOs.KurumDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class KurumService : GenericService<Kurum>, IKurumService
    {
        private readonly IRepositoryManager _repositoryManager;
        public KurumService(IRepositoryBase<Kurum> repository, IRepositoryManager repositoryManager) : base(repository)
        {
            _repositoryManager = repositoryManager;
        }
        public async Task<List<ResultKurumWithPersonelCountDto>> TGetAllKurumWithPersonelCountAsync(bool trackChanges)
        {
           var kurums = await _repositoryManager.Kurum.GetAllKurumWithPersonelCountAsync(trackChanges);
            return kurums;
        }
    }
}
