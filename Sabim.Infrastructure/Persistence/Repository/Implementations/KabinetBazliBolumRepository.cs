using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class KabinetBazliBolumRepository : RepositoryBase<KabinetBazliBolum>, IKabinetBazliBolumRepository
    {
        public KabinetBazliBolumRepository(SabimDbContext context) : base(context)
        {
        }
    }
}
