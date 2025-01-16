using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.GorevlendirilmeTuruDtos;
using Sabim.Domain.DTOs.SehirDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.GorevlendirilmeTuruViewComponents
{
    public class _GorevlendirilmeTuruGenelComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public _GorevlendirilmeTuruGenelComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? selectedGorevlendirilmeTuruId = null)
        {
            var gorevlendirilmeTurleri = await _manager.GorevlendirilmeTuruService.TFindAllAsync(false);
            // Alfabetik sıralama
            var gorevlendirilmeTuruDtoList = gorevlendirilmeTurleri
                .OrderBy(g => g.GorevlendirilmeTuruAdi)
                .Select(g => new ResultGorevlendirilmeTuruDto
                {
                    GorevlendirilmeTuruID = g.GorevlendirilmeTuruID,
                    GorevlendirilmeTuruAdi = g.GorevlendirilmeTuruAdi,
                    Selected = selectedGorevlendirilmeTuruId.HasValue && selectedGorevlendirilmeTuruId.Value == g.GorevlendirilmeTuruID
                }).ToList();
            return View(gorevlendirilmeTuruDtoList);
        }
    }
}
