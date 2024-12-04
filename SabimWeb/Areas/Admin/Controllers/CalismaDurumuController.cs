using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class CalismaDurumuController : Controller
    {
        private readonly IServiceManager _manager;

        public CalismaDurumuController(IServiceManager manager)
        {
            _manager = manager;
        }

        public async Task<IActionResult> Index()
        {
            var calismaDurumlari = await _manager.CalismaDurumuService.TGetAllCalismaDurumuWithPersonelCountAsync(false);
            return View(calismaDurumlari);
        }
    }
}
