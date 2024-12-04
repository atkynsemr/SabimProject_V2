using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.KabinetBazliBolumDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class KabinetBazliBolumController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public KabinetBazliBolumController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _manager.KabinetBazliBolumService.TFindAllAsyncWithEntities(false,e => e.Kisims, equals => equals.Durum );
            var KabinetBazliBolumler = _mapper.Map<List<ResultKabinetBazliBolumWithKisimCount>>(result);
            return View(KabinetBazliBolumler);
        }
    }
}
