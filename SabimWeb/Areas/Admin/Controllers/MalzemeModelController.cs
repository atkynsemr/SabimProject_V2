using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.MalzemeModelDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = nameof(Admin))]
    public class MalzemeModelController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public MalzemeModelController(IServiceManager manager, IMapper mapper)
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
        public async Task<IActionResult> EkleMalzemeModel([FromForm] CreateMalzemeModelDto createMalzemeModelDto)
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
            createMalzemeModelDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createMalzemeModelDto.OlusturulmaTarihi = DateTime.Now;

            var malzemeModel = _mapper.Map<MalzemeModel>(createMalzemeModelDto);
            var status = await _manager.MalzemeModelService.TAddAsync(malzemeModel);

            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni malzeme modeli başarılı bir şekilde eklendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeModel - Action:EkleMalzemeModel - User:{username} - Hata: Yeni Malzeme Modeli Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMalzemeModelById([FromRoute] int id)
        {
            try
            {
                var malzemeModel = await _manager.MalzemeModelService.TGetByIdAsync(id, false);
                if (malzemeModel == null)
                {
                    return Json(new { success = false });
                }
                return Json(new { success = true, data = malzemeModel });
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeModel - Action:GetMalzemeModelById - User:{username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncelleMalzemeModel([FromForm] UpdateMalzemeModelDto updateMalzemeModelDto)
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
            var mevcutMalzemeModel = await _manager.MalzemeModelService.TGetByIdAsync(updateMalzemeModelDto.MalzemeModelID, false);

            if (mevcutMalzemeModel == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }

            _mapper.Map(updateMalzemeModelDto, mevcutMalzemeModel);
            mevcutMalzemeModel.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutMalzemeModel.GuncellenmeTarihi = DateTime.Now;

            var status = await _manager.MalzemeModelService.TUpdateAsync(mevcutMalzemeModel);

            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Malzeme modeli başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeModel - Action:GuncelleMalzemeModel - User:{username} - Hata: Malzeme Model ID : {updateMalzemeModelDto.MalzemeModelID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.MalzemeModelService.TGetAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeModel - Action:GetirSafahatBilgi - User:{username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SilMalzemeModel(short MalzemeModelID)
        {
            var status = await _manager.MalzemeModelService.TDeleteAsync(MalzemeModelID);

            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Malzeme modeli başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu malzeme modeli başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:MalzemeModel - Action:SilMalzemeModel - User:{username} - Hata: Malzeme Model ID : {MalzemeModelID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
    }
}