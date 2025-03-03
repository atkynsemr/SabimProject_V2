using Sabim.Domain.DTOs.HelperDtos;
using Sabim.Domain.DTOs.PersonelAyrilisDtos;
using Sabim.Domain.DTOs.PersonelDtos;
using Sabim.Domain.DTOs.PersonelGeciciGorevlendirilmeDtos;
using Sabim.Domain.DTOs.PersonelGorevlendirilmeDtos;
using Sabim.Domain.DTOs.PersonelUnvanGecmisiDtos;
using Sabim.Domain.DTOs.PersonelWithUserDto;
using Sabim.Domain.DTOs.SavciCalisilanKatipDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.Contracts
{
    public interface IPersonelService : IGenericService<Personel>
    {
        Task<string> TAddPersonelWithUserAsync(CreatePersonelWithUserDto dto);
        Task<ResultPersonelDto> TGetPersonelByIdAsync(int id, bool trackChanges);
        Task<string> TUpdateCalisilanKatipler(UpdateSavciCalisilanKatipDto updateSavciCalisilanKatipler);
        Task<string> TAddPersonelUnvanAsync(CreatePersonelUnvanGecmisiDto createPersonelUnvanGecmisiDto);
        Task<List<ResultPersonelUnvanGecmisiDto>> TGetPersonelUnvanlariByIdAsync(short personelId, bool trackChanges);
        Task<ResultPersonelUnvanGecmisiDto> TGetPersonelUnvanByIdAsync(short personelUnvanGecmisiId, bool trackChanges);
        Task<PersonelUnvanGecmisi> TGetPersonelUnvanGecmisiByIdAsync(short personelUnvanGecmisiId, bool trackChanges);
        Task<string> TUpdatePersonelUnvanGecmisiAsync(PersonelUnvanGecmisi personelUnvanGecmisi);
        Task<string> TDeletePersonelUnvanAsync(short PersonelUnvanGecmisiID);
        Task<AuditTrailDto?> TGetPersonelUnvanAuditTrailWithDetailsAsync(short id);
        Task<List<ResultPersonelAyrilisDto>> TGetPersonelIzinleriByIdAsync(short personelId, bool kaliciAyrilisMi, bool trackChanges);
        Task<List<ResultPersonelGeciciGorevlendirilmeDto>> TGetPersonelGeciciGorevlendirilmeByIdAsync(short personelId, bool trackChanges);
        Task<string> TAddPersonelIzinleriAsync(CreatePersonelAyrilisDto createPersonelAyrilisDto);
        Task<PersonelAyrilis> TGetPersonelAyrilisById(short personelAyrilisId, bool trackChanges);
        Task<string> TUpdatePersonelIzinAsync(PersonelAyrilis updatePersonelAyrilis);
        Task<string> TDeletePersonelIzinAsync(short personelAyrilisID);
        Task<AuditTrailDto?> TGetPersonelIzinAuditTrailWithDetailsAsync(short id);
        Task<ResultPersonelWithGorevYeriDto> TGetByIdWithPersonelInfoAsync(short personelId, bool trankChanges);
    }
}
