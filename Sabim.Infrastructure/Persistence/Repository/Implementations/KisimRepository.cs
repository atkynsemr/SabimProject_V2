using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sabim.Domain.DTOs.KisimDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class KisimRepository : RepositoryBase<Kisim>, IKisimRepository
    {
        private readonly IMapper _mapper;
        public KisimRepository(SabimDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<ResultKisimWithPersonelCountDto>> GetAllKisimWithPersonelCountAsync(bool trackChanges)
        {
            IQueryable<Kisim> query = _context.Kisim;
            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }
            var result = await query
                .ProjectTo<ResultKisimWithPersonelCountDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return result;
        }

        public bool IsKisimExists(string kisimAdi, short birimId, short? excludeId = null)
        {
            return _context.Kisim
               .AsNoTracking() // Performans için AsNoTracking kullanımı
               .Where(k => k.KisimAdi == kisimAdi && k.BirimId == birimId) // Şartları uygula
               .Where(k => !excludeId.HasValue || k.KisimID != excludeId.Value) // excludeId varsa hariç tut
               .Any(); // Herhangi bir eşleşme var mı kontrol et
        }
    }
}
