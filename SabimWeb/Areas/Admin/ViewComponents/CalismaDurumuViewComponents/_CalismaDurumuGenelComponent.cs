using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.CalismaDurumuDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.CalismaDurumuViewComponents
{
    public class _CalismaDurumuGenelComponent :ViewComponent
    {
        private readonly IServiceManager _manager;

        public _CalismaDurumuGenelComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync(string? deger, int? selectedCalismaDurumId = null)
        {
            var calismaDurumlari = await _manager.CalismaDurumuService.TFindAllAsync(false);
            ViewBag.Deger = deger;
            var calismaDurumuDtoList = calismaDurumlari
                .OrderBy(c => c.CalismaDurumAdi)
                .Select(c => new ResultCalismaDurumuDto
                {
                    CalismaDurumuID = c.CalismaDurumuID,
                    CalismaDurumAdi = c.CalismaDurumAdi,
                    Selected = selectedCalismaDurumId.HasValue && selectedCalismaDurumId.Value == c.CalismaDurumuID
                })
                .ToList();
            return View(calismaDurumuDtoList);
        }
    }
}
