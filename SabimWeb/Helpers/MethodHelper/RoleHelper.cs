using Microsoft.AspNetCore.Mvc;

namespace Sabim.Web.Helpers
{
    public static class RoleHelper
    {
        public static string GetRedirectUrl(string userRole, IUrlHelper urlHelper)
        {
            switch (userRole)
            {
                case "Admin":
                    return urlHelper.Action("Index", "Dashboard", new { area = "Admin" });
                case "Bilgi İşlem Müdür":
                    return urlHelper.Action("Index", "Talepler", new { area = "BilgiIslem" });                
                case "Bilgi İşlem Teknik Personel":
                    return urlHelper.Action("Index", "Talepler", new { area = "BilgiIslem" });
                case "Bilgi İşlem Uzman Kullanıcı":
                    return urlHelper.Action("Index", "Talepler", new { area = "BilgiIslem" });
                default:
                    return urlHelper.Action("Index", "TalepListesi", new { area = "User" });
            }
        }
    }
}
