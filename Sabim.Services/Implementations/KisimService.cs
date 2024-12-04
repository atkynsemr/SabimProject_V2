using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class KisimService : GenericService<Kisim>, IKisimService
    {
        public KisimService(IRepositoryBase<Kisim> repository) : base(repository)
        {
        }
    }
}
