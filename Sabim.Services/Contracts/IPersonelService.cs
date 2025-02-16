using Sabim.Domain.DTOs.PersonelDtos;
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
    }
}
