using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sabim.Web.Helpers.HtmlHelpers
{
    public static class ToastrHelper
    {
        public static IHtmlContent Toastr(this IHtmlHelper htmlHelper, string message, string title = null, string type = "info")
        {
            if (string.IsNullOrEmpty(message) || string.IsNullOrEmpty(type))
                return HtmlString.Empty;

            // Türden bağımsız varsayılan başlıklar
            title ??= type.ToLower() switch
            {
                "success" => "Başarılı",
                "error" => "Hata",
                "warning" => "Uyarı",
                "info" => "Bilgi",
                _ => "Bilgi" // Geçersiz tür durumunda varsayılan
            };

            var toastrScript = $@"
            <script>
                $(document).ready(function() {{
                    toastr.options = {{
                        ""closeButton"": true,
                        ""debug"": false,
                        ""newestOnTop"": false,
                        ""progressBar"": true,
                        ""positionClass"": ""toast-top-center"",
                        ""preventDuplicates"": false,
                        ""onclick"": null,
                        ""showDuration"": ""300"",
                        ""hideDuration"": ""1000"",
                        ""timeOut"": ""4000"",
                        ""extendedTimeOut"": ""1000"",
                        ""showEasing"": ""swing"",
                        ""hideEasing"": ""linear"",
                        ""showMethod"": ""fadeIn"",
                        ""hideMethod"": ""fadeOut""
                    }};
                    toastr.{type.ToLower()}('{message}', '{title}');
                }});
            </script>";
            return new HtmlString(toastrScript);
        }
    }
}
