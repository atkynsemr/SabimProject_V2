using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sabim.Domain.DTOs.KabinetBazliBolumDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class KabinetBazliBolumRepository : RepositoryBase<KabinetBazliBolum>, IKabinetBazliBolumRepository
    {
        private readonly IMapper _mapper;
        public KabinetBazliBolumRepository(SabimDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<ResultKabinetBazliBolumWithKisimCountDto>> GetAllKabinetBazliBolumWithKisimCountAsync(bool trackChanges)
        {
            IQueryable<KabinetBazliBolum> query = _context.KabinetBazliBolum;
            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }
            var result = await query
                .ProjectTo<ResultKabinetBazliBolumWithKisimCountDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return result;
        }
    }
}
