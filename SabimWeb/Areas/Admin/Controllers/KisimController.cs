using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.KisimDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = nameof(Admin))]
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
            //var result = await _manager.KisimService.TFindAllAsyncWithEntities(false, e => e.Kisim, e => e.PersonelGorevlendirilmes,  e => e.Durum);
            //var kisimlar = _mapper.Map<List<ResultKisimWithPersonelCountDto>>(result);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EkleKisim([FromForm] CreateKisimDto createKisimDto)
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
            createKisimDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createKisimDto.OlusturulmaTarihi = DateTime.Now;
            var kisim = _mapper.Map<Kisim>(createKisimDto);
            var status = await _manager.KisimService.TAddAsync(kisim);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni kısım başarılı bir şekilde eklenmiştir." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Kisim - Action: EkleKisim - User: {username} - Hata: Yeni Kısım Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.KisimService.TGetAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:Kisim - Action: GetirSafahatBilgi - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
    }
}
