using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.DurumDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.ViewComponents.DurumViewComponents
{
    public class _DurumListesiComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public _DurumListesiComponent(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _manager.DurumService.TFindAllAsync(false);
            var durumlar = _mapper.Map<List<ResultDurumDto>>(result);
            return View(durumlar);
        }
    }
}
