using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class MalzemeTuruService : GenericService<MalzemeTuru>, IMalzemeTuruService
    {
        public MalzemeTuruService(IRepositoryBase<MalzemeTuru> repository) : base(repository)
        {
        }
    }
}
