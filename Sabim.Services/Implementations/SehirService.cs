using AutoMapper;
using Sabim.Domain.DTOs.SehirDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class SehirService : GenericService<Sehir>, ISehirService
    {
        private readonly IRepositoryManager _repositoryManager;
        public SehirService(IRepositoryBase<Sehir> repository, IRepositoryManager repositoryManager) : base(repository)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<List<ResultSehirWithKurumCountDto>> TGetAllSehirWithKurumCountAsync(bool trackChanges)
        {
            var sehirs = await _repositoryManager.Sehir.GetAllSehirWithKurumCountAsync(trackChanges);
            return sehirs;
        }
    }
}
