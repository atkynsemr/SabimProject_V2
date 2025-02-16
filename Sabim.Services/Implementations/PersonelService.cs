using AutoMapper;
using Sabim.Domain.DTOs.PersonelDtos;
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
    }
}
