using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class MalzemeTuruRepository : RepositoryBase<MalzemeTuru>, IMalzemeTuruRepository
    {
        public MalzemeTuruRepository(SabimDbContext context) : base(context)
        {
        }

    }
}
