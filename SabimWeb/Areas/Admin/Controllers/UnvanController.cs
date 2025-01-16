using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.UnvanDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = nameof(Admin))]
    public class UnvanController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public UnvanController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            //var unvanlar = await _manager.UnvanService.TGetAllUnvanWithPersonelCountAsync(false);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EkleUnvan([FromForm] CreateUnvanDto createUnvanDto)
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
            if (createUnvanDto.OncelikDurumu == 0)
            {
                var updateStatus = await _manager.UnvanService.TChangeOncelikSirasi(createUnvanDto.OncelikSirasi,null);
                if (updateStatus != OperationStatus.Success)
                {
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Unvan - Action: EkleUnvan - User: {username} - Hata: Unvan Öncelik Sıraları Güncelenemedi");
                }
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            createUnvanDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createUnvanDto.OlusturulmaTarihi = DateTime.Now;
            var Unvan = _mapper.Map<Unvan>(createUnvanDto);
            var status = await _manager.UnvanService.TAddAsync(Unvan);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni Unvan başarılı bir şekilde eklenmiştir." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Unvan - Action: EkleUnvan - User: {username} - Hata: Yeni Unvan Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetUnvanById([FromRoute] int id)
        {
            try
            {
                var unvan = await _manager.UnvanService.TGetByIdAsync(id, false);
                if (unvan == null)
                {
                    return Json(new { success = false });
                }
                return Json(new { success = true, data = unvan });
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:Unvan - Action: GetUnvanById - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncelleUnvan([FromForm] UpdateUnvanDto updateUnvanDto)
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
            if (updateUnvanDto.OncelikDurumu == 0 && (updateUnvanDto.UnvanID!= updateUnvanDto.SeciliUnvanId))
            {
                var updateStatus = await _manager.UnvanService.TChangeOncelikSirasi(updateUnvanDto.OncelikSirasi, updateUnvanDto.UnvanID);
                if (updateStatus != OperationStatus.Success)
                {
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Unvan - Action: GuncelleUnvan - User: {username} - Hata: Unvan Öncelik Sıraları Güncelenemedi");
                }
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var mevcutUnvan = await _manager.UnvanService.TGetByIdAsync(updateUnvanDto.UnvanID, false);
            if (mevcutUnvan == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }
            _mapper.Map(updateUnvanDto, mevcutUnvan);
            mevcutUnvan.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutUnvan.GuncellenmeTarihi = DateTime.Now;
            var status = await _manager.UnvanService.TUpdateAsync(mevcutUnvan);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Unvan başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Unvan - Action: GuncelleUnvan - User: {username} - Hata: Unvan  ID : {updateUnvanDto.UnvanID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.UnvanService.TGetAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:Unvan - Action: GetirSafahatBilgi - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpGet]
        public IActionResult GetUnvanComponent()
        {
            return ViewComponent("_UnvanGenelComponent");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SilUnvan(short UnvanID)
        {
            var status = _manager.UnvanService.TDeleteAsync(UnvanID).Result;
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Unvan başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu unvan başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Unvan - Action: SilUnvan - User: {username} - Hata: Unvan ID : {UnvanID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
    }
}
