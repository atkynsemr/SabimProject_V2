using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.GorevlendirilmeTuruViewComponents
{
    public class _GorevlendirilmeTuruComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public _GorevlendirilmeTuruComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var gorevlendirilmeTurleri = await _manager.GorevlendirilmeTuruService.TGetAllGorevlendirilmeTurWithPersonelCountAsync(false);
            return View(gorevlendirilmeTurleri);
        }
    }
}
