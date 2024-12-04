using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sabim.Domain.DTOs.KadroTuruDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class KadroTuruRepository : RepositoryBase<KadroTuru>, IKadroTuruRepository
    {
        private readonly IMapper _mapper;
        public KadroTuruRepository(SabimDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public async Task<List<ResultKadroTuruWithPersonelCountDto>> GetAllKadroTuruWithPersonelCountAsync(bool trankChanges)
        {
            IQueryable<KadroTuru> query = _context.KadroTuru;
            if (!trankChanges)
            {
                query = query.AsNoTracking();
            }
            var result = await query
                .ProjectTo<ResultKadroTuruWithPersonelCountDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return result;                
        }
    }
}
