using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class GorevlendirilmeTipiService : GenericService<GorevlendirilmeTipi>, IGorevlendirilmeTipiService
    {
        public GorevlendirilmeTipiService(IRepositoryBase<GorevlendirilmeTipi> repository) : base(repository)
        {
        }
    }
}
