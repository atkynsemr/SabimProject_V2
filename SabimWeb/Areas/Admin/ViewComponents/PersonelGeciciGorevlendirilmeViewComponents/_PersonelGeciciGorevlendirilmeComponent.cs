using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.PersonelGeciciGorevlendirilmeViewComponents
{
    public class _PersonelGeciciGorevlendirilmeComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        public _PersonelGeciciGorevlendirilmeComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync(short PersonelID, string? deger)
        {
            var personelIzinleri = await _manager.PersonelService.TGetPersonelGeciciGorevlendirilmeByIdAsync(PersonelID, false);
            return View(personelIzinleri);
        }
    }
}
