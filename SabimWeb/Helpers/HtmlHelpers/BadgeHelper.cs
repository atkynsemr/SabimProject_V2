namespace Sabim.Web.Helpers.HtmlHelpers
{
    public class BadgeHelper
    {
        public static string GetBadgeClass(short id)
        {
            return id switch
            {
                1 => "bg-label-success",   
                2 => "badge-lg bg-label-secondary",  
                3 => "bg-label-danger",   
                _ => "bg-label-warning"
            };
        }

        public static string GetBadge(bool isActive)
        {
            var badgeClass = isActive ? "badge-lg bg-label-success badge-success" : "badge-lg bg-label-secondary badge-secondary";
            var badgeText = isActive ? "Aktif" : "Pasif";
            return $"<span class=\"badge {badgeClass}\">{badgeText}</span>";
        }
        public static string GetBadgeStatus(bool isActive)
        {
            var badgeClass = isActive ? "badge-lg bg-label-success badge-success" : "badge-lg bg-label-secondary badge-secondary";
            var badgeText = isActive ? "Evet" : "Hayır";
            return $"<span class=\"badge {badgeClass}\">{badgeText}</span>";
        }
    }
}
