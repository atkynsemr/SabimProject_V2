using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class MalzemeService : GenericService<Malzeme>, IMalzemeService
    {
        public MalzemeService(IRepositoryBase<Malzeme> repository) : base(repository)
        {
        }
    }
}
