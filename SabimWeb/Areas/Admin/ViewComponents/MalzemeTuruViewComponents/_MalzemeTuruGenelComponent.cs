using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.MalzemeTuruDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.MalzemeTuruViewComponents
{
    public class _MalzemeTuruGenelComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public _MalzemeTuruGenelComponent(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync(string? deger, int? selectedMalzemeTuruId = null)
        {
            var malzemeTuru = await _manager.MalzemeTuruService.TFindAllAsync(false);
            ViewBag.Deger = deger;
            var malzemeTuruDtoList = malzemeTuru.Select(k => new ResultMalzemeTuruDto
            {
                MalzemeTuruID = k.MalzemeTuruID,
                TurAdi = k.TurAdi,
                Selected = selectedMalzemeTuruId.HasValue && selectedMalzemeTuruId.Value == k.MalzemeTuruID
            }).ToList();
            return View(malzemeTuruDtoList);
        }
    }
}
