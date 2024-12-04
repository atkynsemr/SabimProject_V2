using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.SidebarMenuDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class SidebarMenuController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public SidebarMenuController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _manager.SidebarMenuService.TFindAllAsyncWithEntities(false, e => e.Durum, e => e.Ekrans);
            var sidebarMenuler = _mapper.Map<List<ResultSidebarMenuWithEkranCountDto>>(result);
            return View(sidebarMenuler);
        }
    }
}
