using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.AppUserDtos;
using Sabim.Domain.Entities;
using Sabim.Services.Contracts;
using Sabim.Web.Helpers;
using Sabim.Web.Helpers.MethodHelper;
using System.Security.Claims;

namespace Sabim.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceManager _manager;
        private readonly AuthenticationHelper _authenticationHelper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public AccountController(IServiceManager manager, IHttpContextAccessor httpContextAccessor,UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _manager = manager;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _roleManager = roleManager;
            _authenticationHelper = new AuthenticationHelper(new HttpContextAccessor(),_userManager,_roleManager);
        }
        public IActionResult Login([FromQuery(Name = "ReturnUrl")] string returnUrl = "/")
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                var userRole = claimUser.FindFirst(ClaimTypes.Role).ToString();
                if (userRole != null)
                {
                    // RoleHelper kullanımı
                    var redirectUrl = RoleHelper.GetRedirectUrl(userRole, Url);
                    if (!string.IsNullOrEmpty(redirectUrl))
                    {
                        return Redirect(redirectUrl);
                    }
                }
                else
                {
                    return View();
                }
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginAppUserDto loginDto)
        {
            if (!ModelState.IsValid) // Validation başarısızsa
            {
                return View(loginDto); // Hataları view'e gönder
            }
            // Giriş işlemi
            var (success, message, user) = await _manager.AppUserService.TSignInAsync(loginDto);

            if (success)
            {
                // Kullanıcıyı oturum açtırıyoruz
                await _authenticationHelper.SignInAsync(user, loginDto.RememberMe);
                // Kullanıcının rolünü alıyoruz
                var roles = await _userManager.GetRolesAsync(user);
                // İlk rolü alıyoruz
                var role = roles.FirstOrDefault(); 
                // RoleHelper sınıfını kullanarak yönlendirmeyi yapıyoruz
                var redirectUrl = RoleHelper.GetRedirectUrl(role, Url);
                if (string.IsNullOrEmpty(redirectUrl))
                {
                    // Eğer rol tanımlı değilse, hata sayfasına yönlendir
                    return RedirectToAction("Error", "Home");
                }
                return Redirect(redirectUrl); // İlgili yönlendirmeyi yapıyoruz
            }
            else
            {
                ViewBag.Message = message;
                return View(loginDto);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            HttpContext.Response.Cookies.Delete(".AspNetCore.Identity.Application");
            HttpContext.Response.Cookies.Delete("SabimWEBCookie");
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult AccessDenied(string returnUrl = null)
        {
            return View();
        }
    }
}
