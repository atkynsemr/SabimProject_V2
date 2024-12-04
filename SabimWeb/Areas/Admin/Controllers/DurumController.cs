using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.DurumDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class DurumController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public DurumController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _manager.DurumService.TFindAllAsync(false);
            var durumlar = _mapper.Map<List<ResultDurumDto>>(result);
            return View(durumlar);
 
        }
    }
}
