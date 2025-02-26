using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class PersonelAyrilisNedenleriService : GenericService<PersonelAyrilisNedenleri>, IPersonelAyrilisNedenleriService
    {
        public PersonelAyrilisNedenleriService(IRepositoryBase<PersonelAyrilisNedenleri> repository) : base(repository)
        {
        }
    }
}
