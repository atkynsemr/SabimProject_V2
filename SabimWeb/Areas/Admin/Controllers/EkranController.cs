using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.EkranDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class EkranController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public EkranController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _manager.EkranService.TFindAllAsyncWithEntities(false, e => e.Durum, e=> e.SidebarMenu);
            var ekranlar = _mapper.Map<List<ResultEkranDto>>(result);
            return View(ekranlar);
        }
    }
}
