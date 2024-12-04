using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class BirimRepository : RepositoryBase<Birim>, IBirimRepository
    {
        public BirimRepository(SabimDbContext context) : base(context)
        {
        }
    }
}
