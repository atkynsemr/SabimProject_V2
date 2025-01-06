using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.CinsiyetViewComponents
{
    public class _CinsiyetListesiComponent: ViewComponent
    {
        private readonly IServiceManager _manager;
        public _CinsiyetListesiComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cinsiyetler = await _manager.CinsiyetService.TGetAllCinsiyetWithPersonelCountAsync(false);
            return View(cinsiyetler);
        }
    }
}
