using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.UnvanViewComponent
{
    public class _UnvanComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        public _UnvanComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var unvanlar = await _manager.UnvanService.TGetAllUnvanWithPersonelCountAsync(false);
            return View(unvanlar);
        }
    }
}
