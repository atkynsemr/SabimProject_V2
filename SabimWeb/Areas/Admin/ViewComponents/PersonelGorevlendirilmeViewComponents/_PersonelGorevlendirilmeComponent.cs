using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.PersonelGorevlendirilmeDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.PersonelGorevlendirilmeViewComponents
{
    public class _PersonelGorevlendirilmeComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public _PersonelGorevlendirilmeComponent(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync(short personelID, string? deger)
        {
            var personelGorevleri = await _manager.PersonelGorevlendirilmeService.TFindByIdAsyncWithEntities(false,x => x.PersonelId== personelID, x=>x.Durum, x => x.Kisim,x => x.Kisim.Birim, x=>x.Kisim.Birim.Bolum);
            var personelGorevleriDto = _mapper.Map<List<ResultPersonelGorevlendirilmeDto>>(personelGorevleri);
            return View(personelGorevleriDto);
        }
    }
}
