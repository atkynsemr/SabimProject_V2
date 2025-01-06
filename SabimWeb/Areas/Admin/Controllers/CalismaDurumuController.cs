using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.CalismaDurumuDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = nameof(Admin))]
    public class CalismaDurumuController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public CalismaDurumuController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EkleCalismaDurumu([FromForm] CreateCalismaDurumuDto createCalismaDurumuDto)
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
            createCalismaDurumuDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createCalismaDurumuDto.OlusturulmaTarihi = DateTime.Now;
            var calismaDurumu = _mapper.Map<CalismaDurumu>(createCalismaDurumuDto);
            var status = await _manager.CalismaDurumuService.TAddAsync(calismaDurumu);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni çalışma durumu başarılı bir şekilde eklenmiştir." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:CalismaDurumu - Action: EkleCalismaDurumu - User: {username} - Hata: Yeni Çalışma Durumu Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCalismaDurumuById([FromRoute] int id)
        {
            try
            {
                var calismaDurumu = await _manager.CalismaDurumuService.TGetByIdAsync(id, false);
                if (calismaDurumu == null)
                {
                    return Json(new { success = false });
                }

                return Json(new { success = true, data = calismaDurumu });

            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:CalismaDurumu - Action: GetCalismaDurumuById - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.CalismaDurumuService.TGetAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:CalismaDurumu - Action: GetirSafahatBilgi - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncelleCalismaDurumu([FromForm] UpdateCalismaDurumuDto updateCalismaDurumuDto)
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
            var mevcutCalismaDurumu = await _manager.CalismaDurumuService.TGetByIdAsync(updateCalismaDurumuDto.CalismaDurumuID, false);
            if (mevcutCalismaDurumu == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }
            _mapper.Map(updateCalismaDurumuDto, mevcutCalismaDurumu);
            mevcutCalismaDurumu.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutCalismaDurumu.GuncellenmeTarihi = DateTime.Now;
            var status = await _manager.CalismaDurumuService.TUpdateAsync(mevcutCalismaDurumu);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Çalışma durumu başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:CalismaDurumu - Action: GuncelleCalismaDurumu - User: {username} - Hata: Çalışma Durum ID : {updateCalismaDurumuDto.CalismaDurumuID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SilCalismaDurumu(short calismaDurumuID)
        {
            var status = _manager.CalismaDurumuService.TDeleteAsync(calismaDurumuID).Result;
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Çalışma durumu başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu çalışma durumu başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:CalismaDurumu - Action: SilCalismaDurumu - User: {username} - Hata: Çalışma Durum ID : {calismaDurumuID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
    }
}
