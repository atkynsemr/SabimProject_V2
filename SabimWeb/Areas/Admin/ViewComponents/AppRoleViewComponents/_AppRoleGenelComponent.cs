using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.AppRoleDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.AppRoleGenelViewComponents
{
    public class _AppRoleGenelComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public _AppRoleGenelComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync(string? deger, int? selectedId = null)
        {
            var appRoller = await _manager.AppRoleService.TFindAllAsync(false);
            ViewBag.Deger = deger;
            var appRollerDtoList = appRoller
                .OrderBy(r => r.Name)
                .Select(r => new ResultAppRoleDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Selected = selectedId.HasValue && selectedId.Value == r.Id
                }).ToList();
            return View(appRollerDtoList);
        }
    }
}
