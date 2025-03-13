using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.MalzemeCinsiDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.MalzemeCinsiViewComponents
{
    public class _MalzemeCinsiComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public _MalzemeCinsiComponent(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var malzemeCinsi = await _manager.MalzemeCinsiService.TFindAllAsyncWithEntities(false, x => x.Durum, x=>x.MalzemeTuru);
            var malzemeCinsiDto = _mapper.Map<List<ResultMalzemeCinsiDto>>(malzemeCinsi);
            return View(malzemeCinsiDto);
        }
    }
}
