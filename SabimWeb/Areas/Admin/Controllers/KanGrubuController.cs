using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class KanGrubuController : Controller
    {
        private readonly IServiceManager _manager;

        public KanGrubuController(IServiceManager manager)
        {
            _manager = manager;
        }

        public async Task<IActionResult> Index()
        {
            var kanGrublari = await _manager.KanGrubuService.TGetAllKanGrubuWithPersonelCountAsync(false);
            return View(kanGrublari);
        }
    }
}
