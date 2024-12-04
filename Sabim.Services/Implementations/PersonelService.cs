using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class PersonelService : GenericService<Personel>, IPersonelService
    {
        private readonly IRepositoryManager _repositoryManager;
        public PersonelService(IRepositoryBase<Personel> repository, IRepositoryManager repositoryManager) : base(repository)
        {
            _repositoryManager = repositoryManager;
        }
    }
}
