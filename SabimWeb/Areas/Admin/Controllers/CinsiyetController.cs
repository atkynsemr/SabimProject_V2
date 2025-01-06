using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.CinsiyetDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class CinsiyetController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public CinsiyetController(IServiceManager manager, IMapper mapper)
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
        public async Task<IActionResult> EkleCinsiyet([FromForm] CreateCinsiyetDto createCinsiyetDto)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            if (!ModelState.IsValid)
            {
                var errors = ValidationHelper.GetModelErrors(ModelState);
                return BadRequest(errors);
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            createCinsiyetDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createCinsiyetDto.OlusturulmaTarihi = DateTime.Now;
            var cinsiyet = _mapper.Map<Cinsiyet>(createCinsiyetDto);
            var status = await _manager.CinsiyetService.TAddAsync(cinsiyet);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni cinsiyet başarılı bir şekilde eklenmiştir." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Cinsiyet - Action: EkleCinsiyet - User: {username} - Hata: Yeni Cinsiyet Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCinsiyetById([FromRoute] int id)
        {
            try
            {
                var cinsiyet = await _manager.CinsiyetService.TGetByIdAsync(id, false);
                if (cinsiyet == null)
                {
                    return Json(new { success = false });
                }

                return Json(new { success = true, data = cinsiyet });

            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:Cinsiyet - Action: GetCinsiyetById - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncelleCinsiyet([FromForm] UpdateCinsiyetDto updateCinsiyetDto)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            if (!ModelState.IsValid)
            {
                var errors = ValidationHelper.GetModelErrors(ModelState);
                return BadRequest(errors);
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var mevcutCinsiyet = await _manager.CinsiyetService.TGetByIdAsync(updateCinsiyetDto.CinsiyetID, false);
            if (mevcutCinsiyet == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }
            _mapper.Map(updateCinsiyetDto, mevcutCinsiyet);
            mevcutCinsiyet.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutCinsiyet.GuncellenmeTarihi = DateTime.Now;
            // _mapper.Map<Cinsiyet>(mevcutCinsiyet);
            var status = await _manager.CinsiyetService.TUpdateAsync(mevcutCinsiyet);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Cinsiyet başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Cinsiyet - Action: GuncelleCinsiyet - User: {username} - Hata: Cinsiyet ID : {updateCinsiyetDto.CinsiyetID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SilCinsiyet(short cinsiyetID)
        {
            var status = _manager.CinsiyetService.TDeleteAsync(cinsiyetID).Result;
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Cinsiyet başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu cinsiyet başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Cinsiyet - Action: SilCinsiyet - User: {username} - Hata: Cinsiyet ID : {cinsiyetID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.CinsiyetService.TGetAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:Cinsiyet - Action: GetirSafahatBilgi - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
    }
}
