using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.KurumDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.KurumViewComponents
{
    public class _KurumGenelComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public _KurumGenelComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync(string? deger, int? selectedKurumId = null, int? kurumTipiId = null, int? sehirId = null)
        {
            var kurumlarQuery = _manager.KurumService.TFindAllByConditionAsync(
                x => (!kurumTipiId.HasValue || x.KurumTipiId == kurumTipiId.Value) &&
                     (!sehirId.HasValue || x.SehirId == sehirId.Value),
                false);
            ViewBag.Deger = deger;
            var kurumlar = await kurumlarQuery;
            var kurumDtoList = kurumlar
                .OrderBy(k => k.KurumAdi)
                .Select(k => new ResultKurumDto
                {
                    KurumID = k.KurumID,
                    KurumAdi = k.KurumAdi,
                    Selected = selectedKurumId.HasValue && selectedKurumId.Value == k.KurumID
                })
                .ToList();
            return View(kurumDtoList);
        }
    }
}
