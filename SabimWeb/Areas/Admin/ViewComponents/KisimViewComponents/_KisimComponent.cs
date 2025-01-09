using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.KisimViewComponents
{
    public class _KisimComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public _KisimComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var kisimlar = await _manager.KisimService.TGetAllKisimWithPersonelCountAsync(false);
            return View(kisimlar);
        }
    }
}
