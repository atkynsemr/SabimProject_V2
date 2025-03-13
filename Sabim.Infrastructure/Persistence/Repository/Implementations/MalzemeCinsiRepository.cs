using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class MalzemeCinsiRepository : RepositoryBase<MalzemeCinsi>, IMalzemeCinsiRepository
    {
        public MalzemeCinsiRepository(SabimDbContext context) : base(context)
        {
        }
    }
}
