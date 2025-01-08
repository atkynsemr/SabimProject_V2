using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.BolumViewComponent
{
    public class _BolumComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        public _BolumComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var bolumler = await _manager.BolumService.TGetAllBolumWithBirimCountAsync(false);
            return View(bolumler);
        }
    }
}
