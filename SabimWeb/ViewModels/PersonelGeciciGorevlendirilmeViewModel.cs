using Sabim.Domain.DTOs.PersonelDtos;
using Sabim.Domain.DTOs.PersonelGeciciGorevlendirilmeDtos;

namespace Sabim.Web.ViewModels
{
    public class PersonelGeciciGorevlendirilmeViewModel
    {
        public CreatePersonelGeciciGorevlendirilmeDto YeniPersonelGeciciGorevlendirilme { get; set; }
        public UpdatePersonelGeciciGorevlendirilmeDto GuncellePersonelGeciciGorevlendirilme { get; set; }
        public ResultPersonelDto ListelePersonelGeciciGorevlendirilme { get; set; }
    }
}
