using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.MalzemeMarkaDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.MalzemeMarkaViewComponents
{
    public class _MalzemeMarkaGenelComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public _MalzemeMarkaGenelComponent(IServiceManager manager)
        {
            _manager = manager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? deger, int? selectedMalzemeMarkaId = null)
        {
            var malzemeMarkalari = await _manager.MalzemeMarkaService.TFindAllAsync(false);
            ViewBag.Deger = deger;
            var malzemeMarkaDtoList = malzemeMarkalari.Select(k => new ResultMalzemeMarkaDto
            {
                MalzemeMarkaID = k.MalzemeMarkaID,
                MarkaAdi = k.MarkaAdi,
                Selected = selectedMalzemeMarkaId.HasValue && selectedMalzemeMarkaId.Value == k.MalzemeMarkaID
            }).ToList();
            return View(malzemeMarkaDtoList);
        }
    }
}
