using Microsoft.AspNetCore.Mvc;
using Sabim.Domain.DTOs.PersonelAyrilisNedenleriDtos;
using Sabim.Services.Contracts;

namespace Sabim.Web.Areas.Admin.ViewComponents.PersonelAyrilisNedenleriComponents
{
    public class _PersonelAyrilisNedenleriComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        public _PersonelAyrilisNedenleriComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync(string? deger, bool? kaliciAyrilisMi, int? selectedPersonelAyrilisNedenleriId = null)
        {
            var personelAyrilisNedenleri = await _manager.PersonelAyrilisNedenleriService.TFindAllByConditionAsync(x=>x.KaliciAyrilisMi== kaliciAyrilisMi, false);
            ViewBag.Deger = deger;
            var personelAyrilisNedenleriDtoList = personelAyrilisNedenleri.Select(k => new ResultPersonelAyrilisNedenleriDto
            {
                PersonelAyrilisNedenleriID = k.PersonelAyrilisNedenleriID,
                Aciklama = k.Aciklama,
                KaliciAyrilisMi = k.KaliciAyrilisMi,
                DonanimUyarisi = k.DonanimUyarisi,
                Selected = selectedPersonelAyrilisNedenleriId.HasValue && selectedPersonelAyrilisNedenleriId.Value == k.PersonelAyrilisNedenleriID
            }).ToList();
            return View(personelAyrilisNedenleriDtoList);
        }
    }
}
