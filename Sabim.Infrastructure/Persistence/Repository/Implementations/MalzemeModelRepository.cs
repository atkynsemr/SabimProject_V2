using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class MalzemeModelRepository : RepositoryBase<MalzemeModel>, IMalzemeModelRepository
    {
        public MalzemeModelRepository(SabimDbContext context) : base(context)
        {
        }
    }
}
