using Sabim.Domain.DTOs.CalismaDurumuDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class CalismaDurumuService : GenericService<CalismaDurumu>, ICalismaDurumuService
    {
        private readonly IRepositoryManager _repositoryManager;
        public CalismaDurumuService(IRepositoryBase<CalismaDurumu> repository, IRepositoryManager repositoryManager) : base(repository)
        {
            _repositoryManager = repositoryManager;           
        }

        public async Task<List<ResultCalismaDurumuWithPersonelCountDto>> TGetAllCalismaDurumuWithPersonelCountAsync(bool trackChanges)
        {
            var calismaDurumus = await _repositoryManager.CalismaDurumu.GetAllCalismaDurumuWithPersonelCountAsync(trackChanges);
            return calismaDurumus;
        }
    }
}
