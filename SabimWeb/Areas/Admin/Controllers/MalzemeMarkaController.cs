using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.MalzemeMarkaDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = nameof(Admin))]
    public class MalzemeMarkaController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public MalzemeMarkaController(IServiceManager manager, IMapper mapper)
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
        public async Task<IActionResult> EkleMalzemeMarka([FromForm] CreateMalzemeMarkaDto createMalzemeMarkaDto)
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
            createMalzemeMarkaDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createMalzemeMarkaDto.OlusturulmaTarihi = DateTime.Now;

            var malzemeMarka = _mapper.Map<MalzemeMarka>(createMalzemeMarkaDto);
            var status = await _manager.MalzemeMarkaService.TAddAsync(malzemeMarka);

            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni malzeme markası başarılı bir şekilde eklendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeMarka - Action:EkleMalzemeMarka - User:{username} - Hata: Yeni Malzeme Markası Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMalzemeMarkaById([FromRoute] int id)
        {
            try
            {
                var malzemeMarka = await _manager.MalzemeMarkaService.TGetByIdAsync(id, false);
                if (malzemeMarka == null)
                {
                    return Json(new { success = false });
                }
                return Json(new { success = true, data = malzemeMarka });
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeMarka - Action:GetMalzemeMarkaById - User:{username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncelleMalzemeMarka([FromForm] UpdateMalzemeMarkaDto updateMalzemeMarkaDto)
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
            var mevcutMalzemeMarka = await _manager.MalzemeMarkaService.TGetByIdAsync(updateMalzemeMarkaDto.MalzemeMarkaID, false);

            if (mevcutMalzemeMarka == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }

            _mapper.Map(updateMalzemeMarkaDto, mevcutMalzemeMarka);
            mevcutMalzemeMarka.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutMalzemeMarka.GuncellenmeTarihi = DateTime.Now;

            var status = await _manager.MalzemeMarkaService.TUpdateAsync(mevcutMalzemeMarka);

            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Malzeme markası başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeMarka - Action:GuncelleMalzemeMarka - User:{username} - Hata: Malzeme Marka ID : {updateMalzemeMarkaDto.MalzemeMarkaID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.MalzemeMarkaService.TGetAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeMarka - Action:GetirSafahatBilgi - User:{username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SilMalzemeMarka(short MalzemeMarkaID)
        {
            var status = await _manager.MalzemeMarkaService.TDeleteAsync(MalzemeMarkaID);

            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Malzeme markası başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu malzeme markası başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeMarka - Action:SilMalzemeMarka - User:{username} - Hata: Malzeme Marka ID : {MalzemeMarkaID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
    }
}