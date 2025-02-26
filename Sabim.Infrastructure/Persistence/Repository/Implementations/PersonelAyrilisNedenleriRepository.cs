using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class PersonelAyrilisNedenleriRepository : RepositoryBase<PersonelAyrilisNedenleri>, IPersonelAyrilisNedenleriRepository
    {
        public PersonelAyrilisNedenleriRepository(SabimDbContext context) : base(context)
        {
        }
    }
}
