using Sabim.Domain.DTOs.AppUserDtos;
using Sabim.Domain.DTOs.PersonelDtos;

namespace Sabim.Web.ViewModels
{
    public class PersonelViewModel
    {
        public CreatePersonelDto YeniPersonel { get; set; }
        public UpdatePersonelDto GuncellePersonel { get; set; }
        public CreateUserDto YeniKullanici { get; set; }
    }
}
