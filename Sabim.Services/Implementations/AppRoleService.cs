using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class AppRoleService : GenericService<AppRole>, IAppRoleService
    {
        public AppRoleService(IRepositoryBase<AppRole> repository) : base(repository)
        {
        }
    }
}
