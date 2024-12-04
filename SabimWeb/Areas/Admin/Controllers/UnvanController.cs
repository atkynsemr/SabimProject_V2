using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class UnvanController : Controller
    {

        private readonly IServiceManager _manager;
        public UnvanController(IServiceManager manager)
        {
            _manager = manager;
        }

        public async Task<IActionResult> Index()
        {
            var unvanlar = await _manager.UnvanService.TGetAllUnvanWithPersonelCountAsync(false);
            return View(unvanlar);
        }
    }
}
