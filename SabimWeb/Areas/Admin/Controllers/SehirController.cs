using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.SehirDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = nameof(Admin))]
    public class SehirController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public SehirController(IServiceManager manager, IMapper mapper)
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
        public async Task<IActionResult> EkleSehir([FromForm] CreateSehirDto createSehirDto)
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
            createSehirDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createSehirDto.OlusturulmaTarihi = DateTime.Now;
            var sehir = _mapper.Map<Sehir>(createSehirDto);
            var status = await _manager.SehirService.TAddAsync(sehir);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni şehir başarılı bir şekilde eklenmiştir." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Sehir - Action: EkleSehir - User: {username} - Hata: Yeni Şehir Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetSehirById([FromRoute] int id)
        {
            try
            {
                var sehir = await _manager.SehirService.TGetByIdAsync(id, false);
                if (sehir == null)
                {
                    return Json(new { success = false });
                }

                return Json(new { success = true, data = sehir });

            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:Sehir - Action: GetSehirById - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncelleSehir([FromForm] UpdateSehirDto updateSehirDto)
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
            var mevcutSehir = await _manager.SehirService.TGetByIdAsync(updateSehirDto.SehirID, false);
            if (mevcutSehir == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }
            _mapper.Map(updateSehirDto, mevcutSehir);
            mevcutSehir.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutSehir.GuncellenmeTarihi = DateTime.Now;
            var status = await _manager.SehirService.TUpdateAsync(mevcutSehir);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Şehir başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Sehir - Action: GuncelleSehir - User: {username} - Hata: Sehir ID : {updateSehirDto.SehirID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SilSehir(short sehirID)
        {
            var status = _manager.SehirService.TDeleteAsync(sehirID).Result;
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Şehir başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu şehir başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Sehir - Action: SilSehir - User: {username} - Hata: Sehir ID : {sehirID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.SehirService.TGetAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:Sehir - Action: GetirSafahatBilgi - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
    }
}
