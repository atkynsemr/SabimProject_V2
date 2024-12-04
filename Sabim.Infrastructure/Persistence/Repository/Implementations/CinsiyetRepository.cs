using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sabim.Domain.DTOs.CinsiyetDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class CinsiyetRepository : RepositoryBase<Cinsiyet>, ICinsiyetRepository
    {
        private readonly IMapper _mapper;
        public CinsiyetRepository(SabimDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public async Task<List<ResultCinsiyetWithPersonelCountDto>> GetAllCinsiyetWithPersonelCountAsync(bool trackChanges)
        {
            IQueryable<Cinsiyet> query = _context.Cinsiyet;
            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }
            var result = await query
                .ProjectTo<ResultCinsiyetWithPersonelCountDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return result;
        }
    }
}
