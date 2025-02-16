using Sabim.Domain.DTOs.PersonelDtos;
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
    }
}
