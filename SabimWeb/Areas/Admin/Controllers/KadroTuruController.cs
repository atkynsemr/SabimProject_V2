using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.KadroTuruDtos;
using Sabim.Domain.DTOs.KadroTuruDtos;
using Sabim.Domain.DTOs.KadroTuruDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = nameof(Admin))]
    public class KadroTuruController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public KadroTuruController(IServiceManager manager, IMapper mapper)
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
        public async Task<IActionResult> EkleKadroTuru([FromForm] CreateKadroTuruDto createKadroTuruDto)
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
            createKadroTuruDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createKadroTuruDto.OlusturulmaTarihi = DateTime.Now;
            var KadroTuru = _mapper.Map<KadroTuru>(createKadroTuruDto);
            var status = await _manager.KadroTuruService.TAddAsync(KadroTuru);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni kadro türü başarılı bir şekilde eklenmiştir." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:KadroTuru - Action: EkleKadroTuru - User: {username} - Hata: Yeni Kadro Türü Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetKadroTuruById([FromRoute] int id)
        {
            try
            {
                var KadroTuru = await _manager.KadroTuruService.TGetByIdAsync(id, false);
                if (KadroTuru == null)
                {
                    return Json(new { success = false });
                }

                return Json(new { success = true, data = KadroTuru });

            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:KadroTuru - Action: GetKadroTuruById - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpGet]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncelleKadroTuru([FromForm] UpdateKadroTuruDto updateKadroTuruDto)
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
            var mevcutKadroTuru = await _manager.KadroTuruService.TGetByIdAsync(updateKadroTuruDto.KadroTuruID, false);
            if (mevcutKadroTuru == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }
            _mapper.Map(updateKadroTuruDto, mevcutKadroTuru);
            mevcutKadroTuru.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutKadroTuru.GuncellenmeTarihi = DateTime.Now;
            var status = await _manager.KadroTuruService.TUpdateAsync(mevcutKadroTuru);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Kadro türü başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:KadroTuru - Action: GuncelleKadroTuru - User: {username} - Hata: Kadro Türü ID : {updateKadroTuruDto.KadroTuruID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.KadroTuruService.TGetAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:KadroTuru - Action: GetirSafahatBilgi - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SilKadroTuru(short kadroTuruID)
        {
            var status = _manager.KadroTuruService.TDeleteAsync(kadroTuruID).Result;
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Kadro Türü başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu kadro türü başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:KadroTuru - Action: SilKadroTuru - User: {username} - Hata: Kadro Türü ID : {kadroTuruID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
    }
}
