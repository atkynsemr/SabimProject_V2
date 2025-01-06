using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Policy = "ClaimBasedPolicy")]
    public class DashboardController : Controller
    {
        private readonly IServiceManager _manager;
        public DashboardController(IServiceManager manager)
        {
            _manager = manager;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
