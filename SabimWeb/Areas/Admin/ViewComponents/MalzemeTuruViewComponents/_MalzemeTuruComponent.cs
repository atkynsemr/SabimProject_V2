using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.MalzemeTuruDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.MalzemeTuruViewComponents
{
    public class _MalzemeTuruComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public _MalzemeTuruComponent(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var malzemeTurleri = await _manager.MalzemeTuruService.TFindAllAsyncWithEntities(false, x => x.Durum);
            var malzemeTurleriDto = _mapper.Map<List<ResultMalzemeTuruDto>>(malzemeTurleri);
            return View(malzemeTurleriDto);
        }
    }
}
