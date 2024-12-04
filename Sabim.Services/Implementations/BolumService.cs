using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class BolumService : GenericService<Bolum>, IBolumService
    {
        public BolumService(IRepositoryBase<Bolum> repository) : base(repository)
        {
        }
    }
}
