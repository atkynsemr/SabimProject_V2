using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class MalzemeCinsiService : GenericService<MalzemeCinsi>, IMalzemeCinsiService
    {
        public MalzemeCinsiService(IRepositoryBase<MalzemeCinsi> repository) : base(repository)
        {
        }
    }
}
