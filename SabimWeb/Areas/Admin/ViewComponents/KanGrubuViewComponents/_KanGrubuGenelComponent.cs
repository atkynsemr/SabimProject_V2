using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.KanGrubuDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.KanGrubuViewComponents
{
    public class _KanGrubuGenelComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        public _KanGrubuGenelComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? selectedKanGrubuId = null)
        {
            var kanGruplari = await _manager.KanGrubuService.TFindAllAsync(false);
            // Alfabetik sıralama
            var kanGruplariDtoList = kanGruplari
                .OrderBy(k => k.KanGrubuAdi)
                .Select(k => new ResultKanGrubuDto
                {
                    KanGrubuID = k.KanGrubuID,
                    KanGrubuAdi = k.KanGrubuAdi,
                    Selected = selectedKanGrubuId.HasValue && selectedKanGrubuId.Value == k.KanGrubuID
                }).ToList();
            return View(kanGruplariDtoList);
        }
    }
}