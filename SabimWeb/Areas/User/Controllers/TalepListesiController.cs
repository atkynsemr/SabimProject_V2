using Microsoft.AspNetCore.Mvc;

namespace Sabim.Web.Areas.User.Controllers
{
    [Area(nameof(User))]
    public class TalepListesiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
