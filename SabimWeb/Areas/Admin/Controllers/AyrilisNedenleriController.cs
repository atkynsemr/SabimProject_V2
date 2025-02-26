using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.PersonelAyrilisNedenleriDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = nameof(Admin))]
    public class AyrilisNedenleriController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public AyrilisNedenleriController(IServiceManager manager, IMapper mapper)
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
        public async Task<IActionResult> EklePersonelAyrilisNedenleri([FromForm] CreatePersonelAyrilisNedenleriDto createPersonelAyrilisNedenleriDto)
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
            createPersonelAyrilisNedenleriDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createPersonelAyrilisNedenleriDto.OlusturulmaTarihi = DateTime.Now;
            var ayrilisNedenleri = _mapper.Map<PersonelAyrilisNedenleri>(createPersonelAyrilisNedenleriDto);
            var status = await _manager.PersonelAyrilisNedenleriService.TAddAsync(ayrilisNedenleri);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni ayrılış nedeni başarılı bir şekilde eklenmiştir.", completeStatus = true });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:AyrilisNedenleri - Action: EklePersonelAyrilisNedenleri - User: {username} - Hata: Yeni ayrılış nedeni Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAyrilisNedenleriById([FromRoute] int id)
        {
            try
            {
                var Bolum = await _manager.PersonelAyrilisNedenleriService.TGetByIdAsync(id, false);
                if (Bolum == null)
                {
                    return Json(new { success = false });
                }

                return Json(new { success = true, data = Bolum });

            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:AyrilisNedenleri - Action: GetAyrilisNedenleriById - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.PersonelAyrilisNedenleriService.TGetAuditTrailWithDetailsAsync(id);
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
        public async Task<IActionResult> GuncellePersonelAyrilisNedenleri([FromForm] UpdatePersonelAyrilisNedenleriDto updatePersonelAyrilisNedenleriDto)
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
            var mevcutAyrilisNedenleri = await _manager.PersonelAyrilisNedenleriService.TGetByIdAsync(updatePersonelAyrilisNedenleriDto.PersonelAyrilisNedenleriID, false);
            if (mevcutAyrilisNedenleri == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }
            _mapper.Map(updatePersonelAyrilisNedenleriDto, mevcutAyrilisNedenleri);
            mevcutAyrilisNedenleri.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutAyrilisNedenleri.GuncellenmeTarihi = DateTime.Now;
            var status = await _manager.PersonelAyrilisNedenleriService.TUpdateAsync(mevcutAyrilisNedenleri);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Ayrılış Nedeni başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:AyrilisNedenleri - Action: GuncellePersonelAyrilisNedenleri - User: {username} - Hata: Ayrılış Nedenleri ID : {updatePersonelAyrilisNedenleriDto.PersonelAyrilisNedenleriID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SilPersonelAyrilisNedenleri(short personelAyrilisNedenleriID)
        {
            var status = _manager.PersonelAyrilisNedenleriService.TDeleteAsync(personelAyrilisNedenleriID).Result;
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Ayrılış nedeni başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu ayrılış nedeni başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:AyrilisNedenleri - Action: SilPersonelAyrilisNedenleri - User: {username} - Hata: Ayrılış Nedeni ID : {personelAyrilisNedenleriID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
    }
}
