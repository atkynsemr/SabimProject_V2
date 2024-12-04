using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sabim.Domain.DTOs.UnvanDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class UnvanRepository : RepositoryBase<Unvan>, IUnvanRepository
    {
        private readonly IMapper _mapper;
        public UnvanRepository(SabimDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public async Task<List<ResultUnvanWithPersonelCountDto>> GetAllUnvanWithPersonelCountAsync(bool trackChanges)
        {
            IQueryable<Unvan> query = _context.Unvan;
            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }
            var result = await query
                .ProjectTo<ResultUnvanWithPersonelCountDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return result;
        }
    }
}
