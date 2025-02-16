using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.PersonelDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.PersonelViewComponents
{
    public class _PersonelListesiComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public _PersonelListesiComponent(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync(string? deger, int? selectedPersonelId = null)
        {
            var personelList = await _manager.PersonelService.TFindAllByConditionAsync(p => p.UnvanId == 10 && p.DurumId == 1, false);
            ViewBag.Deger = deger;
            var personeller = personelList
                    .OrderBy(p => p.Ad).ThenBy(p => p.Soyad)
                    .Select(p => new ListPersonelDto
                    {
                        PersonelID = p.PersonelID,
                        Ad = p.Ad,
                        Soyad = p.Soyad,
                        Selected = selectedPersonelId.HasValue && selectedPersonelId.Value == p.PersonelID
                    })
                    .ToList();
            return View(personeller);
        }
    }
}
