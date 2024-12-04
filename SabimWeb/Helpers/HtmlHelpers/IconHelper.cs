namespace Sabim.Web.Helpers.HtmlHelpers
{
    public static class IconHelper
    {
        public static string GetEditIcon()
        {
            return "<i class='fas fa-edit text-primary' title='Düzenle'></i>";
        }

        public static string GetDeleteIcon()
        {
            return "<i class='far fa-trash-alt text-danger' title='Sil'></i>";
        }
        public static string GetStatusIcon(bool isActive)
        {
            return isActive
                ? "<i class='far fa-check-circle text-success' title='Aktif'></i>"
                : "<i class='far fa-times-circle text-danger' title='Pasif'></i>";
        }
    }
}
