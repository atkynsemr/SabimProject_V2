using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.MalzemeDurumuDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.MalzemeDurumuViewComponents
{
    public class _MalzemeDurumuComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public _MalzemeDurumuComponent(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var malzemeDurumlari = await _manager.MalzemeDurumuService.TFindAllAsyncWithEntities(false,x=>x.Durum);
            var malzemeDurumlariDto = _mapper.Map<List<ResultMalzemeDurumuDto>>(malzemeDurumlari);
            return View(malzemeDurumlariDto);
        }
    }
}
