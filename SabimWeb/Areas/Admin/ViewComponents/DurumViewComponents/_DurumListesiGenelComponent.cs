using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.DurumDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.DurumViewComponents
{
    public class _DurumListesiGenelComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public _DurumListesiGenelComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync(string? deger, int? selectedDurumId = null)
        {
            var durumlar = await _manager.DurumService.TFindAllAsync(false);
            ViewBag.Deger = deger;
            var durumDtoList = durumlar.Select(d => new ResultDurumDto
            {
                DurumID = d.DurumID,
                DurumAdi = d.DurumAdi,
                Selected = selectedDurumId.HasValue && selectedDurumId.Value == d.DurumID
            }).ToList();
            return View(durumDtoList);
        }
    }
}
