using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.MalzemeDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = nameof(Admin))]
    public class MalzemeController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public MalzemeController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetirMalzemeCinsi(string? deger, int? selectedMalzemeCinsiId, int? malzemeTuruId)
        {
            return ViewComponent("_MalzemeCinsiGenelComponent", new { deger, selectedMalzemeCinsiId, malzemeTuruId });
        }

        [HttpGet]
        public async Task<IActionResult> GetirMalzemeMarka(string? deger, int? selectedMalzemeMarkaId, int? malzemeCinsiId)
        {
            return ViewComponent("_MalzemeMarkaGenelComponent", new { deger, selectedMalzemeMarkaId, malzemeCinsiId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EkleMalzeme([FromForm] CreateMalzemeDto createMalzemeDto)
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
            createMalzemeDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createMalzemeDto.OlusturulmaTarihi = DateTime.Now;

            var malzeme = _mapper.Map<Malzeme>(createMalzemeDto);
            var status = await _manager.MalzemeService.TAddAsync(malzeme);

            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni malzeme başarılı bir şekilde eklendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Malzeme - Action:EkleMalzeme - User:{username} - Hata: Yeni Malzeme Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMalzemeById([FromRoute] int id)
        {
            var malzeme = await _manager.MalzemeService.TGetByIdAsync(id, false);
            return malzeme == null
                ? Json(new { success = false })
                : Json(new { success = true, data = malzeme });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncelleMalzeme([FromForm] UpdateMalzemeDto updateMalzemeDto)
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
            var mevcutMalzeme = await _manager.MalzemeService.TGetByIdAsync(updateMalzemeDto.MalzemeID, false);

            if (mevcutMalzeme == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }

            _mapper.Map(updateMalzemeDto, mevcutMalzeme);
            mevcutMalzeme.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutMalzeme.GuncellenmeTarihi = DateTime.Now;

            var status = await _manager.MalzemeService.TUpdateAsync(mevcutMalzeme);

            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Malzeme başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Malzeme - Action:GuncelleMalzeme - User:{username} - Hata: Malzeme ID : {updateMalzemeDto.MalzemeID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SilMalzeme(short MalzemeID)
        {
            var status = await _manager.MalzemeService.TDeleteAsync(MalzemeID);

            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Malzeme başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu malzeme başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Malzeme - Action:SilMalzeme - User:{username} - Hata: Malzeme ID : {MalzemeID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
    }
}
