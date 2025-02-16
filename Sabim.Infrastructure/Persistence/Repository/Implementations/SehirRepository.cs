using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sabim.Domain.DTOs.SehirDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class SehirRepository : RepositoryBase<Sehir>, ISehirRepository
    {
        private readonly IMapper _mapper;
        public SehirRepository(SabimDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public async Task<List<ResultSehirWithKurumCountDto>> GetAllSehirWithKurumCountAsync(bool trackChanges)
        {
            IQueryable<Sehir> query = _context.Sehir;
            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }
            #region Join-GroupJoin 
            //var result = await query
            //    .Join(
            //        _context.Durum, // INNER JOIN yapılacak tablo
            //        sehir => sehir.DurumId, // Sehir tablosundaki Foreign Key
            //        durum => durum.DurumID, // Durum tablosundaki Primary Key
            //        (sehir, durum) => new // JOIN sonucu
            //        {
            //            Sehir = sehir,
            //            DurumId = durum.DurumID,
            //            DurumAdi = durum.DurumAdi
            //        }
            //    )
            //    .GroupJoin(
            //        _context.Kurum,
            //        sehirWithDurum => sehirWithDurum.Sehir.SehirID, // Sehir tablosundaki ID
            //        kurum => kurum.SehirId, // Kurum tablosundaki Foreign Key
            //        (sehirWithDurum, kurums) => new
            //        {
            //            Sehir = sehirWithDurum.Sehir,
            //            KurumSayisi = kurums.Count(),
            //            DurumAdi = sehirWithDurum.DurumAdi
            //        }
            //    )
            //    .Select(g => new ResultSehirWithKurumCountDto
            //    {
            //        SehirID = g.Sehir.SehirID,
            //        SehirKodu = g.Sehir.SehirKodu,
            //        SehirAdi = g.Sehir.SehirAdi,
            //        KurumSayisi = (ushort)g.KurumSayisi,
            //        DurumId = g.Sehir.DurumId,
            //        DurumAdi = g.DurumAdi
            //    })
            //    .ToListAsync();
            #endregion
            #region Include
            //var result = await query
            //    .Include(sehir => sehir.Durum)  // Durum tablosunu ilişkilendiriyoruz
            //    .Include(sehir => sehir.Kurums)  // Kurum tablosunu ilişkilendiriyoruz
            //    .Select(sehir => new ResultSehirWithKurumCountDto
            //    {
            //        SehirID = sehir.SehirID,
            //        SehirKodu = sehir.SehirKodu,
            //        SehirAdi = sehir.SehirAdi,
            //        KurumSayisi = (ushort)sehir.Kurums.Count(), // Kurumların sayısını alıyoruz
            //        DurumId = sehir.DurumId,
            //        DurumAdi = sehir.Durum.DurumAdi // Durum tablosundaki DurumAdi
            //    })
            //    .ToListAsync();
            #endregion
            var result = await query
                        .ProjectTo<ResultSehirWithKurumCountDto>(_mapper.ConfigurationProvider)
                        .ToListAsync();
            return result;
        }
    }
}

