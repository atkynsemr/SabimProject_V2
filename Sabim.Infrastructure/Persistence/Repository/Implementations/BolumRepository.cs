using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sabim.Domain.DTOs.BolumDtos;
using Sabim.Domain.DTOs.KurumDtos;
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
    public class BolumRepository : RepositoryBase<Bolum>, IBolumRepository
    {
        private readonly IMapper _mapper;
        public BolumRepository(SabimDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<ResultBolumWithBirimCountDto>> GetAllBolumWithBirimCountAsync(bool trackChanges)
        {
            IQueryable<Bolum> query = _context.Bolum;
            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }
            var result = await query
                .ProjectTo<ResultBolumWithBirimCountDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return result;
        }
    }
}
