using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.MalzemeModelDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.MalzemeModelViewComponents
{
    public class _MalzemeModelComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public _MalzemeModelComponent(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var malzemeModeller = await _manager.MalzemeModelService.TFindAllAsyncWithEntities(false, x => x.Durum, x=>x.MalzemeMarka, x=> x.MalzemeCinsi);
            var malzemeModelleriDto = _mapper.Map<List<ResultMalzemeModelDto>>(malzemeModeller);
            return View(malzemeModelleriDto);
        }
    }

}
