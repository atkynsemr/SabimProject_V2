using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.KabinetBazliBolumDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.KabinetBazliBolumViewComponents
{
    public class _KabinetBazliBolumGenelComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public _KabinetBazliBolumGenelComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? selectedKabinetBazliBolumId = null)
        {
            var kabinetBazliBolumler = await _manager.KabinetBazliBolumService.TFindAllAsync(false);
            // Alfabetik sıralama
            var kabinetBazliBolumlerDtoList = kabinetBazliBolumler
                .OrderBy(k => k.KabinetBazliBolumAdi)
                .Select(k => new ResultKabinetBazliBolumDto
                {
                    KabinetBazliBolumID = k.KabinetBazliBolumID,
                    KabinetBazliBolumAdi = k.KabinetBazliBolumAdi,
                    Selected = selectedKabinetBazliBolumId.HasValue && selectedKabinetBazliBolumId.Value == k.KabinetBazliBolumID
                })
                .ToList();
            return View(kabinetBazliBolumlerDtoList);
        }
    }
}
