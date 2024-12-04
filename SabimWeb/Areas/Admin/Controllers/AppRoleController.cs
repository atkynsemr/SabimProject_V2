using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.AppRoleDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class AppRoleController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public AppRoleController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _manager.AppRoleService.TFindAllAsyncWithEntities(false, e => e.Durum);
            var roller = _mapper.Map<List<ResultAppRoleWithPersonelCountDto>>(result);
            return View(roller);
        }
    }
}
