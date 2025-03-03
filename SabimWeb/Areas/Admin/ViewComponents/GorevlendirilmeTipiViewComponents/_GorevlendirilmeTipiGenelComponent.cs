using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.GorevlendirilmeTipiDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.GorevlendirilmeTipiViewComponents
{
    public class _GorevlendirilmeTipiGenelComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        public _GorevlendirilmeTipiGenelComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync(string? deger, int? selectedGorevlendirilmeTipiId = null)
        {
            var gorevlendirilmeTipleri = await _manager.GorevlendirilmeTipiService.TFindAllAsync(false);
            ViewBag.Deger = deger;
            var GorevlendirilmeTipiDtoList = gorevlendirilmeTipleri
                .OrderBy(g => g.GorevlendirilmeTipiAciklama)
                .Select(g => new ResultGorevlendirilmeTipiDto
                {
                    GorevlendirilmeTipiID = g.GorevlendirilmeTipiID,
                    GorevlendirilmeTipiAciklama = g.GorevlendirilmeTipiAciklama,
                    Selected = selectedGorevlendirilmeTipiId.HasValue && selectedGorevlendirilmeTipiId.Value == g.GorevlendirilmeTipiID
                }).ToList();
            return View(GorevlendirilmeTipiDtoList);
        }
    }
}
