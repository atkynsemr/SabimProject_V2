using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.PersonelDtos;
using Sabim.Domain.DTOs.PersonelGorevlendirilmeDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using Sabim.Web.ViewModels;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = nameof(Admin))]
    public class PersonelGorevlendirilmeController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public PersonelGorevlendirilmeController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(short personelId)
        {
            var personel = await _manager.PersonelService.TGetByIdWithPersonelInfoAsync(personelId, false);
            var personelDto = _mapper.Map<ResultPersonelWithGorevYeriDto>(personel);
            var viewModel = new PersonelGorevlendirilmeViewModel
            {
                YeniPersonelGorevlendirilme = new CreatePersonelGorevlendirilmeDto(), // Boş bir DTO örneği
                GuncellePersonelGorevlendirilme = new UpdatePersonelGorevlendirilmeDto(), // Boş bir DTO örneği
                ListelePersonelBilgileri = personelDto // Personel bilgisi
            };
            // ViewModel ile View'a gönder
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> GetBirimComponent(int selectedBolumId)
        {
            return ViewComponent("_BirimGenelComponent", new { deger = "create", selectedBolumId });
        }
        [HttpGet]
        public async Task<IActionResult> GetKisimComponent(int selectedBirimId)
        {
            return ViewComponent("_KisimGenelComponent", new { deger = "create", selectedBirimId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EklePersonelGorevlendirilme([FromForm] CreatePersonelGorevlendirilmeDto createPersonelGorevlendirilmeDto)
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
            createPersonelGorevlendirilmeDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createPersonelGorevlendirilmeDto.OlusturulmaTarihi = DateTime.Now;
            if (createPersonelGorevlendirilmeDto.GorevlendirilmeBitisTarihi == null)
            {
                createPersonelGorevlendirilmeDto.GorevlendirilmeAktifMi = true;
            }
            else
            {
                if (createPersonelGorevlendirilmeDto.GorevlendirilmeBitisTarihi.Value.Date >= DateTime.Now.Date)
                {
                    createPersonelGorevlendirilmeDto.GorevlendirilmeAktifMi = true;
                }
                else
                {
                    createPersonelGorevlendirilmeDto.GorevlendirilmeAktifMi = false;
                }
            }
            var personelGorevlendirilmeleri = _mapper.Map<PersonelGorevlendirilme>(createPersonelGorevlendirilmeDto);
            var status = await _manager.PersonelGorevlendirilmeService.TAddAsync(personelGorevlendirilmeleri);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni personel görevlendirilme başarılı bir şekilde eklenmiştir.", completeStatus = true });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:EklePersonelGorevlendirilme - Action: EklePersonelGorevlendirilme - User: {username} - Hata: Yeni Personel Görevlendirme Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPersonelGorevlendirilmeById([FromRoute] int id)
        {
            try
            {
                var personelGorevlendirilme = await _manager.PersonelGorevlendirilmeService.TFindByIdWithIncludesAsync(id, false, x => x.Kisim.Birim,x => x.Kisim.Birim.Bolum);
                if (personelGorevlendirilme == null)
                {
                    return Json(new { success = false });
                }
                var personelDto = _mapper.Map<ResultPersonelGorevlendirilmeDto>(personelGorevlendirilme);
                return Json(new { success = true, data = personelDto });
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:PersonelGorevlendirilme - Action: GetPersonelGorevlendirilmeById - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.PersonelGorevlendirilmeService.TGetAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:PersonelGorevlendirilme - Action: GetirSafahatBilgi - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncellePersonelGorevlendirilme([FromForm] UpdatePersonelGorevlendirilmeDto updatePersonelGorevlendirilmeDto)
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
            var mevcutPersonelGorevlendirilme = await _manager.PersonelGorevlendirilmeService.TGetByIdAsync(updatePersonelGorevlendirilmeDto.PersonelGorevlendirilmeID, false);
            if (mevcutPersonelGorevlendirilme == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }
            if (updatePersonelGorevlendirilmeDto.GorevlendirilmeBitisTarihi == null)
            {
                updatePersonelGorevlendirilmeDto.GorevlendirilmeAktifMi = true;
            }
            else
            {
                if (updatePersonelGorevlendirilmeDto.GorevlendirilmeBitisTarihi.Value.Date >= DateTime.Now.Date)
                {
                    updatePersonelGorevlendirilmeDto.GorevlendirilmeAktifMi = true;
                }
                else
                {
                    updatePersonelGorevlendirilmeDto.GorevlendirilmeAktifMi = false;
                }
            }
            _mapper.Map(updatePersonelGorevlendirilmeDto, mevcutPersonelGorevlendirilme);
            mevcutPersonelGorevlendirilme.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutPersonelGorevlendirilme.GuncellenmeTarihi = DateTime.Now;
            var status = await _manager.PersonelGorevlendirilmeService.TUpdateAsync(mevcutPersonelGorevlendirilme);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Personel Görevlendirme başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:PersonelGorevlendirilme - Action: GuncellePersonelGorevlendirilme - User: {username} - Hata: Ayrılış Nedenleri ID : {updatePersonelGorevlendirilmeDto.PersonelGorevlendirilmeID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SilPersonelGorevlendirilme(short personelGorevlendirilmeID)
        {
            var status = _manager.PersonelGorevlendirilmeService.TDeleteAsync(personelGorevlendirilmeID).Result;
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Personel Görevlendirilme başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu personel görevlendirilme verisi başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:PersonelGorevlendirilme - Action: SilPersonelGorevlendirilme - User: {username} - Hata: Personel Gorevlendirilme ID : {personelGorevlendirilmeID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
    }
}

