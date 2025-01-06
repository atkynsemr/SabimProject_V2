using Microsoft.AspNetCore.Mvc;

namespace Sabim.Web.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            // Loglama ve ViewBag mesajı için yardımcı yöntem
            var errorMessage = GetErrorMessage(statusCode, out string viewName);
            LogError(statusCode);

            ViewBag.ErrorMessage = errorMessage;
            return View(viewName);
        }

        [Route("Error/500")]
        public IActionResult ServerError()
        {
            const int statusCode = 500;
            var errorMessage = GetErrorMessage(statusCode, out string viewName);
            LogError(statusCode);

            ViewBag.ErrorMessage = errorMessage;
            return View(viewName);
        }

        private void LogError(int statusCode)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Bilinmiyor";
            var requestUrl = HttpContext.Request.Path;

            switch (statusCode)
            {
                case 403:
                    _logger.LogWarning("403 - Yetkisiz erişim. IP: {IpAddress}, URL: {Url}", ipAddress, requestUrl);
                    break;
                case 404:
                    _logger.LogWarning("404 - Sayfa bulunamadı. IP: {IpAddress}, URL: {Url}", ipAddress, requestUrl);
                    break;
                case 500:
                    _logger.LogError("500 - Sunucu hatası. IP: {IpAddress}, URL: {Url}", ipAddress, requestUrl);
                    break;
                default:
                    _logger.LogError("Hata oluştu. Durum Kodu: {StatusCode}, IP: {IpAddress}, URL: {Url}", statusCode, ipAddress, requestUrl);
                    break;
            }
        }

        private string GetErrorMessage(int statusCode, out string viewName)
        {
            // Durum koduna göre hata mesajı ve view adı belirleme
            switch (statusCode)
            {
                case 403:
                    viewName = "403";
                    return "Bu sayfaya erişim yetkiniz bulunmamaktadır.";
                case 404:
                    viewName = "404";
                    return "Aradığınız sayfa bulunamadı.";
                case 500:
                    viewName = "500";
                    return "Sunucuda bir hata oluştu. Lütfen daha sonra tekrar deneyin.";
                default:
                    viewName = "General";
                    return "Beklenmedik bir hata oluştu.";
            }
        }
    }
}
