using Microsoft.AspNetCore.Mvc;

namespace Sabim.Web.Areas.BilgiIslem.Controllers
{
    [Area(nameof(BilgiIslem))]
    public class TaleplerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
