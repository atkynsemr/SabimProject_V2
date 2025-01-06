using Microsoft.AspNetCore.Mvc;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.KanGrubuViewComponents
{
    public class _KanGrubuComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        public _KanGrubuComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var kanGrublari = await _manager.KanGrubuService.TGetAllKanGrubuWithPersonelCountAsync(false);
            return View(kanGrublari);
        }
    }
}
