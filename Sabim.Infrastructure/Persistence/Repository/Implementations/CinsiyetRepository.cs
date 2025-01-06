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
            #region Join-GroupJoin 
            //var result = await query
            //    .Join(
            //        _context.Durum,
            //        c => c.DurumId,
            //        d => d.DurumID,
            //        (c, d) => new
            //        {
            //            Cinsiyet = c,
            //            DurumAdi = d.DurumAdi
            //        }
            //    )
            //    .GroupJoin(
            //        _context.Personel,
            //        cd => cd.Cinsiyet.CinsiyetID,
            //        p => p.CinsiyetId,
            //        (cd, personels) => new
            //        {
            //            Cinsiyet = cd.Cinsiyet,
            //            DurumAdi = cd.DurumAdi,
            //            PersonelSayisi = personels.Count()
            //        }
            //    )
            //    .Select(g => new ResultCinsiyetWithPersonelCountDto
            //    {
            //        CinsiyetID = g.Cinsiyet.CinsiyetID,
            //        CinsiyetAdi = g.Cinsiyet.CinsiyetAdi,
            //        PersonelSayisi = (ushort)g.PersonelSayisi,
            //        DurumAdi = g.DurumAdi
            //    })
            //    .ToListAsync();
            #endregion
            #region Include 
            //var result = await query
            //        .Include(c => c.Personels)
            //        .Select(c => new ResultCinsiyetWithPersonelCountDto
            //        {
            //            CinsiyetID = c.CinsiyetID,
            //            CinsiyetAdi = c.CinsiyetAdi,
            //            PersonelSayisi = (ushort)c.Personels.Count,
            //            DurumAdi = c.Durum.DurumAdi
            //        })
            //        .ToListAsync();
            #endregion
            var result = await query
                   .ProjectTo<ResultCinsiyetWithPersonelCountDto>(_mapper.ConfigurationProvider)
                   .ToListAsync();
            return result;
        }
    }
}

