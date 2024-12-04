using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.KisimDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class KisimController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public KisimController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _manager.KisimService.TFindAllAsyncWithEntities(false, e => e.Birim, e => e.PersonelGorevlendirilmes,  e => e.Durum);
            var kisimlar = _mapper.Map<List<ResultKisimWithPersonelCountDto>>(result);
            return View(kisimlar);
        }
    }
}
