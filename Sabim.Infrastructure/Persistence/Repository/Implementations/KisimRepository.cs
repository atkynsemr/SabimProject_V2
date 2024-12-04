using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class KisimRepository : RepositoryBase<Kisim>, IKisimRepository
    {
        public KisimRepository(SabimDbContext context) : base(context)
        {
        }
    }
}
