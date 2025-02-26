using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.DurumDtos;
using Sabim.Domain.DTOs.GorevlendirilmeTipiDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.GorevlendirilmeTipiViewComponents
{
    public class _GorevlendirilmeTipiComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public _GorevlendirilmeTipiComponent(IServiceManager manager, IMapper mapper = null)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _manager.GorevlendirilmeTipiService.TFindAllAsyncWithEntities(false, x=>x.Durum);
            var gorevlendirilmeTipiDto = _mapper.Map<List<ResultGorevlendirilmeTipiDto>>(result);
            return View(gorevlendirilmeTipiDto);
        }
    }
}
