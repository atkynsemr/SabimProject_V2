using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class AppRoleRepository : RepositoryBase<AppRole>, IAppRoleRepository
    {
        public AppRoleRepository(SabimDbContext context) : base(context)
        {
        }
    }
}
