using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.SehirDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.SehirViewComponents
{
    public class _SehirListesiGenelComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public _SehirListesiGenelComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? selectedSehirId = null)
        {
            var sehirler = await _manager.SehirService.TFindAllAsync(false);
            // Alfabetik sıralama
            var sehirDtoList = sehirler
                .OrderBy(s => s.SehirAdi)
                .Select(s => new ResultSehirDto
                {
                    SehirID = s.SehirID,
                    SehirAdi = s.SehirAdi,
                    Selected = selectedSehirId.HasValue && selectedSehirId.Value == s.SehirID
                })
                .ToList();
            return View(sehirDtoList);
        }
    }
}
