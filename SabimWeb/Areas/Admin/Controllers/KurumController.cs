using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class KurumController : Controller
    {
        private readonly IServiceManager _manager;

        public KurumController(IServiceManager manager)
        {
            _manager = manager;
        }

        public async Task<IActionResult> Index()
        {
            var kurumlar = await _manager.KurumService.TGetAllKurumWithPersonelCountAsync(false); 
            return View(kurumlar);
        }
    }
}
