using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class PersonelRepository : RepositoryBase<Personel>, IPersonelRepository
    {
        public PersonelRepository(SabimDbContext context) : base(context)
        {
        }
    }
}
