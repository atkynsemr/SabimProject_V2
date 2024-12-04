using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class KadroTuruController : Controller
    {
        private readonly IServiceManager _manager;
        public KadroTuruController(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IActionResult> Index()
        {
            var kadroTurleri = await _manager.KadroTuruService.TGetAllKadroTuruWithPersonelCountAsync(false);
            return View(kadroTurleri);
        }
    }
}
