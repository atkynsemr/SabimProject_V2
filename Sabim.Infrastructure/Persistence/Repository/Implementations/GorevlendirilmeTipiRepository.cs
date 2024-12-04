using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class GorevlendirilmeTipiRepository : RepositoryBase<GorevlendirilmeTipi>, IGorevlendirilmeTipiRepository
    {
        public GorevlendirilmeTipiRepository(SabimDbContext context) : base(context)
        {
        }
    }
}
