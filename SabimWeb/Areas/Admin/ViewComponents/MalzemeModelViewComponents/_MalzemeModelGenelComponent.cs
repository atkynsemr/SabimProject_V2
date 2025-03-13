using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.MalzemeModelDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.MalzemeModelViewComponents
{
    public class _MalzemeModelGenelComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        public _MalzemeModelGenelComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync(string? deger, int? selectedMalzemeModelId = null, int? malzemeMarkaId = null)
        {
            var malzemeModelleriQuery = _manager.MalzemeModelService.TFindAllByConditionAsync(
                x => (!malzemeMarkaId.HasValue || x.MalzemeMarkaId == malzemeMarkaId.Value),
                false);
            ViewBag.Deger = deger;
            var malzemeModelleri = await malzemeModelleriQuery;
            var malzemeModelDtoList = malzemeModelleri
                .OrderBy(k => k.ModelAdi)
                .Select(k => new ResultMalzemeModelDto
                {
                    MalzemeModelID = k.MalzemeModelID,
                    ModelAdi = k.ModelAdi,
                    Selected = selectedMalzemeModelId == k.MalzemeModelID
                })
                .ToList();
            return View(malzemeModelDtoList);
        }
    }
}
