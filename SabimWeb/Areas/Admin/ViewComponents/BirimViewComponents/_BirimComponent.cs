using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.BirimViewComponents
{
    public class _BirimComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public _BirimComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var birimler = await _manager.BirimService.TGetAllBirimWithKisimCountAsync(false);
            return View(birimler);
        }
    }
}
