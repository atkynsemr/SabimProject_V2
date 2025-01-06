using Microsoft.AspNetCore.Mvc;

namespace Sabim.Web.ViewComponents.AdminLayoutViewComponents
{
    public class _SidebarLayoutComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
