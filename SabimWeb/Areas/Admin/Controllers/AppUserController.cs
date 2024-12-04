using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.AppRoleDtos;
using Sabim.Domain.DTOs.AppUserDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class AppUserController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public AppUserController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _manager.AppUserService.TFindAllAsyncWithEntities(false, e => e.Durum, e => e.Personel);
            var appUser = _mapper.Map<List<ResultAppUserDto>>(result);
            return View(appUser);
        }
    }
}
