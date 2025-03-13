using Sabim.Domain.DTOs.HelperDtos;
using Sabim.Domain.DTOs.PersonelAyrilisDtos;
using Sabim.Domain.DTOs.PersonelDtos;
using Sabim.Domain.DTOs.PersonelGeciciGorevlendirilmeDtos;
using Sabim.Domain.DTOs.PersonelUnvanGecmisiDtos;
using Sabim.Domain.DTOs.SavciCalisilanKatipDtos;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Repository.Contracts
{
    public interface IPersonelRepository :IRepositoryBase<Personel>
    {
        Task<string> AddPersonelWithUserAsync(Personel personel, List<short> SelectedPersonelIds, AppUser user, int roleId, string password);
        Task<ResultPersonelDto> GetPersonelByIdAsync(int id, bool trackChanges);
        Task<string> UpdateCalisilanKatipler(UpdateSavciCalisilanKatipDto updateSavciCalisilanKatipler);
        Task<string> AddPersonelUnvanAsync(CreatePersonelUnvanGecmisiDto createPersonelUnvanGecmisiDto);
        Task<List<ResultPersonelUnvanGecmisiDto>> GetPersonelUnvanlariByIdAsync(short personelId, bool trackChanges);
        Task<ResultPersonelUnvanGecmisiDto> GetPersonelUnvanByIdAsync(short personelUnvanGecmisiId, bool trackChanges);
        Task<PersonelUnvanGecmisi> GetPersonelUnvanGecmisiByIdAsync(short personelUnvanGecmisiId, bool trackChanges);
        Task<string> UpdatePersonelUnvanGecmisiAsync(PersonelUnvanGecmisi personelUnvanGecmisi);
        Task<string> DeletePersonelUnvanAsync(short PersonelUnvanGecmisiID);
        Task<AuditTrailDto?> GetPersonelUnvanAuditTrailWithDetailsAsync(short id);
        Task<List<ResultPersonelAyrilisDto>> GetPersonelIzinleriByIdAsync(short personelId, bool kaliciAyrilisMi, bool trackChanges);
        Task<List<ResultPersonelGeciciGorevlendirilmeDto>> GetPersonelGeciciGorevlendirilmeByIdAsync(short personelId, bool trackChanges);
        Task<string> AddPersonelIzinleriAsync(CreatePersonelAyrilisDto createPersonelAyrilisDto);
        Task<PersonelAyrilis> GetPersonelAyrilisById(short personelAyrilisId, bool trackChanges);
        Task<string> UpdatePersonelIzinAsync(PersonelAyrilis updatePersonelAyrilis);
        Task<string> DeletePersonelIzinAsync(short personelAyrilisID);
        Task<AuditTrailDto?> GetPersonelIzinAuditTrailWithDetailsAsync(short id);
        Task<ResultPersonelWithGorevYeriDto> GetByIdWithPersonelInfoAsync(short personelId, bool trackChanges);
        Task<string> AddPersonelGeciciGorevlendirilmeAsync(CreatePersonelGeciciGorevlendirilmeDto createPersonelGeciciGorevlendirilmeDto);
        Task<PersonelGeciciGorevlendirilme> GetPersonelGeciciGorevlendirilmeWithIdAsync(short personelGeciciGorevlendirilmeID, bool trackChanges);
        Task<string> UpdatePersonelGeciciGorevlendirilmeAsync(PersonelGeciciGorevlendirilme updatePersonelGeciciGorevlendirilme);
        Task<string> DeletePersonelGeciciGorevlendirilmeAsync(short personelGeciciGorevlendirilmeID);
        Task<AuditTrailDto?> GetGeciciGorevlendirilmeAuditTrailWithDetailsAsync(short id);
        Task<List<PersonnelHistoryDto?>> GetPersonnelHistoryAsync(short id);
        Task<List<PersonnelTitleStatisticsDto?>> GetUnvanBazliVerilerAsync();
    }
}
