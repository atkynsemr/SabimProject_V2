using Sabim.Domain.DTOs.PersonelGorevlendirilmeDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Infrastructure.Persistence.Repository.Implementations;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class PersonelGorevlendirilmeService : GenericService<PersonelGorevlendirilme>, IPersonelGorevlendirilmeService
    {
        private readonly IRepositoryManager _repositoryManager;
        public PersonelGorevlendirilmeService(IRepositoryBase<PersonelGorevlendirilme> repository, IRepositoryManager repositoryManager) : base(repository)
        {
            _repositoryManager = repositoryManager;
        }
    }
}
