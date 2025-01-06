using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.KurumDtos;
using Sabim.Domain.DTOs.KurumDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = nameof(Admin))]
    public class KurumController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public KurumController(IServiceManager manager, IMapper mapper)
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
        public async Task<IActionResult> EkleKurum([FromForm] CreateKurumDto createKurumDto)
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
            createKurumDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createKurumDto.OlusturulmaTarihi = DateTime.Now;
            var Kurum = _mapper.Map<Kurum>(createKurumDto);
            var status = await _manager.KurumService.TAddAsync(Kurum);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni kurum başarılı bir şekilde eklenmiştir." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Kurum - Action: EkleKurum - User: {username} - Hata: Yeni Kurum Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetKurumById([FromRoute] int id)
        {
            try
            {
                var Kurum = await _manager.KurumService.TGetByIdAsync(id, false);
                if (Kurum == null)
                {
                    return Json(new { success = false });
                }

                return Json(new { success = true, data = Kurum });

            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:Kurum - Action: GetKurumById - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpGet]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncelleKurum([FromForm] UpdateKurumDto updateKurumDto)
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
            var mevcutKurum = await _manager.KurumService.TGetByIdAsync(updateKurumDto.KurumID, false);
            if (mevcutKurum == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }
            _mapper.Map(updateKurumDto, mevcutKurum);
            mevcutKurum.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutKurum.GuncellenmeTarihi = DateTime.Now;
            var status = await _manager.KurumService.TUpdateAsync(mevcutKurum);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Kurum  başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Kurum - Action: GuncelleKurum - User: {username} - Hata: Kurum  ID : {updateKurumDto.KurumID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.KurumService.TGetAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:Kurum - Action: GetirSafahatBilgi - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SilKurum(short KurumID)
        {
            var status = _manager.KurumService.TDeleteAsync(KurumID).Result;
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Kurum başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu kurum başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Kurum - Action: SilKurum - User: {username} - Hata: Kurum Tipi ID : {KurumID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
    }
}
