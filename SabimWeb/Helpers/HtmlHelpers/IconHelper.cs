namespace Sabim.Web.Helpers.HtmlHelpers
{
    public static class IconHelper
    {
        public static string GetAddIcon()
        {
            return "<i class='fa fa-plus-circle' title='Ekle'></i>";
        }
        public static string GetInfoIcon()
        {
            return "<i class='fa fa-info-circle text-info' title='Göster'></i>";
        }
        public static string GetEditIcon()
        {
            return "<i class='fas fa-pen-alt text-orange' title='Düzenle'></i>";
        }
        public static string GetDeleteIcon()
        {
            return "<i class='far fa-trash-alt text-danger' title='Sil'></i>";
        }
        public static string GetStatusIcon(bool isActive)
        {
            return isActive
                ? "<i class='far fa-check-circle text-success' title='Aktif'></i>"
                : "<i class='fa fa-minus-circle text-secondary' title='Pasif'></i>";
        }
        public static string GetStatusNameIcon(string durumAdi)
        {
            return durumAdi switch
            {
                "Aktif" => "<i class='far fa-check-circle text-success' title='Aktif'></i>",
                "Pasif" => "<i class='fa fa-minus-circle text-secondary' title='Pasif'></i>",
                "Beklemede" => "<i class='far fa-hourglass text-warning' title='Beklemede'></i>",
                "Silinmiş" => "<i class='far fa-times-circle text-danger' title='Pasif'></i>",
                _ => "<i class='far fa-question-circle text-secondary' title='Bilinmiyor'></i>"
            };
        }
        public static string GetGenderIcon(string cinsiyetAdi)
        {
            return cinsiyetAdi switch
            {
                "Erkek" => "<i class='fas fa-male text-primary fa-lg mr-1'></i>",
                "Kadın" => "<i class='fas fa-female text-pink fa-lg mr-1'></i>",
                _ => "<i class='fa fa-users text-secondary mr-1'></i>"
            };
        }
        //Modal Çağırırken Kullanılacak
        public static string CreateButton(string buttonClass, string title, string iconHtml, string tooltip, string modalId)
        {
            return $"<button type='button' class='{buttonClass}' title='{tooltip}' data-toggle='modal' data-target='#{modalId}'>{iconHtml} {title}</button>";
        }
        //Modal Çağırırken Kullanılacak
        public static string CreateLink(string linkClass, string title, string iconHtml, string tooltip, string modalId, string dataId)
        {
            return $"<a href='#' class='{linkClass}' title='{tooltip}' data-toggle='modal' data-target='#{modalId}' data-id='{dataId}'>{iconHtml} {title}</a>";
        }
        public static string CreateActionButton(string buttonClass, string title,
            string iconHtml, string tooltip,
            string javascriptFunctionName, string parameter)
        {
            return $"<button type='button' class='{buttonClass}' title='{tooltip}' " +
                $"onclick=\"{javascriptFunctionName}('{parameter}'); return false;\">" +
                $"{iconHtml} {title}</button>";
        }
        public static string CreateActionLink(string linkClass, string title,
            string iconHtml, string tooltip,
            string javascriptFunctionName, string parameter)
        {
            return $"<a href='#' class='{linkClass}' title='{tooltip}' " +
                   $"onclick=\"{javascriptFunctionName}('{parameter}'); return false;\">" +
                   $"{iconHtml} {title}</a>";
        }


    }
}
