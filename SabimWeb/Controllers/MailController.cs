using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.AppUserDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;

namespace Sabim.Web.Controllers
{
    public class MailController : Controller
    {
        private readonly IServiceManager _manager;
        public MailController(IServiceManager manager)
        {
            _manager = manager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous,HttpGet]
        public IActionResult SifremiUnuttum()
        {
            return View();
        }
        [AllowAnonymous, HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SifremiUnuttum(ForgotPasswordDto forgotPasswordDto)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                // E-posta kontrolü
                var user = await _manager.AppUserService.TFindByEmailAsync(forgotPasswordDto.Eposta);
                if (user == null)
                {
                    ViewBag.Message = "Bu e-posta adresine ait bir kullanıcı bulunamadı.";
                    return View(forgotPasswordDto);
                }
                else
                {
                    var resetCode = await _manager.AppUserService.TSavePasswordResetTokenAsync(user, "PasswordReset", "ResetCode");
                    await _manager.EmailService.SendPasswordResetEmailAsync(forgotPasswordDto.Eposta, resetCode);
                    forgotPasswordDto.EmailSent = true;
                    return RedirectToAction(nameof(KurtarmaKodu), new { id = user.Id });
                }
            }
            return View(forgotPasswordDto);
        }
        [AllowAnonymous, HttpGet]
        public  IActionResult KurtarmaKodu(int? id)
        {
            if (id == null || id <= 0)
            {
                return RedirectToAction(nameof(SifremiUnuttum));
            }
            var model = new ResetPasswordDto
            {
                Id = id.Value 
            };
            return View(model);
        }
        [AllowAnonymous, HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KurtarmaKodu([FromForm]ResetPasswordDto resetPasswordDto)
        {
            bool isValidToken = await _manager.AppUserService.TIsValidTokenAsync(resetPasswordDto.Id, resetPasswordDto.recoveryCode);
            if (!isValidToken)
            {
                ViewBag.Message = "Lütfen kurtarma kodunu doğru giriniz.";
                return View(resetPasswordDto);
            }
            // Token geçerli ise bir doğrulama token'ı oluştur ve sakla
            TempData["VerificationToken"] = resetPasswordDto.recoveryCode;
            return RedirectToAction("SifreYenileme", new { id = resetPasswordDto.Id, token = TempData["VerificationToken"] });
        }
        [AllowAnonymous, HttpGet]
        public IActionResult SifreYenileme(int? id, string token)
        {
            if (string.IsNullOrEmpty(token) || id == null || id <= 0)
            {
                return RedirectToAction(nameof(SifremiUnuttum));
            }
            //Token kontrolü yap
            var storedToken = TempData["VerificationToken"];
            if (storedToken == null || !storedToken.Equals(token))
            {
                return RedirectToAction(nameof(SifremiUnuttum));
            }
            var model = new ResetPasswordDto
            {
                Id = id.Value,
                recoveryCode = token
            };
            return View(model);
        }
        [AllowAnonymous, HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SifreYenileme([FromForm] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid) // Validation başarısızsa
            {
                return View(resetPasswordDto); // Hataları view'e gönder
            }
            AppUser user = await _manager.AppUserService.TFindByUserIdAsync(resetPasswordDto.Id);
            if (user == null) {
                //ViewBag.Message = "Kullanıcı Bulunamadı. Bilgi İşlem Birimi ile iletişime geçiniz.";
                return View(resetPasswordDto);
            }
            var resetPassword = await _manager.AppUserService.TResetUserPasswordAsync(user, resetPasswordDto.recoveryCode, resetPasswordDto.NewPassword);
            if (resetPassword.Succeeded) {
                TempData["Message"] = "Şifreniz Değiştirildi. Giriş Yapabilirsiniz";
            }
            else
            {
                ViewBag.Message = "Şifre değiştirme sırasında bir hata oluştu. Lütfen tekrar deneyiniz.";
                return View(resetPasswordDto);
            }
            return RedirectToAction("Login", "Account");
        }
    }
}
