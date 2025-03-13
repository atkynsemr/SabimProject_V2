using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class MalzemeDurumuRepository : RepositoryBase<MalzemeDurumu>, IMalzemeDurumuRepository
    {
        public MalzemeDurumuRepository(SabimDbContext context) : base(context)
        {
        }
    }
}
