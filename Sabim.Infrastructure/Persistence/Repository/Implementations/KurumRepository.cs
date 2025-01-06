using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sabim.Domain.DTOs.KurumDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class KurumRepository : RepositoryBase<Kurum>, IKurumRepository
    {
        private readonly IMapper _mapper;
        public KurumRepository(SabimDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public async Task<List<ResultKurumWithPersonelCountDto>> GetAllKurumWithPersonelCountAsync(bool trackChanges)
        {
            IQueryable<Kurum> query = _context.Kurum;
            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }
            var result = await query
                .ProjectTo<ResultKurumWithPersonelCountDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return result;
        }

        public bool IsKurumExists(string kurumAdi, short sehirId, short? excludeId = null)
        {
            return _context.Kurum
                .AsNoTracking() // Performans için AsNoTracking kullanımı
                .Where(k => k.KurumAdi == kurumAdi && k.SehirId == sehirId) // Şartları uygula
                .Where(k => !excludeId.HasValue || k.KurumID != excludeId.Value) // excludeId varsa hariç tut
                .Any(); // Herhangi bir eşleşme var mı kontrol et
        }
    }
}
