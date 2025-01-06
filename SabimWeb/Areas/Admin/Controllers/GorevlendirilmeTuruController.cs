using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.GorevlendirilmeTuruDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class GorevlendirilmeTuruController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public GorevlendirilmeTuruController(IServiceManager manager, IMapper mapper)
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
        public async Task<IActionResult> EkleGorevlendirilmeTuru([FromForm] CreateGorevlendirilmeTuruDto createGorevlendirilmeTuruDto)
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
            createGorevlendirilmeTuruDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createGorevlendirilmeTuruDto.OlusturulmaTarihi = DateTime.Now;
            var gorevlendirilmeTuru = _mapper.Map<GorevlendirilmeTuru>(createGorevlendirilmeTuruDto);
            var status = await _manager.GorevlendirilmeTuruService.TAddAsync(gorevlendirilmeTuru);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni görevlendirilme türü başarılı bir şekilde eklenmiştir." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:GorevlendirilmeTuru - Action: EkleGorevlendirilmeTuru - User: {username} - Hata: Yeni Görevlendirilme Türü Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetGorevlendirilmeTuruById([FromRoute] int id)
        {
            try
            {
                var gorevlendirilmeTuru = await _manager.GorevlendirilmeTuruService.TGetByIdAsync(id, false);
                if (gorevlendirilmeTuru == null)
                {
                    return Json(new { success = false });
                }

                return Json(new { success = true, data = gorevlendirilmeTuru });

            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:GorevlendirilmeTuru - Action: GetGorevlendirilmeTuruById - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncelleGorevlendirilmeTuru([FromForm] UpdateGorevlendirilmeTuruDto updateGorevlendirilmeTuruDto)
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
            var mevcutGorevlendirilmeTuru = await _manager.GorevlendirilmeTuruService.TGetByIdAsync(updateGorevlendirilmeTuruDto.GorevlendirilmeTuruID, false);
            if (mevcutGorevlendirilmeTuru == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }
            _mapper.Map(updateGorevlendirilmeTuruDto, mevcutGorevlendirilmeTuru);
            mevcutGorevlendirilmeTuru.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutGorevlendirilmeTuru.GuncellenmeTarihi = DateTime.Now;
            var status = await _manager.GorevlendirilmeTuruService.TUpdateAsync(mevcutGorevlendirilmeTuru);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Görevlendirilme türü başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:GorevlendirilmeTuru - Action: GuncelleGorevlendirilmeTuru - User: {username} - Hata: Görevlendirilme Türü ID : {updateGorevlendirilmeTuruDto.GorevlendirilmeTuruID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SilGorevlendirilmeTuru(short gorevlendirilmeTuruID)
        {
            var status = _manager.GorevlendirilmeTuruService.TDeleteAsync(gorevlendirilmeTuruID).Result;
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Görevlendirilme türü başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu görevlendirilme türü başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:GorevlendirilmeTuru - Action: SilGorevlendirilmeTuru - User: {username} - Hata: Görevlendirilme Türü ID : {gorevlendirilmeTuruID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.GorevlendirilmeTuruService.TGetAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:GorevlendirilmeTuru - Action: GetirSafahatBilgi - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
    }
}
