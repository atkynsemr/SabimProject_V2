using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class SehirController : Controller
    {
        private readonly IServiceManager _manager;

        public SehirController(IServiceManager manager)
        {
            _manager = manager;
        }

        public async Task<IActionResult> Index()
        {
            var sehirler = await _manager.SehirService.TGetAllSehirWithKurumCountAsync(false);
            return View(sehirler);
        }
    }
}
