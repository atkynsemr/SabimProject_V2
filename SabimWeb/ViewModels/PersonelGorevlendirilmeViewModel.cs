using Sabim.Domain.DTOs.PersonelDtos;
using Sabim.Domain.DTOs.PersonelGorevlendirilmeDtos;

namespace Sabim.Web.ViewModels
{
    public class PersonelGorevlendirilmeViewModel
    {
        public CreatePersonelGorevlendirilmeDto YeniPersonelGorevlendirilme { get; set; }
        public UpdatePersonelGorevlendirilmeDto GuncellePersonelGorevlendirilme { get; set; }
        public ResultPersonelWithGorevYeriDto ListelePersonelBilgileri { get; set; }
    }
}


