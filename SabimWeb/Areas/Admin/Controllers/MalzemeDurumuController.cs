using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.MalzemeDurumuDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = nameof(Admin))]
    public class MalzemeDurumuController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public MalzemeDurumuController(IServiceManager manager, IMapper mapper)
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
        public async Task<IActionResult> EkleMalzemeDurumu([FromForm] CreateMalzemeDurumuDto createMalzemeDurumuDto)
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
            createMalzemeDurumuDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createMalzemeDurumuDto.OlusturulmaTarihi = DateTime.Now;

            var malzemeDurumu = _mapper.Map<MalzemeDurumu>(createMalzemeDurumuDto);
            var status = await _manager.MalzemeDurumuService.TAddAsync(malzemeDurumu);

            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni malzeme durumu başarılı bir şekilde eklendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeDurumu - Action:EkleMalzemeDurumu - User:{username} - Hata: Yeni Malzeme Durumu Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMalzemeDurumuById([FromRoute] int id)
        {
            try
            {
                var malzemeDurumu = await _manager.MalzemeDurumuService.TGetByIdAsync(id, false);
                if (malzemeDurumu == null)
                {
                    return Json(new { success = false });
                }
                return Json(new { success = true, data = malzemeDurumu });
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeDurumu - Action:GetMalzemeDurumuById - User:{username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncelleMalzemeDurumu([FromForm] UpdateMalzemeDurumuDto updateMalzemeDurumuDto)
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
            var mevcutMalzemeDurumu = await _manager.MalzemeDurumuService.TGetByIdAsync(updateMalzemeDurumuDto.MalzemeDurumuID, false);

            if (mevcutMalzemeDurumu == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }

            _mapper.Map(updateMalzemeDurumuDto, mevcutMalzemeDurumu);
            mevcutMalzemeDurumu.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutMalzemeDurumu.GuncellenmeTarihi = DateTime.Now;

            var status = await _manager.MalzemeDurumuService.TUpdateAsync(mevcutMalzemeDurumu);

            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Malzeme durumu başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeDurumu - Action:GuncelleMalzemeDurumu - User:{username} - Hata: Malzeme Durumu ID : {updateMalzemeDurumuDto.MalzemeDurumuID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.MalzemeDurumuService.TGetAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeDurumu - Action:GetirSafahatBilgi - User:{username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SilMalzemeDurumu(short MalzemeDurumuID)
        {
            var status = await _manager.MalzemeDurumuService.TDeleteAsync(MalzemeDurumuID);

            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Malzeme durumu başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu malzeme durumu başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeDurumu - Action:SilMalzemeDurumu - User:{username} - Hata: Malzeme Durumu ID : {MalzemeDurumuID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
    }
}