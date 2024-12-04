using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sabim.Domain.DTOs.CinsiyetDtos;
using Sabim.Domain.DTOs.KurumTipiDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class KurumTipiRepository : RepositoryBase<KurumTipi>, IKurumTipiRepository
    {
        private readonly IMapper _mapper;
        public KurumTipiRepository(SabimDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public async Task<List<ResultKurumTipiWithKurumCountDto>> GetAllKurumTipiWithKurumCountAsync(bool trackChanges)
        {
            IQueryable<KurumTipi> query = _context.KurumTipi;
            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }
            var result = await query
                .ProjectTo<ResultKurumTipiWithKurumCountDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return result;
        }
    }
}
