using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sabim.Domain.DTOs.PersonelDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.PersonelViewComponents
{
    public class _PersonelComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public _PersonelComponent(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var personeller = await _manager.PersonelService
                .TFindAll(false)
                .ProjectTo<ResultPersonelDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return View(personeller);
        }
    }
}
