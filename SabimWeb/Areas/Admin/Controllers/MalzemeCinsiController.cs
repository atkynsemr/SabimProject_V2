using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.MalzemeCinsiDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = nameof(Admin))]
    public class MalzemeCinsiController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public MalzemeCinsiController(IServiceManager manager, IMapper mapper)
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
        public async Task<IActionResult> EkleMalzemeCinsi([FromForm] CreateMalzemeCinsiDto createMalzemeCinsiDto)
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
            createMalzemeCinsiDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createMalzemeCinsiDto.OlusturulmaTarihi = DateTime.Now;

            var malzemeCinsi = _mapper.Map<MalzemeCinsi>(createMalzemeCinsiDto);
            var status = await _manager.MalzemeCinsiService.TAddAsync(malzemeCinsi);

            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni malzeme cinsi başarılı bir şekilde eklendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeCinsi - Action:EkleMalzemeCinsi - User:{username} - Hata: Yeni Malzeme Cinsi Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMalzemeCinsiById([FromRoute] int id)
        {
            try
            {
                var malzemeCinsi = await _manager.MalzemeCinsiService.TGetByIdAsync(id, false);
                if (malzemeCinsi == null)
                {
                    return Json(new { success = false });
                }
                return Json(new { success = true, data = malzemeCinsi });
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeCinsi - Action:GetMalzemeCinsiById - User:{username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncelleMalzemeCinsi([FromForm] UpdateMalzemeCinsiDto updateMalzemeCinsiDto)
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
            var mevcutMalzemeCinsi = await _manager.MalzemeCinsiService.TGetByIdAsync(updateMalzemeCinsiDto.MalzemeCinsiID, false);

            if (mevcutMalzemeCinsi == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }

            _mapper.Map(updateMalzemeCinsiDto, mevcutMalzemeCinsi);
            mevcutMalzemeCinsi.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutMalzemeCinsi.GuncellenmeTarihi = DateTime.Now;

            var status = await _manager.MalzemeCinsiService.TUpdateAsync(mevcutMalzemeCinsi);

            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Malzeme cinsi başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeCinsi - Action:GuncelleMalzemeCinsi - User:{username} - Hata: Malzeme Cinsi ID : {updateMalzemeCinsiDto.MalzemeCinsiID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.MalzemeCinsiService.TGetAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeCinsi - Action:GetirSafahatBilgi - User:{username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SilMalzemeCinsi(short MalzemeCinsiID)
        {
            var status = await _manager.MalzemeCinsiService.TDeleteAsync(MalzemeCinsiID);

            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Malzeme cinsi başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu malzeme cinsi başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeCinsi - Action:SilMalzemeCinsi - User:{username} - Hata: Malzeme Cinsi ID : {MalzemeCinsiID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
    }
}