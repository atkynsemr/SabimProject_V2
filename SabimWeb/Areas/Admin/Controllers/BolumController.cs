using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.BolumDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = nameof(Admin))]
    public class BolumController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public BolumController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            //var result = await _manager.BolumService.TFindAllAsyncWithEntities(false, e => e.Durum, e => e.Birims);
            //var bolumler = _mapper.Map<List<ResultBolumWithBirimCountDto>>(result);
            //return View(bolumler);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EkleBolum([FromForm] CreateBolumDto createBolumDto)
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
            createBolumDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createBolumDto.OlusturulmaTarihi = DateTime.Now;
            var bolum = _mapper.Map<Bolum>(createBolumDto);
            var status = await _manager.BolumService.TAddAsync(bolum);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni bölüm başarılı bir şekilde eklenmiştir." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Bolum - Action: EkleBolum - User: {username} - Hata: Yeni Bölüm Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetBolumById([FromRoute] int id)
        {
            try
            {
                var Bolum = await _manager.BolumService.TGetByIdAsync(id, false);
                if (Bolum == null)
                {
                    return Json(new { success = false });
                }

                return Json(new { success = true, data = Bolum });

            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:Bolum - Action: GetBolumById - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncelleBolum([FromForm] UpdateBolumDto updateBolumDto)
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
            var mevcutBolum = await _manager.BolumService.TGetByIdAsync(updateBolumDto.BolumID, false);
            if (mevcutBolum == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }
            _mapper.Map(updateBolumDto, mevcutBolum);
            mevcutBolum.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutBolum.GuncellenmeTarihi = DateTime.Now;
            var status = await _manager.BolumService.TUpdateAsync(mevcutBolum);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Bölüm başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Bolum - Action: GuncelleBolum - User: {username} - Hata: Bölüm  ID : {updateBolumDto.BolumID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.BolumService.TGetAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:Bolum - Action: GetirSafahatBilgi - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SilBolum(short BolumID)
        {
            var status = _manager.BolumService.TDeleteAsync(BolumID).Result;
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Bölüm başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu bölüm başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Bolum - Action: SilBolum - User: {username} - Hata: Bölüm ID : {BolumID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
    }
}
