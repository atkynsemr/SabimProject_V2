using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class MalzemeDurumuService : GenericService<MalzemeDurumu>, IMalzemeDurumuService
    {
        public MalzemeDurumuService(IRepositoryBase<MalzemeDurumu> repository) : base(repository)
        {
        }
    }
}
