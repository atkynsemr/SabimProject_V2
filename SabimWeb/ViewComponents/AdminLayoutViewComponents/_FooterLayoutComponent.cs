using Microsoft.AspNetCore.Mvc;

namespace Sabim.Web.ViewComponents.AdminLayoutViewComponents
{
    public class _FooterLayoutComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
