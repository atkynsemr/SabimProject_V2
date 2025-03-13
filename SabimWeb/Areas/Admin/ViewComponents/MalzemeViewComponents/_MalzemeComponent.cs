using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.MalzemeDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.MalzemeViewComponents
{
    public class _MalzemeComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public _MalzemeComponent(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var malzeme = await _manager.MalzemeService.TFindAllAsyncWithEntities(false, x => x.Durum, x => x.MalzemeModel, x => x.MalzemeModel.MalzemeMarka, x => x.MalzemeModel.MalzemeCinsi, x => x.MalzemeModel.MalzemeCinsi.MalzemeTuru);
            var malzemeDto = _mapper.Map<List<ResultMalzemeDto>>(malzeme);
            return View(malzemeDto);
        }
    }
}
