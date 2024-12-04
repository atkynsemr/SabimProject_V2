using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class KabinetBazliBolumService : GenericService<KabinetBazliBolum>, IKabinetBazliBolumService
    {
        public KabinetBazliBolumService(IRepositoryBase<KabinetBazliBolum> repository) : base(repository)
        {
        }
    }
}
