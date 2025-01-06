using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.SehirViewComponents
{
    public class _SehirListesiComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        public _SehirListesiComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sehirler = await _manager.SehirService.TGetAllSehirWithKurumCountAsync(false);
            return View(sehirler);
        }
    }
}
