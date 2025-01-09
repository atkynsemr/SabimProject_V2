using Sabim.Domain.DTOs.KisimDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class KisimService : GenericService<Kisim>, IKisimService
    {
        private readonly IRepositoryManager _repositoryManager;
        public KisimService(IRepositoryBase<Kisim> repository, IRepositoryManager repositoryManager) : base(repository)
        {
            _repositoryManager = repositoryManager;
        }
        public async Task<List<ResultKisimWithPersonelCountDto>> TGetAllKisimWithPersonelCountAsync(bool trackChanges)
        {
            var kisims = await _repositoryManager.Kisim.GetAllKisimWithPersonelCountAsync(trackChanges);
            return kisims;
        }
        public bool TIsKisimExists(string kisimAdi, short birimId, short? excludeId = null)
        {
            return _repositoryManager.Kisim.IsKisimExists(kisimAdi, birimId, excludeId);
        }
    }
}
