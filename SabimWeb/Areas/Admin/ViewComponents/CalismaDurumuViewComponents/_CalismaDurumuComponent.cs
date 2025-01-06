using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.CalismaDurumuViewComponents
{
    public class _CalismaDurumuComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public _CalismaDurumuComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var calismaDurumlari = await _manager.CalismaDurumuService.TGetAllCalismaDurumuWithPersonelCountAsync(false);
            return View(calismaDurumlari);
        }
    }
}
