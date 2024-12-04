using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class DurumService : GenericService<Durum>, IDurumService
    {
        private readonly IRepositoryManager _repositoryManager;
        public DurumService(IRepositoryBase<Durum> repository, IRepositoryManager repositoryManager) : base(repository)
        {
            _repositoryManager = repositoryManager;
        }
    }
}
