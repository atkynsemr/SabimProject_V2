using Sabim.Domain.DTOs.HelperDtos;
using Sabim.Domain.DTOs.PersonelDtos;

namespace Sabim.Web.ViewModels
{
    public class PersonnelHistoryViewModel
    {
        public ResultPersonelDto ListelePersonel { get; set; }
        public List<PersonnelHistoryDto> PersonelSafahatBilgi{ get; set; }
    }
}
