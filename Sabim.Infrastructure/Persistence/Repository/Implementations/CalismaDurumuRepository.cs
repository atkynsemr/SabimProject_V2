using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sabim.Domain.DTOs.CalismaDurumuDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using System;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class CalismaDurumuRepository : RepositoryBase<CalismaDurumu>, ICalismaDurumuRepository
    {
        private readonly IMapper _mapper;
        public CalismaDurumuRepository(SabimDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<ResultCalismaDurumuWithPersonelCountDto>> GetAllCalismaDurumuWithPersonelCountAsync(bool trackChanges)
        {
            IQueryable<CalismaDurumu> query = _context.CalismaDurumu;
            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }
            var result = await query
                .ProjectTo<ResultCalismaDurumuWithPersonelCountDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return result;
        }
    }
}
