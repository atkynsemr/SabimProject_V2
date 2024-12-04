using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class KurumTipiController : Controller
    {
        private readonly IServiceManager _manager;

        public KurumTipiController(IServiceManager manager)
        {
            _manager = manager;
        }

        public async Task<IActionResult> Index()
        {
            var kurumTipleri = await _manager.KurumTipiService.TGetAllKurumTipiWithKurumCountAsync(false);
            return View(kurumTipleri);
        }
    }
}
