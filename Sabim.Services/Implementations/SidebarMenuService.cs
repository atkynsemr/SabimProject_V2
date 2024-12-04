using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class SidebarMenuService : GenericService<SidebarMenu>, ISidebarMenuService
    {
        public SidebarMenuService(IRepositoryBase<SidebarMenu> repository) : base(repository)
        {
        }
    }
}
