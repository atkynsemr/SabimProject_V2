using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.BirimDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class BirimController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public BirimController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result =await _manager.BirimService.TFindAllAsyncWithEntities(false, e=>e.Durum, e=>e.Bolum, e=>e.Kisims);
            var birimler = _mapper.Map<List<ResultBirimWithKisimCount>>(result);
            return View(birimler);
        }
    }
}
