using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.KadroTuruViewComponents
{
    public class _KadroTuruComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        public _KadroTuruComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var kadroTurleri = await _manager.KadroTuruService.TGetAllKadroTuruWithPersonelCountAsync(false);
            return View(kadroTurleri);
        }
    }
}
