using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.BolumDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class BolumController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public BolumController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _manager.BolumService.TFindAllAsyncWithEntities(false, e => e.Durum, e => e.Birims);
            var bolumler = _mapper.Map<List<ResultBolumWithBirimCountDto>>(result);
            return View(bolumler);
        }
    }
}
