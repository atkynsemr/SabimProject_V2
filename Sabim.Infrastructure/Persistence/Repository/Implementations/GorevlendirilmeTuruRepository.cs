using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sabim.Domain.DTOs.GorevlendirilmeTuruDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class GorevlendirilmeTuruRepository : RepositoryBase<GorevlendirilmeTuru>, IGorevlendirilmeTuruRepository
    {
        private readonly IMapper _mapper;
        public GorevlendirilmeTuruRepository(SabimDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<ResultGorevlendirilmeTuruWithPersonelCountDto>> GetAllGorevlendirilmeTurWithPersonelCountAsync(bool trackChanges)
        {
            IQueryable<GorevlendirilmeTuru> query = _context.GorevlendirilmeTuru;
            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }
            var result = await query
                .ProjectTo<ResultGorevlendirilmeTuruWithPersonelCountDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return result;
        }
    }
}
