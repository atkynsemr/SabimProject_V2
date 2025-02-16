using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class SavciCalisilanKatipServiceRepository : RepositoryBase<SavciCalisilanKatip>, ISavciCalisilanKatipServiceRepository
    {
        public SavciCalisilanKatipServiceRepository(SabimDbContext context) : base(context)
        {
        }
    }
}
