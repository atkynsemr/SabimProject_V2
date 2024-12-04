using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class BirimService : GenericService<Birim>, IBirimService
    {
        public BirimService(IRepositoryBase<Birim> repository) : base(repository)
        {
        }
    }
}
