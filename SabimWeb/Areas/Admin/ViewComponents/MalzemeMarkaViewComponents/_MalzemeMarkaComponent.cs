using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.MalzemeMarkaDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.MalzemeMarkaViewComponents
{
    public class _MalzemeMarkaComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public _MalzemeMarkaComponent(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var malzemeMarkalar = await _manager.MalzemeMarkaService.TFindAllAsyncWithEntities(false, x => x.Durum);
            var malzemeMarkalariDto = _mapper.Map<List<ResultMalzemeMarkaDto>>(malzemeMarkalar);
            return View(malzemeMarkalariDto);
        }
    }
}
