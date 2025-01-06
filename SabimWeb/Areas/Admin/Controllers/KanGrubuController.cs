using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.KanGrubuDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = nameof(Admin))]
    public class KanGrubuController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public KanGrubuController(IServiceManager manager, IMapper mapper)
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
        public async Task<IActionResult> EkleKanGrubu([FromForm] CreateKanGrubuDto createKanGrubuDto)
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
            createKanGrubuDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createKanGrubuDto.OlusturulmaTarihi = DateTime.Now;
            var kanGrubu = _mapper.Map<KanGrubu>(createKanGrubuDto);
            var status = await _manager.KanGrubuService.TAddAsync(kanGrubu);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni kan grubu başarılı bir şekilde eklenmiştir." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:KanGrubu - Action: EkleKanGrubu - User: {username} - Hata: Yeni Kan Grubu Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetKanGrubuById([FromRoute] int id)
        {
            try
            {
                var kanGrubu = await _manager.KanGrubuService.TGetByIdAsync(id, false);
                if (kanGrubu == null)
                {
                    return Json(new { success = false });
                }

                return Json(new { success = true, data = kanGrubu });

            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:KanGrubu - Action: GetKanGrubuById - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncelleKanGrubu([FromForm] UpdateKanGrubuDto updateKanGrubuDto)
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
            var mevcutKanGrubu = await _manager.KanGrubuService.TGetByIdAsync(updateKanGrubuDto.KanGrubuID, false);
            if (mevcutKanGrubu == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }
            _mapper.Map(updateKanGrubuDto, mevcutKanGrubu);
            mevcutKanGrubu.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutKanGrubu.GuncellenmeTarihi = DateTime.Now;
            var status = await _manager.KanGrubuService.TUpdateAsync(mevcutKanGrubu);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Kan grubu başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:KanGrubu - Action: GuncelleKanGrubu - User: {username} - Hata: Kan Grubu ID : {updateKanGrubuDto.KanGrubuID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SilKanGrubu(short kanGrubuID)
        {
            var status = _manager.KanGrubuService.TDeleteAsync(kanGrubuID).Result;
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Kan grubu başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu kan grubu başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:KanGrubu - Action: SilKanGrubu - User: {username} - Hata: Kan Grubu ID : {kanGrubuID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.KanGrubuService.TGetAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:KanGrubu - Action: GetirSafahatBilgi - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
    }
}
