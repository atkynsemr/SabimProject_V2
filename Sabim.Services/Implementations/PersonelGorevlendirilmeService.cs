using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class PersonelGorevlendirilmeService : GenericService<PersonelGorevlendirilme>, IPersonelGorevlendirilmeService
    {
        public PersonelGorevlendirilmeService(IRepositoryBase<PersonelGorevlendirilme> repository) : base(repository)
        {
        }
    }
}
