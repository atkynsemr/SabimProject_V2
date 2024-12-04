using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class EkranService : GenericService<Ekran>, IEkranService
    {
        public EkranService(IRepositoryBase<Ekran> repository) : base(repository)
        {
        }
    }
}
