using AutoMapper;
using Sabim.Domain.DTOs.HelperDtos;
using Sabim.Domain.DTOs.PersonelAyrilisDtos;
using Sabim.Domain.DTOs.PersonelDtos;
using Sabim.Domain.DTOs.PersonelGeciciGorevlendirilmeDtos;
using Sabim.Domain.DTOs.PersonelUnvanGecmisiDtos;
using Sabim.Domain.DTOs.PersonelWithUserDto;
using Sabim.Domain.DTOs.SavciCalisilanKatipDtos;
using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class PersonelService : GenericService<Personel>, IPersonelService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public PersonelService(IRepositoryBase<Personel> repository, IRepositoryManager repositoryManager, IMapper mapper) : base(repository)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public async Task<string> TAddPersonelUnvanAsync(CreatePersonelUnvanGecmisiDto createPersonelUnvanGecmisiDto)
        {
            return await _repositoryManager.Personel.AddPersonelUnvanAsync(createPersonelUnvanGecmisiDto);
        }
        public async Task<string> TAddPersonelWithUserAsync(CreatePersonelWithUserDto dto)
        {
            var user = _mapper.Map<AppUser>(dto.UserDto);
            return await _repositoryManager.Personel.AddPersonelWithUserAsync(dto.Personel,dto.SelectedPersonelIds, user, dto.UserDto.RoleId, dto.UserDto.Password);
        }
        public async Task<ResultPersonelDto> TGetPersonelByIdAsync(int id, bool trackChanges)
        {
            return await _repositoryManager.Personel.GetPersonelByIdAsync(id, trackChanges);
        }
        public async Task<List<ResultPersonelUnvanGecmisiDto>> TGetPersonelUnvanlariByIdAsync(short personelId, bool trackChanges)
        {
            return await _repositoryManager.Personel.GetPersonelUnvanlariByIdAsync(personelId, trackChanges);
        }
        public async Task<string> TUpdateCalisilanKatipler(UpdateSavciCalisilanKatipDto updateSavciCalisilanKatipler)
        {
            return await _repositoryManager.Personel.UpdateCalisilanKatipler(updateSavciCalisilanKatipler);
        }
        public async Task<PersonelUnvanGecmisi> TGetPersonelUnvanGecmisiByIdAsync(short personelUnvanGecmisiId, bool trackChanges)
        {
            return await _repositoryManager.Personel.GetPersonelUnvanGecmisiByIdAsync(personelUnvanGecmisiId, trackChanges);
        }
        public async Task<ResultPersonelUnvanGecmisiDto> TGetPersonelUnvanByIdAsync(short personelUnvanGecmisiId, bool trackChanges)
        {
            return await _repositoryManager.Personel.GetPersonelUnvanByIdAsync(personelUnvanGecmisiId, trackChanges);
        }
        public async Task<string> TUpdatePersonelUnvanGecmisiAsync(PersonelUnvanGecmisi personelUnvanGecmisi)
        {
            return await _repositoryManager.Personel.UpdatePersonelUnvanGecmisiAsync(personelUnvanGecmisi);
        }
        public async Task<string> TDeletePersonelUnvanAsync(short PersonelUnvanGecmisiID)
        {
            return await _repositoryManager.Personel.DeletePersonelUnvanAsync(PersonelUnvanGecmisiID);
        }
        public async Task<AuditTrailDto?> TGetPersonelUnvanAuditTrailWithDetailsAsync(short id)
        {
            return await _repositoryManager.Personel.GetPersonelUnvanAuditTrailWithDetailsAsync(id);
        }
        public async Task<List<ResultPersonelAyrilisDto>> TGetPersonelIzinleriByIdAsync(short personelId, bool kaliciAyrilisMi, bool trackChanges)
        {
            return await _repositoryManager.Personel.GetPersonelIzinleriByIdAsync(personelId, kaliciAyrilisMi, trackChanges);
        }
        public async Task<string> TAddPersonelIzinleriAsync(CreatePersonelAyrilisDto createPersonelAyrilisDto)
        {
            return await _repositoryManager.Personel.AddPersonelIzinleriAsync(createPersonelAyrilisDto);
        }
        public async Task<PersonelAyrilis> TGetPersonelAyrilisById(short personelAyrilisId, bool trackChanges)
        {
            return await _repositoryManager.Personel.GetPersonelAyrilisById(personelAyrilisId, trackChanges);
        }
        public async Task<string> TUpdatePersonelIzinAsync(PersonelAyrilis updatePersonelAyrilis)
        {
            return await _repositoryManager.Personel.UpdatePersonelIzinAsync(updatePersonelAyrilis);
        }
        public async Task<string> TDeletePersonelIzinAsync(short personelAyrilisID)
        {
            return await _repositoryManager.Personel.DeletePersonelIzinAsync(personelAyrilisID);
        }
        public async Task<AuditTrailDto?> TGetPersonelIzinAuditTrailWithDetailsAsync(short id)
        {
            return await _repositoryManager.Personel.GetPersonelIzinAuditTrailWithDetailsAsync(id);
        }

        public async Task<ResultPersonelWithGorevYeriDto> TGetByIdWithPersonelInfoAsync(short personelId, bool trankChanges)
        {
            return await _repositoryManager.Personel.GetByIdWithPersonelInfoAsync(personelId, trankChanges);
        }

        public async Task<List<ResultPersonelGeciciGorevlendirilmeDto>> TGetPersonelGeciciGorevlendirilmeByIdAsync(short personelId, bool trackChanges)
        {
            return await _repositoryManager.Personel.GetPersonelGeciciGorevlendirilmeByIdAsync(personelId, trackChanges); 
        }

        public async Task<string> TAddPersonelGeciciGorevlendirilmeAsync(CreatePersonelGeciciGorevlendirilmeDto createPersonelGeciciGorevlendirilmeDto)
        {
            return await _repositoryManager.Personel.AddPersonelGeciciGorevlendirilmeAsync(createPersonelGeciciGorevlendirilmeDto);
        }

        public async Task<PersonelGeciciGorevlendirilme> TGetPersonelGeciciGorevlendirilmeWithIdAsync(short personelGeciciGorevlendirilmeID, bool trackChanges)
        {
            return await _repositoryManager.Personel.GetPersonelGeciciGorevlendirilmeWithIdAsync(personelGeciciGorevlendirilmeID, trackChanges);
        }

        public async Task<string> TUpdatePersonelGeciciGorevlendirilmeAsync(PersonelGeciciGorevlendirilme updatePersonelGeciciGorevlendirilme)
        {
            return await _repositoryManager.Personel.UpdatePersonelGeciciGorevlendirilmeAsync(updatePersonelGeciciGorevlendirilme);
        }

        public async Task<string> TDeletePersonelGeciciGorevlendirilmeAsync(short personelGeciciGorevlendirilmeID)
        {
            return await _repositoryManager.Personel.DeletePersonelGeciciGorevlendirilmeAsync(personelGeciciGorevlendirilmeID);
        }

        public async Task<AuditTrailDto?> TGetGeciciGorevlendirilmeAuditTrailWithDetailsAsync(short id)
        {
            return await _repositoryManager.Personel.GetGeciciGorevlendirilmeAuditTrailWithDetailsAsync(id);
        }

        public async Task<List<PersonnelHistoryDto?>> TGetPersonnelHistoryAsync(short id)
        {
            return await _repositoryManager.Personel.GetPersonnelHistoryAsync(id);
        }
    }
}
