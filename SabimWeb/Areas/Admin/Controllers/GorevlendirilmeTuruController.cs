using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class GorevlendirilmeTuruController : Controller
    {
        private readonly IServiceManager _manager;

        public GorevlendirilmeTuruController(IServiceManager manager)
        {
            _manager = manager;
        }

        public async Task<IActionResult> Index()
        {
            var gorevlendirilmeTurleri = await _manager.GorevlendirilmeTuruService.TGetAllGorevlendirilmeTurWithPersonelCountAsync(false);
            return View(gorevlendirilmeTurleri);
        }
    }
}
