using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.MalzemeTuruDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = nameof(Admin))]
    public class MalzemeTuruController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public MalzemeTuruController(IServiceManager manager, IMapper mapper)
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
        public async Task<IActionResult> EkleMalzemeTuru([FromForm] CreateMalzemeTuruDto createMalzemeTuruDto)
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
            createMalzemeTuruDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createMalzemeTuruDto.OlusturulmaTarihi = DateTime.Now;

            var malzemeTuru = _mapper.Map<MalzemeTuru>(createMalzemeTuruDto);
            var status = await _manager.MalzemeTuruService.TAddAsync(malzemeTuru);

            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni malzeme türü başarılı bir şekilde eklendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeTuru - Action:EkleMalzemeTuru - User:{username} - Hata: Yeni Malzeme Türü Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMalzemeTuruById([FromRoute] int id)
        {
            try
            {
                var malzemeTuru = await _manager.MalzemeTuruService.TGetByIdAsync(id, false);
                if (malzemeTuru == null)
                {
                    return Json(new { success = false });
                }
                return Json(new { success = true, data = malzemeTuru });
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeTuru - Action:GetMalzemeTuruById - User:{username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncelleMalzemeTuru([FromForm] UpdateMalzemeTuruDto updateMalzemeTuruDto)
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
            var mevcutMalzemeTuru = await _manager.MalzemeTuruService.TGetByIdAsync(updateMalzemeTuruDto.MalzemeTuruID, false);

            if (mevcutMalzemeTuru == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }

            _mapper.Map(updateMalzemeTuruDto, mevcutMalzemeTuru);
            mevcutMalzemeTuru.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutMalzemeTuru.GuncellenmeTarihi = DateTime.Now;

            var status = await _manager.MalzemeTuruService.TUpdateAsync(mevcutMalzemeTuru);

            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Malzeme türü başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeTuru - Action:GuncelleMalzemeTuru - User:{username} - Hata: Malzeme Türü ID : {updateMalzemeTuruDto.MalzemeTuruID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.MalzemeTuruService.TGetAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeTuru - Action:GetirSafahatBilgi - User:{username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SilMalzemeTuru(short MalzemeTuruID)
        {
            var status = await _manager.MalzemeTuruService.TDeleteAsync(MalzemeTuruID);

            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Malzeme türü başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu malzeme türü başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeTuru - Action:SilMalzemeTuru - User:{username} - Hata: Malzeme Türü ID : {MalzemeTuruID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
    }
}
