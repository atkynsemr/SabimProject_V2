using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.BirimDtos;
using Sabim.Domain.DTOs.PersonelDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = nameof(Admin))]
    public class PersonelController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public PersonelController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetirKurumlar(int id)
        {
            return ViewComponent("_KurumGenelComponent", new { kurumTipiId = 1, sehirId = id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EklePersonel([FromForm] CreatePersonelDto createPersonelDto)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            if (!ModelState.IsValid)
            {
                var errors = ValidationHelper.GetModelErrors(ModelState);
                return Json(new { success = false, errors });
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            createPersonelDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createPersonelDto.OlusturulmaTarihi = DateTime.Now;
            var Personel = _mapper.Map<Personel>(createPersonelDto);
            var status = await _manager.PersonelService.TAddAsync(Personel);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni Personel başarılı bir şekilde eklenmiştir." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Personel - Action: EklePersonel - User: {username} - Hata: Yeni Personel Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.PersonelService.TGetAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:Personel - Action: GetirSafahatBilgi - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
    }
}

