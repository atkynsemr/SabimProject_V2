using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class EkranRepository : RepositoryBase<Ekran>, IEkranRepository
    {
        public EkranRepository(SabimDbContext context) : base(context)
        {
        }
    }
}
