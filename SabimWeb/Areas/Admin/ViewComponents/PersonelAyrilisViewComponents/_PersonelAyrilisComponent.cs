using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.PersonelAyrilisViewComponents
{
    public class _PersonelAyrilisComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        public _PersonelAyrilisComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync(short PersonelID,bool kaliciAyrilisMi, string? deger)
        {
            var personelIzinleri = await _manager.PersonelService.TGetPersonelIzinleriByIdAsync(PersonelID, kaliciAyrilisMi, false);
            return View(personelIzinleri);
        }
    }
}
