using Sabim.Domain.DTOs.PersonelDtos;
using Sabim.Domain.DTOs.PersonelUnvanGecmisiDtos;

namespace Sabim.Web.ViewModels
{
    public class PersonelUnvanGecmisiViewModel
    {
        public CreatePersonelUnvanGecmisiDto YeniPersonelUnvan { get; set; }
        public UpdatePersonelUnvanGecmisiDto GuncellePersonelUnvan { get; set; }
        public ResultPersonelDto ListelePersonelUnvan { get; set; }
    }
}
