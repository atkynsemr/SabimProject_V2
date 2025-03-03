using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.KisimDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.KisimViewComponents
{
    public class _KisimGenelComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public _KisimGenelComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync(string? deger, int? selectedBirimId = null, int? selectedKisimId = null)
        {
            // Veritabanından sadece gerekli verileri çek
            var kisimlar = selectedBirimId.HasValue
                ? await _manager.KisimService.TFindAllByConditionAsync(c => c.BirimId == selectedBirimId.Value, false)
                : await _manager.KisimService.TFindAllAsync(false);

            // ViewBag'e değer ataması
            ViewBag.Deger = deger;

            // KisimDto listesini oluştur ve selectedKisimId'ye göre seçili durumunu belirle
            var kisimDtoList = kisimlar
                .OrderBy(c => c.KisimAdi)
                .Select(c => new ResultKisimDto
                {
                    BirimId = c.BirimId,
                    KisimID = c.KisimID,
                    KisimAdi = c.KisimAdi,
                    Selected = selectedKisimId.HasValue && selectedKisimId.Value == c.KisimID
                })
                .ToList();

            return View(kisimDtoList);
        }
    }
}
