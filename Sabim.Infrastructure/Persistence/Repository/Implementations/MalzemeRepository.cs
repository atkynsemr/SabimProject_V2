using Microsoft.EntityFrameworkCore;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class MalzemeRepository : RepositoryBase<Malzeme>, IMalzemeRepository
    {
        public MalzemeRepository(SabimDbContext context) : base(context)
        {
        }

    }
}
