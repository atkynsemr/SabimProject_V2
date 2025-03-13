using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class MalzemeMarkaRepository : RepositoryBase<MalzemeMarka>, IMalzemeMarkaRepository
    {
        public MalzemeMarkaRepository(SabimDbContext context) : base(context)
        {
        }

    }
}
