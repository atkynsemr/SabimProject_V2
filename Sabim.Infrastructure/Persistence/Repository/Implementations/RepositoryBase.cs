using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.HelperDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected SabimDbContext _context;
        private static readonly Dictionary<Type, string> PrimaryKeyCache = new();
        public RepositoryBase(SabimDbContext context)
        {
            _context = context;
        }
        private string FormatString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }
        private void FormatEntityStrings(T entity)
        {
            var stringProperties = entity.GetType().GetProperties()
                .Where(p => p.PropertyType == typeof(string) && p.CanWrite);

            foreach (var property in stringProperties)
            {
                var currentValue = (string?)property.GetValue(entity);
                if (!string.IsNullOrEmpty(currentValue))
                {
                    property.SetValue(entity, FormatString(currentValue));
                }
            }
        }
        private async Task<object?> FindEntityByIdAsync<TKey>(TKey id, string keyProperty)
        {
            return typeof(TKey) switch
            {
                Type when typeof(TKey) == typeof(int) => await Entity.AsNoTracking().SingleOrDefaultAsync(e => EF.Property<int>(e, keyProperty) == (int)(object)id),
                Type when typeof(TKey) == typeof(short) => await Entity.AsNoTracking().SingleOrDefaultAsync(e => EF.Property<short>(e, keyProperty) == (short)(object)id),
                Type when typeof(TKey) == typeof(long) => await Entity.AsNoTracking().SingleOrDefaultAsync(e => EF.Property<long>(e, keyProperty) == (long)(object)id),
                Type when typeof(TKey) == typeof(Guid) => await Entity.AsNoTracking().SingleOrDefaultAsync(e => EF.Property<Guid>(e, keyProperty) == (Guid)(object)id),
                Type when typeof(TKey) == typeof(string) => await Entity.AsNoTracking().SingleOrDefaultAsync(e => EF.Property<string>(e, keyProperty) == (string)(object)id),
                _ => throw new InvalidOperationException($"Desteklenmeyen tür: {typeof(TKey)}")
            };
        }
        public DbSet<T> Entity { get => _context.Set<T>(); }
        public async Task<string> AddAsync(T entity)
        {
            try
            {
                FormatEntityStrings(entity); // Gerekli formatlama işlemleri
                await Entity.AddAsync(entity); // Yeni veriyi ekle
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
        public async Task<string> DeleteAsync<TKey>(TKey id)
        {
            try
            {
                // 1. PrimaryKey'in adını almak
                if (!PrimaryKeyCache.TryGetValue(typeof(T), out var keyProperty))
                {
                    // Birincil anahtarın adını alıyoruz
                    keyProperty = _context.Model
                                           .FindEntityType(typeof(T))
                                           ?.FindPrimaryKey()
                                           ?.Properties.FirstOrDefault()?.Name;

                    if (string.IsNullOrEmpty(keyProperty))
                    {
                        // Eğer primary key bulunamazsa hata döndürülür
                        throw new InvalidOperationException(OperationStatus.PrimaryKeyNotDefined);
                    }

                    PrimaryKeyCache[typeof(T)] = keyProperty;
                }

                // 2. Entity'yi bulmak için doğru property tipi ile sorgu yapıyoruz
                var entity = typeof(TKey) switch
                {
                    Type when typeof(TKey) == typeof(int) => await Entity.SingleOrDefaultAsync(e => EF.Property<int>(e, keyProperty) == (int)(object)id),
                    Type when typeof(TKey) == typeof(short) => await Entity.SingleOrDefaultAsync(e => EF.Property<short>(e, keyProperty) == (short)(object)id),
                    Type when typeof(TKey) == typeof(long) => await Entity.SingleOrDefaultAsync(e => EF.Property<long>(e, keyProperty) == (long)(object)id),
                    _ => throw new InvalidOperationException($"Desteklenmeyen tür: {typeof(TKey)}")
                };

                // 3. Eğer entity bulunmazsa, hata mesajı döndürüyoruz
                if (entity == null)
                {
                    return OperationStatus.NotFound; // Kayıt bulunamadı
                }

                // 4. Silme işlemi
                Entity.Remove(entity);
                var affectedRows = await _context.SaveChangesAsync(); // Veritabanı değişikliklerini kaydetme
                if (affectedRows > 0)
                {
                    return OperationStatus.Success; // Silme işlemi başarılı
                }
                else
                {
                    return OperationStatus.GlobalError; // Genel hata
                }
            }
            catch (DbUpdateException dbEx) when (dbEx.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                // Dış anahtar hatası (SQL 547: Foreign key violation)
                return OperationStatus.ForeignKeyConflict;
            }
            catch (Exception)
            {
                // Diğer tüm hatalar
                return OperationStatus.GlobalError;
            }
        }
        public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ? Entity.AsNoTracking() : Entity;
        public async Task<List<T>> FindAllAsync(bool trackChanges)
        {
            return trackChanges ? await Entity.ToListAsync() : await Entity.AsNoTracking().ToListAsync();
        }
        public async Task<List<T>> FindAllAsyncWithEntities(bool trackChanges, params Expression<Func<T, object>>[] childrens)
        {
            IQueryable<T> query = Entity;

            // Dinamik olarak include işlemi yapıyoruz
            if (childrens.Any())
            {
                foreach (var includeExpression in childrens)
                {
                    query = query.Include(includeExpression); // Include edilen ilişkiler
                }
            }

            // Eğer tracking yapılacaksa
            if (trackChanges)
            {
                return await query.ToListAsync();
            }
            else
            {
                // Tracking yapılmayacaksa, AsNoTracking kullan
                return await query.AsNoTracking().ToListAsync();
            }
        }
        public async Task<ICollection<T>> FindAllByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            var query = Entity.Where(expression);
            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }
            return await query.ToListAsync();
        }
        public async Task<T?> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            IQueryable<T> query = Entity;
            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(expression);
        }
        public async Task<T> GetByIdAsync<TKey>(TKey id, bool trackChanges)
        {
            if (!PrimaryKeyCache.TryGetValue(typeof(T), out var keyProperty))
            {
                // Birincil anahtarın adını alıyoruz
                keyProperty = _context.Model
                                      .FindEntityType(typeof(T))
                                      ?.FindPrimaryKey()
                                      ?.Properties.FirstOrDefault()?.Name;

                if (string.IsNullOrEmpty(keyProperty))
                {
                    // Birincil anahtar tanımlanmadığı için hata fırlatıyoruz
                    throw new InvalidOperationException(OperationStatus.PrimaryKeyNotDefined);
                }

                PrimaryKeyCache[typeof(T)] = keyProperty;
            }
            // Türü belirleyip, doğru Property türünü kullanarak sorguyu çalıştırıyoruz
            if (typeof(TKey) == typeof(int))
            {
                return await Entity.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, keyProperty) == (int)(object)id);
            }
            else if (typeof(TKey) == typeof(short))
            {
                return await Entity.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<short>(e, keyProperty) == (short)(object)id);
            }
            else if (typeof(TKey) == typeof(long))
            {
                return await Entity.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<long>(e, keyProperty) == (long)(object)id);
            }
            else if (typeof(TKey) == typeof(Guid))
            {
                return await Entity.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<Guid>(e, keyProperty) == (Guid)(object)id);
            }
            else if (typeof(TKey) == typeof(string))
            {
                return await Entity.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<string>(e, keyProperty) == (string)(object)id);
            }
            else
            {
                throw new InvalidOperationException($"Desteklenmeyen tür: {typeof(TKey)}");
            }
        }
        public async Task<string> UpdateAsync(T entity)
        {
            try
            {
                FormatEntityStrings(entity); // Gerekli formatlama işlemleri
                Entity.Attach(entity); // Nesneyi bağla
                _context.Entry(entity).State = EntityState.Modified; // Güncelleme durumunu ayarla

                var affectedRows = await _context.SaveChangesAsync(); // Veritabanına değişiklikleri kaydet

                if (affectedRows > 0)
                {
                    return OperationStatus.Success; // İşlem başarılı
                }
                else
                {
                    return OperationStatus.GlobalError; // Satır etkilenmediği durumda hata
                }
            }
            catch (DbUpdateException)
            {
                // Veritabanı ile ilgili spesifik hata
                return OperationStatus.GlobalError;
            }
            catch (Exception)
            {
                // Genel hata durumu
                return OperationStatus.GlobalError;
            }
        }

        public bool IsAny(Expression<Func<T, bool>> predicate, short? excludeId = null)
        {
            var query = Entity.AsQueryable();

            // Birincil anahtarın adını cache'den al veya belirle
            if (!PrimaryKeyCache.TryGetValue(typeof(T), out var keyProperty))
            {
                keyProperty = _context.Model
                                      .FindEntityType(typeof(T))
                                      ?.FindPrimaryKey()
                                      ?.Properties.FirstOrDefault()?.Name;

                if (keyProperty == null)
                    throw new InvalidOperationException($"'{typeof(T).Name}' için birincil anahtar bulunamadı!");

                PrimaryKeyCache[typeof(T)] = keyProperty; // Cache'e ekle
            }

            // excludeId varsa, bu kaydı sorgudan hariç tut
            if (excludeId.HasValue)
            {
                query = query.Where(x => !EF.Property<short>(x, keyProperty).Equals(excludeId.Value));
            }

            // Predicate'i uygula
            query = query.Where(predicate);

            // Herhangi bir sonuç var mı?
            return query.Any();
        }
        public async Task<AuditTrailDto?> GetAuditTrailWithDetailsAsync<TKey>(TKey id)
        {
            // PrimaryKey adını bul veya cache'den getir
            if (!PrimaryKeyCache.TryGetValue(typeof(T), out var keyProperty))
            {
                keyProperty = _context.Model
                                      .FindEntityType(typeof(T))
                                      ?.FindPrimaryKey()
                                      ?.Properties.FirstOrDefault()?.Name;

                if (string.IsNullOrEmpty(keyProperty))
                {
                    throw new InvalidOperationException(OperationStatus.PrimaryKeyNotDefined);
                }

                PrimaryKeyCache[typeof(T)] = keyProperty;
            }

            // Entity'yi ID'ye göre bul
            var entity = await FindEntityByIdAsync(id, keyProperty);

            if (entity == null) return null; // Eğer kayıt bulunamazsa

            // Safahat bilgilerini al
            var olusturanPersonelId = entity.GetType().GetProperty(nameof(AuditTrailDto.OlusturanPersonelId))?.GetValue(entity) as short?;
            var guncelleyenPersonelId = entity.GetType().GetProperty(nameof(AuditTrailDto.GuncelleyenPersonelId))?.GetValue(entity) as short?;
            var silenPersonelId = entity.GetType().GetProperty(nameof(AuditTrailDto.SilenPersonelId))?.GetValue(entity) as short?;

            // Personel bilgilerini toplu olarak al
            var personelIds = new List<short?> { olusturanPersonelId, guncelleyenPersonelId, silenPersonelId }
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .Distinct()
                .ToList();

            var personeller = await _context.Personel
                .AsNoTracking()
                .Where(p => personelIds.Contains(p.PersonelID))
                .ToDictionaryAsync(p => p.PersonelID, p => new { p.Ad, p.Soyad });

            var olusturanPersonel = olusturanPersonelId.HasValue && personeller.ContainsKey(olusturanPersonelId.Value)
                ? personeller[olusturanPersonelId.Value]
                : null;

            var guncelleyenPersonel = guncelleyenPersonelId.HasValue && personeller.ContainsKey(guncelleyenPersonelId.Value)
                ? personeller[guncelleyenPersonelId.Value]
                : null;

            var silenPersonel = silenPersonelId.HasValue && personeller.ContainsKey(silenPersonelId.Value)
                ? personeller[silenPersonelId.Value]
                : null;

            // Audit trail DTO'sunu oluştur
            var auditTrail = new AuditTrailDto
            {
                Id = id,
                OlusturanPersonelId = olusturanPersonelId,
                OlusturanAdSoyad = olusturanPersonel != null ? $"{olusturanPersonel.Ad} {olusturanPersonel.Soyad}" : null,
                OlusturulmaTarihi = entity.GetType().GetProperty(nameof(AuditTrailDto.OlusturulmaTarihi))?.GetValue(entity) as DateTime? ?? default,
                GuncelleyenPersonelId = guncelleyenPersonelId,
                GuncelleyenAdSoyad = guncelleyenPersonel != null ? $"{guncelleyenPersonel.Ad} {guncelleyenPersonel.Soyad}" : null,
                GuncellenmeTarihi = entity.GetType().GetProperty(nameof(AuditTrailDto.GuncellenmeTarihi))?.GetValue(entity) as DateTime? ?? default,
                SilenPersonelId = silenPersonelId,
                SilenAdSoyad = silenPersonel != null ? $"{silenPersonel.Ad} {silenPersonel.Soyad}" : null,
                SilinmeTarihi = entity.GetType().GetProperty(nameof(AuditTrailDto.SilinmeTarihi))?.GetValue(entity) as DateTime? ?? default,
            };
            return auditTrail;
        }
    }
}
