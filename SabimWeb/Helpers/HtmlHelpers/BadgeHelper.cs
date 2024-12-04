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
    }
}
