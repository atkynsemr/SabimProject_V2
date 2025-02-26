using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.GorevlendirilmeTipiDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class GorevlendirilmeTipiController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public GorevlendirilmeTipiController(IServiceManager manager, IMapper mapper)
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
        public async Task<IActionResult> EkleGorevlendirilmeTipi([FromForm] CreateGorevlendirilmeTipiDto createGorevlendirilmeTipiDto)
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
            createGorevlendirilmeTipiDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createGorevlendirilmeTipiDto.OlusturulmaTarihi = DateTime.Now;
            var gorevlendirilmeTipi = _mapper.Map<GorevlendirilmeTipi>(createGorevlendirilmeTipiDto);
            var status = await _manager.GorevlendirilmeTipiService.TAddAsync(gorevlendirilmeTipi);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni görevlendirilme tipi başarılı bir şekilde eklenmiştir.", completeStatus = true });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:GorevlendirilmeTipi - Action: EkleGorevlendirilmeTipi - User: {username} - Hata: Yeni görevlendirilme tipi Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetGorevlendirilmeTipiById([FromRoute] int id)
        {
            try
            {
                var Bolum = await _manager.GorevlendirilmeTipiService.TGetByIdAsync(id, false);
                if (Bolum == null)
                {
                    return Json(new { success = false });
                }

                return Json(new { success = true, data = Bolum });

            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:GorevlendirilmeTipi - Action: GetGorevlendirilmeTipiById - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.GorevlendirilmeTipiService.TGetAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:AyrilisNedenleri - Action: GetirSafahatBilgi - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncelleGorevlendirilmeTipi([FromForm] UpdateGorevlendirilmeTipiDto updateGorevlendirilmeTipiDto)
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
            var mevcutGorevlendirilmeTipi = await _manager.GorevlendirilmeTipiService.TGetByIdAsync(updateGorevlendirilmeTipiDto.GorevlendirilmeTipiID, false);
            if (mevcutGorevlendirilmeTipi == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }
            _mapper.Map(updateGorevlendirilmeTipiDto, mevcutGorevlendirilmeTipi);
            mevcutGorevlendirilmeTipi.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutGorevlendirilmeTipi.GuncellenmeTarihi = DateTime.Now;
            var status = await _manager.GorevlendirilmeTipiService.TUpdateAsync(mevcutGorevlendirilmeTipi);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Görevlendirilme Tipi başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:GorevlendirilmeTipi - Action: GuncelleGorevlendirilmeTipi - User: {username} - Hata: Görevlendirilme Tipi ID : {updateGorevlendirilmeTipiDto.GorevlendirilmeTipiID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SilGorevlendirilmeTipi(short gorevlendirilmeTipiID)
        {
            var status = _manager.GorevlendirilmeTipiService.TDeleteAsync(gorevlendirilmeTipiID).Result;
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Görevlendirilme tipi başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu görevlendirilme tipi başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:GorevlendirilmeTipi - Action: SilGorevlendirilmeTipi - User: {username} - Hata: Görevlendirilme Tipi ID : {gorevlendirilmeTipiID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
    }
}
