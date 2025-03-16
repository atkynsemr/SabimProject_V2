using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.MalzemeCinsiDtos;
using Sabim.Domain.DTOs.MalzemeMarkaDtos;
using Sabim.Domain.Entities;
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

        public async Task<IViewComponentResult> InvokeAsync(string? deger, int? selectedMalzemeMarkaId = null, int? malzemeCinsiId = null)
        {
            var malzemeMarkalariQuery = _manager.MalzemeMarkaService.
                TFindAllByConditionAsync(x => (!malzemeCinsiId.HasValue || x.MalzemeCinsiId == malzemeCinsiId.Value),false);
            ViewBag.Deger = deger;
            var malzemeMarkaModelleri = await malzemeMarkalariQuery;
            var malzemeMarkaDtoList = malzemeMarkaModelleri
                .OrderBy(k => k.MarkaAdi)
                .Select(k => new ResultMalzemeMarkaDto
                {
                    MalzemeMarkaID = k.MalzemeMarkaID,
                    MarkaAdi = k.MarkaAdi,
                    Selected = selectedMalzemeMarkaId == k.MalzemeMarkaID
                })
                .ToList();
            return View(malzemeMarkaDtoList);
        }
    }
}
