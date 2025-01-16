using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.UnvanDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.UnvanViewComponent
{
    public class _UnvanGenelComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        public _UnvanGenelComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? selectedUnvanId = null)
        {
            var unvanlar = await _manager.UnvanService.TFindAllAsync(false);
            // Alfabetik sıralama
            var unvanDtoList = unvanlar
                .OrderBy(u => u.UnvanAdi)
                .Select(u => new ResultUnvanDto
                {
                    UnvanID = u.UnvanID,
                    UnvanAdi = u.UnvanAdi,
                    OncelikSirasi = u.OncelikSirasi,
                    Selected = selectedUnvanId.HasValue && selectedUnvanId.Value == u.UnvanID
                }).ToList();
            return View(unvanDtoList);
        }
    }
}
