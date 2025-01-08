using Sabim.Domain.DTOs.BirimDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Infrastructure.Persistence.Repository.Implementations;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class BirimService : GenericService<Birim>, IBirimService
    {
        private readonly IRepositoryManager _repositoryManager;
        public BirimService(IRepositoryBase<Birim> repository, IRepositoryManager repositoryManager) : base(repository)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<List<ResultBirimWithKisimCountDto>> TGetAllBirimWithKisimCountAsync(bool trackChanges)
        {
            var birims = await _repositoryManager.Birim.GetAllBirimWithKisimCountAsync(trackChanges);
            return birims;
        }
    }
}
