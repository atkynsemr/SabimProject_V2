using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sabim.Domain.Constants;
using Sabim.Domain.DTOs.AppUserDtos;
using Sabim.Domain.DTOs.PersonelAyrilisDtos;
using Sabim.Domain.DTOs.PersonelDtos;
using Sabim.Domain.DTOs.PersonelUnvanGecmisiDtos;
using Sabim.Domain.DTOs.PersonelWithUserDto;
using Sabim.Domain.DTOs.SavciCalisilanKatipDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers.MethodHelper;
using Sabim.Web.ViewModels;
using System.Security.Claims;

namespace Sabim.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = nameof(Admin))]
    public class PersonelController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public PersonelController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetirKurumlar(string? deger, int? selectedKurumId, int id)
        {
            return ViewComponent("_KurumGenelComponent", new { deger, selectedKurumId, kurumTipiId = 1, sehirId = id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EklePersonel([FromForm] CreatePersonelDto createPersonelDto, List<short> SelectedPersonelIds, [FromForm] CreateUserDto createUserDto)
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
            var dto = new CreatePersonelWithUserDto
            {
                Personel = _mapper.Map<Personel>(createPersonelDto),
                UserDto = createUserDto,
                SelectedPersonelIds = SelectedPersonelIds
            };
            dto.Personel.OlusturanPersonelId = Convert.ToInt16(userId);
            dto.Personel.OlusturulmaTarihi = DateTime.Now;
            dto.UserDto.OlusturanPersonelId = Convert.ToInt16(userId);
            dto.UserDto.OlusturulmaTarihi = DateTime.Now;
            // Service çağrısı
            var status = await _manager.PersonelService.TAddPersonelWithUserAsync(dto);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni personel başarılı bir şekilde eklenmiştir.", completeStatus = true });
                case OperationStatus.Incomplete:
                    var usrname = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Personel - Action: EklePersonel - User: {usrname} - Hata: Yeni Personel Eklendi. Ancak {createPersonelDto.AdSoyad} kullanıcı ve rolleri eklenememiştir!");
                    return Json(new { success = true, message = "İşleminizi kontrol ediniz. İşlem tamamlanmamış ise lütfen tekrar deneyiniz!", completeStatus = false });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Personel - Action: EklePersonel - User: {username} - Hata: Yeni Personel Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPersonelById([FromRoute] int id)
        {
            try
            {
                var personel = await _manager.PersonelService.TGetPersonelByIdAsync(id, false);
                if (personel == null)
                {
                    return Json(new { success = false });
                }
                return Json(new { success = true, data = personel });
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:Personel - Action: GetPersonelById - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncellePersonel([FromForm] UpdatePersonelDto updatePersonelDto, List<short>? SelectedPersonelIds)
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
            var mevcutPersonel = await _manager.PersonelService.TGetByIdAsync(updatePersonelDto.PersonelID, false);
            if (mevcutPersonel == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }
            _mapper.Map(updatePersonelDto, mevcutPersonel);
            mevcutPersonel.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutPersonel.GuncellenmeTarihi = DateTime.Now;
            var status = await _manager.PersonelService.TUpdateAsync(mevcutPersonel);
            switch (status)
            {
                case OperationStatus.Success:
                    if ((SelectedPersonelIds != null && SelectedPersonelIds.Count > 0) && (updatePersonelDto.UnvanId == 1 || updatePersonelDto.UnvanId == 2 || updatePersonelDto.UnvanId == 5))
                    {
                        var updateSavciCalisilanKatipler = new UpdateSavciCalisilanKatipDto
                        {
                            SelectedPersonelIds = SelectedPersonelIds,
                            SavciId = updatePersonelDto.PersonelID,
                            OlusturanPersonelId = Convert.ToInt16(userId)
                        };
                        var result = await _manager.PersonelService.TUpdateCalisilanKatipler(updateSavciCalisilanKatipler);
                        switch (result)
                        {
                            case OperationStatus.Success:
                                return Json(new { success = true, message = "Savcının çalıştığı katipler güncellendi." });
                            case OperationStatus.GlobalError:
                            default:
                                return Json(new { success = false, message = "Savcının çalıştığı katipler güncellenirken hata oluştu." });
                        }
                    }
                    else if (
                        (SelectedPersonelIds != null && SelectedPersonelIds.Count > 0 && !(new[] { 1, 2, 5 }.Contains(updatePersonelDto.UnvanId))) ||
                        (SelectedPersonelIds == null || SelectedPersonelIds.Count == 0 && (new[] { 1, 2, 5 }.Contains(updatePersonelDto.UnvanId))))
                    {
                        var result = await _manager.SavciCalisilanKatipService.TDeleteRangeByExpressionAsync(x => x.SavciId == updatePersonelDto.PersonelID);
                    }

                    return Json(new { success = true, message = "Personel başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Personel - Action: GuncellePersonel - User: {username} - Hata: Personel ID : {updatePersonelDto.PersonelID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetirSafahatBilgi([FromRoute] int id)
        {
            try
            {
                var safahatBilgi = await _manager.PersonelService.TGetAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:Personel - Action: GetirSafahatBilgi - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SilPersonel(short PersonelID)
        {
            var status = _manager.PersonelService.TDeleteAsync(PersonelID).Result;
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Personel başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu Personel başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Personel - Action: SilPersonel - User: {username} - Hata: Personel ID : {PersonelID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> PersonelUnvanlari(short PersonelID) {
            var personel = await _manager.PersonelService.TGetPersonelByIdAsync(PersonelID,false);
            var personelDto = _mapper.Map<ResultPersonelDto>(personel);
            var viewModel = new PersonelUnvanGecmisiViewModel
            {
                // Yeni personel unvan bilgisi (örnek, bu kısmı ihtiyacınıza göre uyarlayın)
                YeniPersonelUnvan = new CreatePersonelUnvanGecmisiDto(), // Boş bir DTO örneği
                GuncellePersonelUnvan = new UpdatePersonelUnvanGecmisiDto(), // Boş bir DTO örneği
                ListelePersonelUnvan = personelDto // Personel bilgisi
            };
            // ViewModel ile View'a gönder
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EklePersonelUnvan([FromForm] CreatePersonelUnvanGecmisiDto createPersonelUnvanGecmisiDto)
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
            createPersonelUnvanGecmisiDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createPersonelUnvanGecmisiDto.OlusturulmaTarihi = DateTime.Now;
            var status = await _manager.PersonelService.TAddPersonelUnvanAsync(createPersonelUnvanGecmisiDto);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni personel unvanı başarılı bir şekilde eklenmiştir.", completeStatus = true });
               
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Personel - Action: EklePersonelUnvan - User: {username} - Hata: Yeni Personel Unvanı Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPersonelUnvanById([FromRoute] short id)
        {
            try
            {
                var personelUnvan = await _manager.PersonelService.TGetPersonelUnvanByIdAsync(id, false);
                if (personelUnvan == null)
                {
                    return Json(new { success = false });                                                      
                }
                return Json(new { success = true, data = personelUnvan });
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:Personel - Action: GetPersonelUnvanById - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncellePersonelUnvanGecmisi([FromForm] UpdatePersonelUnvanGecmisiDto updatePersonelUnvanGecmisiDto)
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
            var mevcutPersonelUnvanGecmisi = await _manager.PersonelService.TGetPersonelUnvanGecmisiByIdAsync(updatePersonelUnvanGecmisiDto.PersonelUnvanGecmisiID, false);
            if (mevcutPersonelUnvanGecmisi == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }
            _mapper.Map(updatePersonelUnvanGecmisiDto, mevcutPersonelUnvanGecmisi);
            mevcutPersonelUnvanGecmisi.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutPersonelUnvanGecmisi.GuncellenmeTarihi = DateTime.Now;
            var status = await _manager.PersonelService.TUpdatePersonelUnvanGecmisiAsync(mevcutPersonelUnvanGecmisi);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Personel unvanı başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Personel - Action: GuncellePersonelUnvanGecmisi - User: {username} - Hata: Personel Unvan Gecmisi ID : {updatePersonelUnvanGecmisiDto.PersonelUnvanGecmisiID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SilPersonelUnvanGecmisi(short PersonelUnvanGecmisiID)
        {
            var status = _manager.PersonelService.TDeletePersonelUnvanAsync(PersonelUnvanGecmisiID).Result;
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Personel unvanı başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu Personel başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Personel - Action: SilPersonelUnvanGecmisi - User: {username} - Hata: Personel Unvan Gecmisi : {PersonelUnvanGecmisiID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        public async Task<IActionResult> GetirPersonelUnvanSafahatBilgi([FromRoute] short id)
        {
            try
            {
                var safahatBilgi = await _manager.PersonelService.TGetPersonelUnvanAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:Personel - Action: GetirSafahatBilgi - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpGet]
        public async Task<IActionResult> PersonelIzinleri(short PersonelID)
        {
            var personel = await _manager.PersonelService.TGetPersonelByIdAsync(PersonelID, false);
            var personelDto = _mapper.Map<ResultPersonelDto>(personel);
            var viewModel = new PersonelAyrilisViewModel
            {
                YeniPersonelAyrilis = new CreatePersonelAyrilisDto(), // Boş bir DTO örneği
                GuncellePersonelAyrilis = new UpdatePersonelAyrilisDto(), // Boş bir DTO örneği
                ListelePersonelAyrilis = personelDto // Personel bilgisi
            };
            // ViewModel ile View'a gönder
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EklePersonelIzin([FromForm] CreatePersonelAyrilisDto createPersonelAyrilisDto)
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
            createPersonelAyrilisDto.OlusturanPersonelId = Convert.ToInt16(userId);
            createPersonelAyrilisDto.OlusturulmaTarihi = DateTime.Now;
            var status = await _manager.PersonelService.TAddPersonelIzinleriAsync(createPersonelAyrilisDto);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Yeni personel izni başarılı bir şekilde eklenmiştir.", completeStatus = true });
                case OperationStatus.DateConflict:
                    return Json(new { success = true, message = OperationStatus.DateConflict, completeStatus = false });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Personel - Action: EklePersonelIzin - User: {username} - Hata: Yeni Personel İzni Eklenemedi!");
                    return Json(new { success = false, message = "Kaydetme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPersonelAyrilisById([FromRoute] short id)
        {
            try
            {
                var personelIzin = await _manager.PersonelService.TGetPersonelAyrilisById(id, false);
                if (personelIzin == null)
                {
                    return Json(new { success = false });
                }
                return Json(new
                {
                    success = true,
                    data = new
                    {
                        personelIzin.PersonelAyrilisID,
                        Aciklama = personelIzin.PersonelAyrilisNedenleri?.Aciklama,
                        BaslangicTarihi = personelIzin.BaslangicTarihi?.ToString("yyyy-MM-dd"),
                        BitisTarihi = personelIzin.BitisTarihi?.ToString("yyyy-MM-dd"),
                        KaliciAyrilisMi = personelIzin.PersonelAyrilisNedenleri?.KaliciAyrilisMi,
                        DonanimUyarisi = personelIzin.PersonelAyrilisNedenleri?.DonanimUyarisi,
                        PersonelId = personelIzin.PersonelId,
                        DurumId=personelIzin.DurumId
                    }
                });
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:Personel - Action: GetPersonelAyrilisById - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuncellePersonelIzin([FromForm] UpdatePersonelAyrilisDto updatePersonelAyrilisDto)
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
            var mevcutPersonelIzin= await _manager.PersonelService.TGetPersonelAyrilisById(updatePersonelAyrilisDto.PersonelAyrilisID, false);
            if (mevcutPersonelIzin == null)
            {
                return Json(new { success = false, message = "Kayıt Bulunamadı!" });
            }
            _mapper.Map(updatePersonelAyrilisDto, mevcutPersonelIzin);
            mevcutPersonelIzin.GuncelleyenPersonelId = Convert.ToInt16(userId);
            mevcutPersonelIzin.GuncellenmeTarihi = DateTime.Now;
            var status = await _manager.PersonelService.TUpdatePersonelIzinAsync(mevcutPersonelIzin);
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Personel izin başarılı bir şekilde güncellendi." });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Personel - Action: GuncellePersonelIzin - User: {username} - Hata: Personel Ayrilis ID : {updatePersonelAyrilisDto.PersonelAyrilisID} Güncellenemedi!");
                    return Json(new { success = false, message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SilPersonelAyrilis(short PersonelAyrilisID)
        {
            var status = _manager.PersonelService.TDeletePersonelIzinAsync(PersonelAyrilisID).Result;
            switch (status)
            {
                case OperationStatus.Success:
                    return Json(new { success = true, message = "Personel izni başarılı bir şekilde silindi." });
                case OperationStatus.NotFound:
                    return Json(new { success = false, message = "Kayıt bulunamadığı için silme işlemi gerçekleştirilemez!" });
                case OperationStatus.ForeignKeyConflict:
                    return Json(new { success = false, message = "Bu Personel izni başka bir tabloda kullanıldığı için silinemez!" });
                case OperationStatus.GlobalError:
                default:
                    var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                    _manager.LoggerService.LogError($"Area:Admin - Controller:Personel - Action: SilPersonelAyrilis - User: {username} - Hata: Personel İzni : {PersonelAyrilisID} Silinemedi!");
                    return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
        public async Task<IActionResult> GetirPersonelIzinSafahatBilgi([FromRoute] short id)
        {
            try
            {
                var safahatBilgi = await _manager.PersonelService.TGetPersonelIzinAuditTrailWithDetailsAsync(id);
                if (safahatBilgi == null)
                {
                    return Json(new { success = false });
                }
                return PartialView("~/Views/Shared/Partials/_SafahatBilgiPartial.cshtml", safahatBilgi);
            }
            catch (Exception ex)
            {
                var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Unknown User";
                _manager.LoggerService.LogError($"Area:Admin - Controller:Personel - Action: GetirSafahatBilgi - User: {username} - Hata: {ex.Message}");
                return Json(new { success = false });
            }
        }
        [HttpGet]
        public async Task<IActionResult> PersonelAyrilislari(short PersonelID)
        {
            var personel = await _manager.PersonelService.TGetPersonelByIdAsync(PersonelID, false);
            var personelDto = _mapper.Map<ResultPersonelDto>(personel);
            var viewModel = new PersonelAyrilisViewModel
            {
                YeniPersonelAyrilis = new CreatePersonelAyrilisDto(), // Boş bir DTO örneği
                GuncellePersonelAyrilis = new UpdatePersonelAyrilisDto(), // Boş bir DTO örneği
                ListelePersonelAyrilis = personelDto // Personel bilgisi
            };
            // ViewModel ile View'a gönder
            return View(viewModel);
        }

    }
}

