using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sabim.Domain.DTOs.KanGrubuDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class KanGrubuRepository : RepositoryBase<KanGrubu>, IKanGrubuRepository
    {
        private readonly IMapper _mapper;
        public KanGrubuRepository(SabimDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<ResultKanGrubuWithPersonelCountDto>> GetAllKanGrubuWithPersonelCountAsync(bool trackChanges)
        {
            IQueryable<KanGrubu> query = _context.KanGrubu;
            if (!trackChanges) { query.AsNoTracking(); }
            var result = await query
                .ProjectTo<ResultKanGrubuWithPersonelCountDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return result;
        }
    }
}
