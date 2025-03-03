using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.BirimDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.BirimViewComponents
{
    public class _BirimGenelComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public _BirimGenelComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync(string? deger, int? selectedBolumId = null, int? selectedBirimId = null)
        {
            // Veritabanından sadece gerekli verileri çek
            var birimler = selectedBolumId.HasValue
                ? await _manager.BirimService.TFindAllByConditionAsync(c => c.BolumId == selectedBolumId.Value, false)
                : await _manager.BirimService.TFindAllAsync(false);
            ViewBag.Deger = deger;
            // Alfabetik sıralama
            var birimDtoList = birimler
                .OrderBy(b => b.BirimAdi)
                .Select(b => new ResultBirimDto
                {
                    BolumId = b.BolumId,
                    BirimID = b.BirimID,
                    BirimAdi = b.BirimAdi,
                    Selected = selectedBirimId.HasValue && selectedBirimId.Value == b.BirimID
                })
                .ToList();
            return View(birimDtoList);
        }
    }
}
