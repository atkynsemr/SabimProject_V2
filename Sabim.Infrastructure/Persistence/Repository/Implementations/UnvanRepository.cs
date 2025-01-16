using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sabim.Domain.Constants;
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
        public async Task<string> ChangeOncelikSirasi(byte oncelikSirasi, short? UnvanID)
        {
            try
            {
                var unvansToUpdate = await _context.Unvan.Where(u => u.OncelikSirasi >= oncelikSirasi && (UnvanID == null || u.UnvanID != UnvanID)).ToListAsync();
                foreach (var unvan in unvansToUpdate)
                {
                    unvan.OncelikSirasi += 1; // Öncelik sırasını artırıyoruz
                    _context.Entry(unvan).Property(u => u.OncelikSirasi).IsModified = true;
                }
                // Veritabanındaki değişiklikleri kaydediyoruz
                var affectedRows = await _context.SaveChangesAsync(); // Veritabanına kaydet
                // Eğer etkilenen satır sayısı 0 ise, işlem başarısız olabilir
                if (affectedRows > 0)
                {
                    return OperationStatus.Success; // İşlem başarılı
                }
                else
                {
                    return OperationStatus.GlobalError; // Satır eklenmediği takdirde hata
                }
            }
            catch (DbUpdateException dbEx)
            {
                // Veritabanı ile ilgili spesifik hata
                return OperationStatus.GlobalError; // Örneğin, veritabanı bağlantısı veya benzeri bir hata
            }
            catch (Exception)
            {
                // Genel hata durumu
                return OperationStatus.GlobalError;
            }
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
