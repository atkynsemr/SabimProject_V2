using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.CinsiyetDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.CinsiyetViewComponents
{
    public class _CinsiyetListesiGenelComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public _CinsiyetListesiGenelComponent(IServiceManager manager)
        {
            _manager = manager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? deger, int? selectedCinsiyetId = null)
        {
            var cinsiyetler = await _manager.CinsiyetService.TFindAllAsync(false);
            ViewBag.Deger= deger;
            // Alfabetik sıralama
            var cinsiyetDtoList = cinsiyetler
                .OrderBy(c => c.CinsiyetAdi)
                .Select(c => new ResultCinsiyetDto
                {
                    CinsiyetID = c.CinsiyetID,
                    CinsiyetAdi = c.CinsiyetAdi,
                    Selected = selectedCinsiyetId.HasValue && selectedCinsiyetId.Value == c.CinsiyetID
                })
                .ToList();
            return View(cinsiyetDtoList);
        }
    }
}