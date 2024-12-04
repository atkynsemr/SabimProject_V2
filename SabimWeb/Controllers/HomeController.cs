using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;
using Sabim.Web.Filters;
using SabimWeb.Models;
using System.Diagnostics;

namespace SabimWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceManager _serviceManager;

        public HomeController(ILogger<HomeController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }
        [Authorize(Policy = "ClaimBasedPolicy")]
        public IActionResult Index()
        {
           // var result = _serviceManager.CalismaDurumuService.TFindAll(false);
            return View();
        }
        [Authorize(Policy = "ClaimBasedPolicy")]
        public IActionResult Privacy()
        {
          //  var result = _serviceManager.CalismaDurumuService.TFindAllByConditionAsync(c => c.DurumId == 1, false);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
