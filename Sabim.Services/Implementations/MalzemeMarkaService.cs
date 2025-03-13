using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class MalzemeMarkaService : GenericService<MalzemeMarka>, IMalzemeMarkaService
    {
        public MalzemeMarkaService(IRepositoryBase<MalzemeMarka> repository) : base(repository)
        {
        }
    }
}
