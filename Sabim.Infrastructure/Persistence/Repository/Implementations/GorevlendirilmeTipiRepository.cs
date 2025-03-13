using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class GorevlendirilmeTipiRepository : RepositoryBase<GorevlendirilmeTipi>, IGorevlendirilmeTipiRepository
    {
        public GorevlendirilmeTipiRepository(SabimDbContext context) : base(context)
        {
        }
    }
}
