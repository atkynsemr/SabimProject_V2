using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.HelperDtos;
using Sabim.Domain.DTOs.PersonelDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.PersonelSafahatBilgileriViewComponents
{
    public class _PersonelSafahatBilgileriComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public _PersonelSafahatBilgileriComponent(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync(short PersonelID)
        {
            var safahatBilgi = await _manager.PersonelService.TGetPersonnelHistoryAsync(PersonelID);
            return View(safahatBilgi);
        }
    }
}
