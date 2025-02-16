using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.PersonelUnvanGecmisiViewComponents
{
    public class _PersonelUnvanGecmisiComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        public _PersonelUnvanGecmisiComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync(short PersonelID, string? deger, int? selectedSehirId = null)
        {
            var personelUnvanGecmisi = await _manager.PersonelService.TGetPersonelUnvanlariByIdAsync(PersonelID, false);
            return View(personelUnvanGecmisi);
        }
    }
}
