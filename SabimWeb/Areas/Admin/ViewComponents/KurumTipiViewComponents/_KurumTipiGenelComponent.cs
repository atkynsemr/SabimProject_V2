using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.KurumTipiDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.KurumTipiViewComponents
{
    public class _KurumTipiGenelComponent:ViewComponent
    {
        private readonly IServiceManager _manager;

        public _KurumTipiGenelComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync(string? deger, int? selectedKurumTipiId = null)
        {
            var kurumTipleri = await _manager.KurumTipiService.TFindAllAsync(false);
            ViewBag.Deger = deger;
            var kurumTipiDtoList = kurumTipleri.Select(k => new ResultKurumTipiDto
            {
               KurumTipiID = k.KurumTipiID,
               KurumTipiAdi = k.KurumTipiAdi,
               Selected = selectedKurumTipiId.HasValue && selectedKurumTipiId.Value == k.KurumTipiID
            }).ToList();
            return View(kurumTipiDtoList);
        }
    }
}

