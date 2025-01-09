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
        public async Task<IViewComponentResult> InvokeAsync(int? selectedBirimId = null)
        {
            var birimler = await _manager.BirimService.TFindAllAsync(false);
            // Alfabetik sıralama
            var birimDtoList = birimler
                .OrderBy(b => b.BirimAdi)
                .Select(b => new ResultBirimDto
                {
                    BirimID = b.BirimID,
                    BirimAdi = b.BirimAdi,
                    Selected = selectedBirimId.HasValue && selectedBirimId.Value == b.BirimID
                })
                .ToList();
            return View(birimDtoList);
        }
    }
}
