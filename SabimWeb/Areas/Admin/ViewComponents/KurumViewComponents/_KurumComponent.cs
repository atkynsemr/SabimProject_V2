using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.KurumViewComponents
{
    public class _KurumComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public _KurumComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var kurumlar = await _manager.KurumService.TGetAllKurumWithPersonelCountAsync(false);
            return View(kurumlar);
        }
    }
}
