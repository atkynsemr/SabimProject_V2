using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Sabim.Web.Helpers.MethodHelper
{
    public class ValidationHelper
    {
        //public static object GetModelErrors(ModelStateDictionary modelState)
        //{
        //    var errors = modelState
        //        .Where(x => x.Value.Errors.Count > 0)
        //        .ToDictionary(
        //            x => x.Key,
        //            x => x.Value.Errors.Select(e => e.ErrorMessage).ToArray()
        //        );
        //    //var errors = modelState.Values
        //    //                 .SelectMany(v => v.Errors)
        //    //                 .Select(e => e.ErrorMessage)
        //    //                 .ToList();
        //    return new { errors };
        //}
        public static object GetModelErrors(ModelStateDictionary modelState)
        {
            return modelState
                .Where(ms => ms.Value.Errors.Count > 0) // Hatalı alanları filtrele
                .ToDictionary(
                    kvp => kvp.Key, // Alan adı
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray() // Hata mesajları
                );
        }
    }
}
