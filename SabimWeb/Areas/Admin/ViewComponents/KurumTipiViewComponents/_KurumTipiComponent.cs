using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.KurumTipiViewComponents
{
    public class _KurumTipiComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public _KurumTipiComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var kurumTipleri = await _manager.KurumTipiService.TGetAllKurumTipiWithKurumCountAsync(false);
            return View(kurumTipleri);
        }
    }
}
