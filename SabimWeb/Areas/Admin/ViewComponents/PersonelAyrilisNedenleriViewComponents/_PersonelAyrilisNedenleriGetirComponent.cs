using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.PersonelAyrilisNedenleriDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.PersonelAyrilisNedenleriViewComponents
{
    public class _PersonelAyrilisNedenleriGetirComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public _PersonelAyrilisNedenleriGetirComponent(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var personelAyrilisNedenleri = await _manager.PersonelAyrilisNedenleriService.TFindAllAsyncWithEntities(false, x => x.Durum);
            var personelAyrilisNedenleriDto = _mapper.Map<List<ResultPersonelAyrilisNedenleriDto>>(personelAyrilisNedenleri); 
            return View(personelAyrilisNedenleriDto);
        }
    }
}
