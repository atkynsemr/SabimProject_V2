using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.KabinetBazliBolumDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = nameof(Admin))]
    public class KabinetBazliBolumController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public KabinetBazliBolumController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            //var result = await _manager.KabinetBazliBolumService.TFindAllAsyncWithEntities(false,e => e.Kisims, equals => equals.Durum );
            //var KabinetBazliBolumler = _mapper.Map<List<ResultKabinetBazliBolumWithKisimCountDto>>(result);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EkleKabinetBazliBolum([FromForm] CreateKabinetBazliBolumDto createKabinetBazliBolumDto)
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
            createKabinetBazliBolumDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createKabinetBazliBolumDto.OlusturulmaTarihi = DateTime.Now;
            var kabinetBazliBolum = _mapper.Map<KabinetBazliBolum>(createKabinetBazliBolumDto);
            var status = await _manager.KabinetBazliBolumService.TAddAsync(kabinetBazliBolum);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni kabinet bazlı bölüm başarılı bir şekilde eklenmiştir." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:KabinetBazliBolum - Action: EkleKabinetBazliBolum - User: {username} - Hata: Yeni Kabinet Bazlı Bölüm Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetKabinetBazliBolumById([FromRoute] int id)
        {
            try
            {
                var kabinetBazliBolum = await _manager.KabinetBazliBolumService.TGetByIdAsync(id, false);
                if (kabinetBazliBolum == null)
                {
                    return Json(new { success = false });
                }
                return Json(new { success = true, data = kabinetBazliBolum });
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:KabinetBazliBolum - Action: GetKabinetBazliBolumById - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncelleKabinetBazliBolum([FromForm] UpdateKabinetBazliBolumDto updateKabinetBazliBolumDto)
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
            var mevcutKabinetBazliBolum = await _manager.KabinetBazliBolumService.TGetByIdAsync(updateKabinetBazliBolumDto.KabinetBazliBolumID, false);
            if (mevcutKabinetBazliBolum == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }
            _mapper.Map(updateKabinetBazliBolumDto, mevcutKabinetBazliBolum);
            mevcutKabinetBazliBolum.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutKabinetBazliBolum.GuncellenmeTarihi = DateTime.Now;
            var status = await _manager.KabinetBazliBolumService.TUpdateAsync(mevcutKabinetBazliBolum);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Kabinet bazlı bölüm başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:KabinetBazliBolum - Action: GuncelleKabinetBazliBolum - User: {username} - Hata: Kabinet Bazlı Bölüm ID : {updateKabinetBazliBolumDto.KabinetBazliBolumID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.KabinetBazliBolumService.TGetAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:KabinetBazliBolum - Action: GetirSafahatBilgi - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SilKabinetBazliBolum(short KabinetBazliBolumID)
        {
            var status = _manager.KabinetBazliBolumService.TDeleteAsync(KabinetBazliBolumID).Result;
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Kabinet bazlı bölüm başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu kabinet bazlı bölüm başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:KabinetBazliBolum - Action: SilKabinetBazliBolum - User: {username} - Hata: Kabinet Bazlı Bölüm ID : {KabinetBazliBolumID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
    }
}

