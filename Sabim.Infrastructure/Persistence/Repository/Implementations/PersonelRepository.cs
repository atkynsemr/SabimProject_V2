using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sabim.Domain.Constants;
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
        public async Task<string> AddPersonelUnvanAsync(CreatePersonelUnvanGecmisiDto createPersonelUnvanGecmisiDto)
         {
            try
            {
                var personelUnvanGecmisi = _mapper.Map<PersonelUnvanGecmisi>(createPersonelUnvanGecmisiDto);
                await _context.PersonelUnvanGecmisi.AddAsync(personelUnvanGecmisi);
                int affectedRows = await _context.SaveChangesAsync();
                if (affectedRows > 0)
                {
                    var enSonUnvan = await _context.PersonelUnvanGecmisi.Where(p => p.PersonelId == createPersonelUnvanGecmisiDto.PersonelId)
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
    }
}
