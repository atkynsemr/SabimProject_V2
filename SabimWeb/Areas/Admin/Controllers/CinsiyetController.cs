using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class CinsiyetController : Controller
    {
        private readonly IServiceManager _manager;
        public CinsiyetController(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IActionResult> Index()
        {
            var cinsiyetler = await _manager.CinsiyetService.TGetAllCinsiyetWithPersonelCountAsync(false);
            return View(cinsiyetler);
        }
    }
}
