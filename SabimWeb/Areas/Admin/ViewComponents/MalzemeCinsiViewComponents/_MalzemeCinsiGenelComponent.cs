using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.MalzemeCinsiDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.MalzemeCinsiViewComponents
{
    public class _MalzemeCinsiGenelComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public _MalzemeCinsiGenelComponent(IServiceManager manager)
        {
            _manager = manager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? deger, int? selectedMalzemeCinsiId = null, int? malzemeTuruId = null)
        {
            //var malzemeCinsilari = await _manager.MalzemeCinsiService.TFindAllAsync(false);
            //ViewBag.Deger = deger;
            //var malzemeCinsiDtoList = malzemeCinsilari.Select(k => new ResultMalzemeCinsiDto
            //{
            //    MalzemeCinsiID = k.MalzemeCinsiID,
            //    MalzemeCinsiAdi = k.MalzemeCinsiAdi,
            //    Selected = selectedMalzemeCinsiId.HasValue && selectedMalzemeCinsiId.Value == k.MalzemeCinsiID
            //}).ToList();
            //return View(malzemeCinsiDtoList);

             var malzemeCinsileriQuery = _manager.MalzemeCinsiService.TFindAllByConditionAsync(
                x => (!malzemeTuruId.HasValue || x.MalzemeTuruId == malzemeTuruId.Value),
                false);
            ViewBag.Deger = deger;
            var malzemeCinsiModelleri = await malzemeCinsileriQuery;
            var malzemeModelDtoList = malzemeCinsiModelleri
                .OrderBy(k => k.MalzemeCinsiAdi)
                .Select(k => new ResultMalzemeCinsiDto
                {
                    MalzemeCinsiID = k.MalzemeCinsiID,
                    MalzemeCinsiAdi = k.MalzemeCinsiAdi,
                    Selected = selectedMalzemeCinsiId == k.MalzemeCinsiID
                })
                .ToList();
            return View(malzemeModelDtoList);
        }
    }
}
