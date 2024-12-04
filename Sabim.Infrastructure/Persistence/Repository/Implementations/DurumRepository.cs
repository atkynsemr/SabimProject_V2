using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class DurumRepository : RepositoryBase<Durum>, IDurumRepository
    {
        public DurumRepository(SabimDbContext context) : base(context)
        {
        }
    }
}
