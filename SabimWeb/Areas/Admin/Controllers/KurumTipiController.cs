using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.KurumTipiDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = nameof(Admin))]
    public class KurumTipiController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public KurumTipiController(IServiceManager manager, IMapper mapper)
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
        public async Task<IActionResult> EkleKurumTipi([FromForm] CreateKurumTipiDto createKurumTipiDto)
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
            createKurumTipiDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createKurumTipiDto.OlusturulmaTarihi = DateTime.Now;
            var KurumTipi = _mapper.Map<KurumTipi>(createKurumTipiDto);
            var status = await _manager.KurumTipiService.TAddAsync(KurumTipi);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni kurum tipi başarılı bir şekilde eklenmiştir." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:KurumTipi - Action: EkleKurumTipi - User: {username} - Hata: Yeni Kurum Tipi Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetKurumTipiById([FromRoute] int id)
        {
            try
            {
                var KurumTipi = await _manager.KurumTipiService.TGetByIdAsync(id, false);
                if (KurumTipi == null)
                {
                    return Json(new { success = false });
                }

                return Json(new { success = true, data = KurumTipi });

            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:KurumTipi - Action: GetKurumTipiById - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpGet]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncelleKurumTipi([FromForm] UpdateKurumTipiDto updateKurumTipiDto)
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
            var mevcutKurumTipi = await _manager.KurumTipiService.TGetByIdAsync(updateKurumTipiDto.KurumTipiID, false);
            if (mevcutKurumTipi == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }
            _mapper.Map(updateKurumTipiDto, mevcutKurumTipi);
            mevcutKurumTipi.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutKurumTipi.GuncellenmeTarihi = DateTime.Now;
            var status = await _manager.KurumTipiService.TUpdateAsync(mevcutKurumTipi);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Kurum tipi başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:KurumTipi - Action: GuncelleKurumTipi - User: {username} - Hata: Kurum Tipi ID : {updateKurumTipiDto.KurumTipiID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.KurumTipiService.TGetAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:KurumTipi - Action: GetirSafahatBilgi - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SilKurumTipi(short KurumTipiID)
        {
            var status = _manager.KurumTipiService.TDeleteAsync(KurumTipiID).Result;
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Kurum Tipi başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu kurum tipi başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:KurumTipi - Action: SilKurumTipi - User: {username} - Hata: Kurum Tipi ID : {KurumTipiID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
    }
}
