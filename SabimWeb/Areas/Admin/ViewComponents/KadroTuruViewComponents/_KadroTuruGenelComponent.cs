using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.KadroTuruDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.KadroTuruViewComponents
{
    public class _KadroTuruGenelComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public _KadroTuruGenelComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync(string? deger, int? selectedKadroTuruId = null)
        {
            var kadroTurleri = await _manager.KadroTuruService.TFindAllAsync(false);
            ViewBag.Deger = deger;
            var kadroTurleriDtoList = kadroTurleri
                .OrderBy(k => k.KadroTuruAdi)
                .Select(k => new ResultKadroTuruDto
                {
                    KadroTuruID = k.KadroTuruID,
                    KadroTuruAdi = k.KadroTuruAdi,
                    Selected = selectedKadroTuruId.HasValue && selectedKadroTuruId.Value == k.KadroTuruID
                }).ToList();
            return View(kadroTurleriDtoList);
        }
    }
}
