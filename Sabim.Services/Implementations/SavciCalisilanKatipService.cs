using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class SavciCalisilanKatipService : GenericService<SavciCalisilanKatip>, ISavciCalisilanKatipService
    {
        private readonly IRepositoryManager _repositoryManager;
        public SavciCalisilanKatipService(IRepositoryBase<SavciCalisilanKatip> repository, IRepositoryManager repositoryManager) : base(repository)
        {
            _repositoryManager = repositoryManager;
        }
    }
}
