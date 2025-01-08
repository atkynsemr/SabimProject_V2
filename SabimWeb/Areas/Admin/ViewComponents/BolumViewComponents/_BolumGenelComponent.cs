using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.BolumDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.BolumViewComponent
{
    public class _BolumGenelComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        public _BolumGenelComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? selectedBolumId = null)
        {
            var bolumler = await _manager.BolumService.TFindAllAsync(false);
            // Alfabetik sıralama
            var bolumDtoList = bolumler
                .OrderBy(b => b.BolumAdi)
                .Select(b => new ResultBolumDto
                {
                    BolumID = b.BolumID,
                    BolumAdi = b.BolumAdi,
                    Selected = selectedBolumId.HasValue && selectedBolumId.Value == b.BolumID
                })
                .ToList();
            return View(bolumDtoList);
        }
    }
}
