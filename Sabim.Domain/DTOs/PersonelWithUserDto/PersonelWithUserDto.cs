using Sabim.Domain.DTOs.AppUserDtos;
using Sabim.Domain.Entities;

namespace Sabim.Domain.DTOs.PersonelWithUserDto
{
    public class CreatePersonelWithUserDto
    {
        public Personel Personel { get; set; }
        public CreateUserDto UserDto { get; set; }
        public List<short> SelectedPersonelIds { get; set; }
    }
}
