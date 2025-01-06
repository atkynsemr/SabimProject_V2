using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.CinsiyetDtos;
using Sabim.Domain.DTOs.DurumDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = nameof(Admin))]
    public class DurumController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public DurumController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EkleDurum([FromForm] CreateDurumDto createDurumDto)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            if (!ModelState.IsValid)
                {
                    var errors = ValidationHelper.GetModelErrors(ModelState);
                    return BadRequest(errors);
                }
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                createDurumDto.OlusturanPersonelId = Convert.ToInt16(userId);
                createDurumDto.OlusturulmaTarihi = DateTime.Now;
                var durum = _mapper.Map<Durum>(createDurumDto);
                var status = await _manager.DurumService.TAddAsync(durum);
                switch (status)
                {
                    case OperationStatus.Success:
                        return Json(new { success = true, message = "Yeni durum başarılı bir şekilde eklenmiştir." });
                    case OperationStatus.GlobalError:
                    default:
                        var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                        _manager.LoggerService.LogError($"Area:Admin - Controller:Durum - Action: EkleDurum - User: {username} - Hata: Yeni Durum Eklenemedi!");
                        return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
                }
        }
        [HttpGet]
        public async Task<IActionResult> GetDurumById([FromRoute] int id)
        {
            try
            {
                var durum = await _manager.DurumService.TGetByIdAsync(id, false);
                if (durum == null)
                {
                    return Json(new { success = false });
                }
                return Json(new { success = true, data = durum });
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:Durum - Action: GetDurumById - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false});
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncelleDurum([FromForm] UpdateDurumDto updateDurumDto)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            if (!ModelState.IsValid)
                {
                    var errors = ValidationHelper.GetModelErrors(ModelState);
                    return BadRequest(errors);
                }
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var mevcutDurum = await _manager.DurumService.TGetByIdAsync(updateDurumDto.DurumID, false);
                if (mevcutDurum == null)
                {
                    return Json(new { success = false, message = "Kayıt Bulunamadı!" });
                }
                _mapper.Map(updateDurumDto, mevcutDurum);
               // mevcutDurum.GuncelleyenPersonelId = Convert.ToInt16(userId);
               // mevcutDurum.GuncellenmeTarihi = DateTime.Now;
                var status = await _manager.DurumService.TUpdateAsync(mevcutDurum);
                switch (status)
                {
                    case OperationStatus.Success:
                        return Json(new { success = true, message = "Durum başarılı bir şekilde güncellendi." });
                    case OperationStatus.GlobalError:
                    default:
                        var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                        _manager.LoggerService.LogError($"Area:Admin - Controller:Durum - Action: GuncelleDurum - User: {username} - Hata: Durum ID : {updateDurumDto.DurumID} Güncellenemedi!");
                        return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
                }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SilDurum(short durumID)
        {
            var status = _manager.DurumService.TDeleteAsync(durumID).Result;
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Durum başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu durum başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Durum - Action: SilDurum - User: {username} - Hata: Durum ID : {durumID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.DurumService.TGetAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:Durum - Action: GetirSafahatBilgi - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
    }
}
