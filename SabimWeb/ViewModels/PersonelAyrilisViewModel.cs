using Sabim.Domain.DTOs.PersonelAyrilisDtos;
using Sabim.Domain.DTOs.PersonelDtos;

namespace Sabim.Web.ViewModels
{
    public class PersonelAyrilisViewModel
    {
        public CreatePersonelAyrilisDto YeniPersonelAyrilis { get; set; }
        public UpdatePersonelAyrilisDto GuncellePersonelAyrilis { get; set; }
        public ResultPersonelDto ListelePersonelAyrilis { get; set; }
    }
}
