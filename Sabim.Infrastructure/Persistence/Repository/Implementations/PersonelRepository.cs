using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.HelperDtos;
using Sabim.Domain.DTOs.PersonelAyrilisDtos;
using Sabim.Domain.DTOs.PersonelDtos;
using Sabim.Domain.DTOs.PersonelUnvanGecmisiDtos;
using Sabim.Domain.DTOs.SavciCalisilanKatipDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class PersonelRepository : RepositoryBase<Personel>, IPersonelRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public PersonelRepository(SabimDbContext context, UserManager<AppUser> userManager, IMapper mapper) : base(context)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<string> AddPersonelIzinleriAsync(CreatePersonelAyrilisDto createPersonelAyrilisDto)
        {
            try
            {
                // Çakışma kontrolü: Aynı personelin tarihleri çakışan bir kaydı var mı?
                bool hasConflict = await _context.PersonelAyrilis.AnyAsync(izin =>
                    izin.PersonelId == createPersonelAyrilisDto.PersonelId &&
                    (
                        // 1️⃣ Yeni izin başlangıç tarihi mevcut bir izin aralığına denk geliyorsa
                        (createPersonelAyrilisDto.BaslangicTarihi.HasValue &&
                         izin.BaslangicTarihi.HasValue &&
                         createPersonelAyrilisDto.BaslangicTarihi >= izin.BaslangicTarihi &&
                         createPersonelAyrilisDto.BaslangicTarihi <= izin.BitisTarihi) ||

                        // 2️⃣ Yeni izin bitiş tarihi mevcut bir izin aralığına denk geliyorsa
                        (createPersonelAyrilisDto.BitisTarihi.HasValue &&
                         izin.BitisTarihi.HasValue &&
                         createPersonelAyrilisDto.BitisTarihi >= izin.BaslangicTarihi &&
                         createPersonelAyrilisDto.BitisTarihi <= izin.BitisTarihi) ||

                        // 3️⃣ Yeni izin, mevcut bir izni tamamen kapsıyorsa
                        (createPersonelAyrilisDto.BaslangicTarihi.HasValue &&
                         createPersonelAyrilisDto.BitisTarihi.HasValue &&
                         createPersonelAyrilisDto.BaslangicTarihi <= izin.BaslangicTarihi &&
                         createPersonelAyrilisDto.BitisTarihi >= izin.BitisTarihi) ||

                        // 4️⃣ Sadece başlangıç tarihinin birebir aynı olması durumu
                        (createPersonelAyrilisDto.BaslangicTarihi.HasValue &&
                         izin.BaslangicTarihi.HasValue &&
                         createPersonelAyrilisDto.BaslangicTarihi == izin.BaslangicTarihi) ||

                        // 5️⃣ Sadece bitiş tarihinin birebir aynı olması durumu
                        (createPersonelAyrilisDto.BitisTarihi.HasValue &&
                         izin.BitisTarihi.HasValue &&
                         createPersonelAyrilisDto.BitisTarihi == izin.BitisTarihi)
                    )
                );

                if (hasConflict)
                {
                    return OperationStatus.DateConflict; // Çakışma varsa işlemi durdur
                }

                // DTO'dan Entity'ye dönüşüm
                var personelIzin = _mapper.Map<PersonelAyrilis>(createPersonelAyrilisDto);
                await _context.PersonelAyrilis.AddAsync(personelIzin);

                // Personel bilgisini çek
                var personel = await _context.Personel
                    .SingleOrDefaultAsync(p => p.PersonelID == createPersonelAyrilisDto.PersonelId);

                // Eğer personel bulunduysa çalışma durumunu güncelle
                if (personel != null)
                {
                    personel.CalismaDurumuId = (short)(createPersonelAyrilisDto.KaliciAyrilisMi ? 3 : 2);
                }

                // Tüm değişiklikleri tek seferde kaydet
                int affectedRows = await _context.SaveChangesAsync();
                return affectedRows > 0 ? OperationStatus.Success : OperationStatus.Incomplete;
            }
            catch (DbUpdateException)
            {
                return OperationStatus.GlobalError; // Veritabanı hatası
            }
            catch (Exception)
            {
                return OperationStatus.GlobalError; // Genel hata
            }
        }
        public async Task<string> AddPersonelUnvanAsync(CreatePersonelUnvanGecmisiDto createPersonelUnvanGecmisiDto)
        {
            try
            {
                var personelUnvanGecmisi = _mapper.Map<PersonelUnvanGecmisi>(createPersonelUnvanGecmisiDto);
                await _context.PersonelUnvanGecmisi.AddAsync(personelUnvanGecmisi);
                int affectedRows = await _context.SaveChangesAsync();
                if (affectedRows > 0)
                {
                    var enSonUnvan = await _context.PersonelUnvanGecmisi.Where(p => p.PersonelId == createPersonelUnvanGecmisiDto.PersonelId && p.DurumId == 1)
                .OrderByDescending(p => p.UnvanaSahipOlduguTarih).FirstOrDefaultAsync();
                    if (enSonUnvan != null)
                    {
                        // Personel tablosundaki ilgili kaydı bulup, UnvanId'yi güncelleme
                        var personel = await _context.Personel.FirstOrDefaultAsync(p => p.PersonelID == createPersonelUnvanGecmisiDto.PersonelId);
                        if (personel != null)
                        {
                            personel.UnvanId = enSonUnvan.UnvanId;
                            await _context.SaveChangesAsync(); // Personel tablosunu güncelleme
                        }
                    }
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
        public async Task<string> AddPersonelWithUserAsync(Personel personel, List<short> SelectedPersonelIds, AppUser user, int roleId, string password)
        {
            try
            {
                await _context.Personel.AddAsync(personel);
                int affectedRows = await _context.SaveChangesAsync();
                if (affectedRows > 0)
                {
                    var createDto = new CreatePersonelUnvanGecmisiDto
                    {
                        PersonelId = personel.PersonelID,
                        UnvanId = personel.UnvanId,
                        UnvanaSahipOlduguTarih = personel.MeslegeGirisTarihi ?? DateTime.Now,
                        OlusturanPersonelId = personel.OlusturanPersonelId,
                        OlusturulmaTarihi = personel.OlusturulmaTarihi ?? DateTime.Now,
                        DurumId = 1
                    };
                    // AutoMapper ile DTO'yu Entity'ye dönüştür
                    var unvanGecmisiDto = _mapper.Map<PersonelUnvanGecmisi>(createDto);
                    // Unvan geçmişini ekleyip tekrar kaydediyoruz
                    await _context.PersonelUnvanGecmisi.AddAsync(unvanGecmisiDto);
                    await _context.SaveChangesAsync();
                    if (SelectedPersonelIds.Count > 0)
                    {
                        var createSavciKatipDtoList = SelectedPersonelIds.Select(katip => new CreateSavciCalisilanKatipDto
                        {
                            GorevlendirilmeAktifMi = true,
                            GorevlendirilmeBaslamaTarihi = DateTime.Now,
                            KatipId = katip,
                            SavciId = personel.PersonelID,
                            DurumId = 1,
                            OlusturanPersonelId = personel.OlusturanPersonelId,
                            OlusturulmaTarihi = personel.OlusturulmaTarihi ?? DateTime.Now
                        }).ToList();
                        var savciCalisilanKatipEntities = _mapper.Map<List<SavciCalisilanKatip>>(createSavciKatipDtoList);
                        await _context.SavciCalisilanKatip.AddRangeAsync(savciCalisilanKatipEntities);
                        await _context.SaveChangesAsync();
                    }
                }
                if (affectedRows > 0)
                {
                    user.PersonelId = personel.PersonelID;
                    var result = await _userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {

                        var userRole = new IdentityUserRole<int>
                        {
                            UserId = user.Id,
                            RoleId = roleId
                        };
                        await _context.UserRoles.AddAsync(userRole);
                        int affectedRoleRows = await _context.SaveChangesAsync();
                        if (affectedRoleRows > 0)
                        {
                            return OperationStatus.Success;
                        }
                        else
                        {
                            return OperationStatus.Incomplete;
                        }
                    }
                    else
                    {
                        return OperationStatus.Incomplete;
                    }
                }
                else
                {
                    return OperationStatus.GlobalError;
                }
            }
            catch
            {
                return OperationStatus.GlobalError;
            }
        }
        public async Task<string> DeletePersonelIzinAsync(short personelAyrilisID)
        {
            try
            {
                var personelIzin = await _context.PersonelAyrilis.FirstOrDefaultAsync(p => p.PersonelAyrilisID == personelAyrilisID);
                if (personelIzin == null)
                {
                    return OperationStatus.NotFound;
                }
                _context.PersonelAyrilis.Remove(personelIzin);
                int affectedRows = await _context.SaveChangesAsync();
                if (affectedRows > 0)
                {
                    var izinSayisi = await _context.PersonelAyrilis
                        .Where(p => p.PersonelId == personelIzin.PersonelId && p.DurumId == 1)
                        .CountAsync();

                    if (izinSayisi == 0)
                    {
                        var personel = await _context.Personel.SingleOrDefaultAsync(x => x.PersonelID == personelIzin.PersonelId);
                        if (personel == null)
                        {
                            return OperationStatus.NotFound; // Personel bulunamadı
                        }

                        personel.CalismaDurumuId = 1;
                        int changedRows = await _context.SaveChangesAsync();
                        if (changedRows > 0)
                        {
                            return OperationStatus.Success; // İşlem başarılı
                        }
                        else
                        {
                            return OperationStatus.Incomplete;
                        }
                    }
                    return OperationStatus.Success; // İşlem başarılı
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
        public async Task<string> DeletePersonelUnvanAsync(short PersonelUnvanGecmisiID)
        {
            try
            {
                var personelUnvan = await _context.PersonelUnvanGecmisi
                    .FirstOrDefaultAsync(p => p.PersonelUnvanGecmisiID == PersonelUnvanGecmisiID);
                if (personelUnvan == null)
                {
                    return OperationStatus.NotFound;
                }
                _context.PersonelUnvanGecmisi.Remove(personelUnvan);
                int affectedRows = await _context.SaveChangesAsync();
                if (affectedRows > 0)
                {
                    return OperationStatus.Success;
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
        public async Task<PersonelAyrilis> GetPersonelAyrilisById(short personelAyrilisId, bool trackChanges)
        {
            IQueryable<PersonelAyrilis> query = _context.PersonelAyrilis.Where(x => x.PersonelAyrilisID == personelAyrilisId)
                .Include(x => x.PersonelAyrilisNedenleri);
            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(); // Filtrelenmiş query'de ilk kaydı getir
        }
        public async Task<ResultPersonelDto> GetPersonelByIdAsync(int id, bool trackChanges)
        {
            IQueryable<Personel> query = _context.Personel;
            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }
            var result = await query.ProjectTo<ResultPersonelDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.PersonelID == id);
            return result;
        }

        public async Task<AuditTrailDto?> GetPersonelIzinAuditTrailWithDetailsAsync(short id)
        {
            // Entity'yi bul
            var entity = await _context.PersonelAyrilis
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PersonelAyrilisID == id);

            if (entity == null) return null; // Eğer kayıt bulunamazsa

            // Safahat bilgilerini al
            short? olusturanPersonelId = entity.OlusturanPersonelId;
            short? guncelleyenPersonelId = entity.GuncelleyenPersonelId;
            short? silenPersonelId = entity.SilenPersonelId;

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
                OlusturulmaTarihi = entity.OlusturulmaTarihi ?? default,
                GuncelleyenPersonelId = guncelleyenPersonelId,
                GuncelleyenAdSoyad = guncelleyenPersonel != null ? $"{guncelleyenPersonel.Ad} {guncelleyenPersonel.Soyad}" : null,
                GuncellenmeTarihi = entity.GuncellenmeTarihi ?? default,
                SilenPersonelId = silenPersonelId,
                SilenAdSoyad = silenPersonel != null ? $"{silenPersonel.Ad} {silenPersonel.Soyad}" : null,
                SilinmeTarihi = entity.SilinmeTarihi ?? default,
            };

            return auditTrail;
        }

        public async Task<List<ResultPersonelAyrilisDto>> GetPersonelIzinleriByIdAsync(short personelId, bool kaliciAyrilisMi, bool trackChanges)
        {
            IQueryable<PersonelAyrilis> query = _context.PersonelAyrilis;
            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }
            var result = await query
                .Where(p => p.PersonelId == personelId && p.PersonelAyrilisNedenleri.KaliciAyrilisMi==kaliciAyrilisMi)
                .ProjectTo<ResultPersonelAyrilisDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.BaslangicTarihi)
                .ToListAsync();
            return result;
        }
        public async Task<AuditTrailDto?> GetPersonelUnvanAuditTrailWithDetailsAsync(short id)
        {
            // Entity'yi bul
            var entity = await _context.PersonelUnvanGecmisi
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PersonelUnvanGecmisiID == id);

            if (entity == null) return null; // Eğer kayıt bulunamazsa

            // Safahat bilgilerini al
            short? olusturanPersonelId = entity.OlusturanPersonelId;
            short? guncelleyenPersonelId = entity.GuncelleyenPersonelId;
            short? silenPersonelId = entity.SilenPersonelId;

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
                OlusturulmaTarihi = entity.OlusturulmaTarihi ?? default,
                GuncelleyenPersonelId = guncelleyenPersonelId,
                GuncelleyenAdSoyad = guncelleyenPersonel != null ? $"{guncelleyenPersonel.Ad} {guncelleyenPersonel.Soyad}" : null,
                GuncellenmeTarihi = entity.GuncellenmeTarihi ?? default,
                SilenPersonelId = silenPersonelId,
                SilenAdSoyad = silenPersonel != null ? $"{silenPersonel.Ad} {silenPersonel.Soyad}" : null,
                SilinmeTarihi = entity.SilinmeTarihi ?? default,
            };

            return auditTrail;
        }
        public async Task<ResultPersonelUnvanGecmisiDto> GetPersonelUnvanByIdAsync(short personelUnvanGecmisiId, bool trackChanges)
        {
            IQueryable<PersonelUnvanGecmisi> query = _context.PersonelUnvanGecmisi;
            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }
            var result = await query.ProjectTo<ResultPersonelUnvanGecmisiDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.PersonelUnvanGecmisiID == personelUnvanGecmisiId);
            return result;
        }
        public async Task<PersonelUnvanGecmisi> GetPersonelUnvanGecmisiByIdAsync(short personelUnvanGecmisiId, bool trackChanges)
        {
            IQueryable<PersonelUnvanGecmisi> query = _context.PersonelUnvanGecmisi
                .Where(x => x.PersonelUnvanGecmisiID == personelUnvanGecmisiId); // Önce filtreleme yapılmalı

            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(); // Filtrelenmiş query'de ilk kaydı getir
        }
        public async Task<List<ResultPersonelUnvanGecmisiDto>> GetPersonelUnvanlariByIdAsync(short personelId, bool trackChanges)
        {
            IQueryable<PersonelUnvanGecmisi> query = _context.PersonelUnvanGecmisi;

            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }
            var result = await query
                .Where(p => p.PersonelId == personelId)
                .ProjectTo<ResultPersonelUnvanGecmisiDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.UnvanaSahipOlduguTarih)
                .ToListAsync();

            return result;
        }
        public async Task<string> UpdateCalisilanKatipler(UpdateSavciCalisilanKatipDto updateSavciCalisilanKatipler)
        {
            try
            {
                var mevcutKatipler = await _context.SavciCalisilanKatip.Where(x => x.SavciId == updateSavciCalisilanKatipler.SavciId).ToListAsync();
                var yeniEklenenKatipler = updateSavciCalisilanKatipler.SelectedPersonelIds.Except(mevcutKatipler.Select(x => x.KatipId)).ToList();
                var silinecekKatipler = mevcutKatipler.Where(x => !updateSavciCalisilanKatipler.SelectedPersonelIds.Contains(x.KatipId)).ToList();
                if (silinecekKatipler.Any())
                {
                    _context.SavciCalisilanKatip.RemoveRange(silinecekKatipler);
                }
                if (yeniEklenenKatipler.Any())
                {
                    var eklenenKatipEntities = yeniEklenenKatipler.Select(katipId => new SavciCalisilanKatip
                    {
                        GorevlendirilmeAktifMi = true,
                        GorevlendirilmeBaslamaTarihi = DateTime.Now,
                        KatipId = katipId,
                        SavciId = updateSavciCalisilanKatipler.SavciId,
                        DurumId = 1,
                        OlusturanPersonelId = updateSavciCalisilanKatipler.OlusturanPersonelId,
                        OlusturulmaTarihi = updateSavciCalisilanKatipler.OlusturulmaTarihi
                    }).ToList();
                    await _context.SavciCalisilanKatip.AddRangeAsync(eklenenKatipEntities);
                }
                int affectedRoleRows = await _context.SaveChangesAsync();
                if (affectedRoleRows > 0)
                {
                    return OperationStatus.Success;
                }
                else
                {
                    return OperationStatus.Incomplete;
                }
            }
            catch
            {
                return OperationStatus.GlobalError;
            }
        }
        public async Task<string> UpdatePersonelIzinAsync(PersonelAyrilis updatePersonelAyrilis)
        {
            try
            {
                _context.PersonelAyrilis.Update(updatePersonelAyrilis);
                int affectedRows = await _context.SaveChangesAsync();
                if (affectedRows > 0)
                {
                    var izinSayisi = await _context.PersonelAyrilis
                        .Where(p => p.PersonelId == updatePersonelAyrilis.PersonelId && p.DurumId == 1)
                        .CountAsync();

                    if (izinSayisi == 0)
                    {
                        var personel = await _context.Personel.SingleOrDefaultAsync(x => x.PersonelID == updatePersonelAyrilis.PersonelId);
                        if (personel == null)
                        {
                            return OperationStatus.NotFound; // Personel bulunamadı
                        }

                        personel.CalismaDurumuId = 1;
                        int changedRows = await _context.SaveChangesAsync();
                        if (changedRows > 0)
                        {
                            return OperationStatus.Success; // İşlem başarılı
                        }
                        else
                        {
                            return OperationStatus.Incomplete;
                        }
                    }
                    return OperationStatus.Success; // İşlem başarılı
                }
                return OperationStatus.GlobalError; // Eğer affectedRows > 0 değilse

            }
            catch (DbUpdateException ex)
            {
                return OperationStatus.GlobalError;
            }
            catch (Exception ex)
            {
                return OperationStatus.GlobalError;
            }
        }
        public async Task<string> UpdatePersonelUnvanGecmisiAsync(PersonelUnvanGecmisi personelUnvanGecmisi)
        {
            try
            {
                // Veritabanında değişiklikleri kaydet
                _context.PersonelUnvanGecmisi.Update(personelUnvanGecmisi);
                int affectedRows = await _context.SaveChangesAsync();
                if (affectedRows > 0)
                {
                    var enSonUnvan = await _context.PersonelUnvanGecmisi.Where(p => p.PersonelId == personelUnvanGecmisi.PersonelId && p.DurumId == 1)
                .OrderByDescending(p => p.UnvanaSahipOlduguTarih).FirstOrDefaultAsync();
                    if (enSonUnvan != null)
                    {
                        // Personel tablosundaki ilgili kaydı bulup, UnvanId'yi güncelleme
                        var personel = await _context.Personel.FirstOrDefaultAsync(p => p.PersonelID == personelUnvanGecmisi.PersonelId);
                        if (personel != null)
                        {
                            personel.UnvanId = enSonUnvan.UnvanId;
                            await _context.SaveChangesAsync(); // Personel tablosunu güncelleme
                        }
                    }
                    return OperationStatus.Success; // İşlem başarılı
                }
                else
                {
                    return OperationStatus.GlobalError; // Satır eklenmediği takdirde hata
                }
            }
            catch (DbUpdateException ex)
            {
                return OperationStatus.GlobalError;
            }
            catch (Exception ex)
            {
                return OperationStatus.GlobalError;
            }
        }
    }
}
