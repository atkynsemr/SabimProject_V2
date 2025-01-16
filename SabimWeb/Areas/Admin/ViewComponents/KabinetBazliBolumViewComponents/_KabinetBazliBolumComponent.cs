using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.KabinetBazliBolumViewComponents
{
    public class _KabinetBazliBolumComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        public _KabinetBazliBolumComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var kabinetBazliBolum = await _manager.KabinetBazliBolumService.TGetAllKabinetBazliBolumWithKisimCountAsync(false);
            return View(kabinetBazliBolum);
        }
    }
}
