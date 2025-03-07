using FluentValidation;
using Sabim.Domain.DTOs.PersonelGeciciGorevlendirilmeDtos;

namespace Sabim.Services.Validators.PersonelGeciciGorevlendirilmeValidators
{
    public class UpdatePersonelGeciciGorevlendirilmeDtoValidator : AbstractValidator<UpdatePersonelGeciciGorevlendirilmeDto>
    {
        public UpdatePersonelGeciciGorevlendirilmeDtoValidator()
        {
            RuleFor(x => x.PersonelGeciciGorevlendirilmeID).NotEmpty().WithMessage("Personel Gecici Gorevlendirilme ID boş değer alamaz.");
            RuleFor(x => x.PersonelId).NotEmpty().WithMessage("Personel boş olamaz.").GreaterThan((short)0).WithMessage("Personel ID geçerli bir değer olmalıdır.");
            RuleFor(x => x.GorevlendirilmeTipiId).NotEmpty().WithMessage("Görevlendirme Tipi boş olamaz.").GreaterThan((short)0).WithMessage("Görevlendirme Tipi ID geçerli bir değer olmalıdır.");
            RuleFor(x => x.BaslangicTarihi).NotEmpty().WithMessage("Başlangıç Tarihi boş olamaz.");
            RuleFor(x => x.BitisTarihi).GreaterThan(x => x.BaslangicTarihi).When(x => x.BitisTarihi.HasValue).WithMessage("Bitiş Tarihi, Başlangıç Tarihi'nden küçük olamaz.");
            RuleFor(x => x.KurumId).NotEmpty().WithMessage("Personel Görevlendirilme Kurumu boş olamaz.").GreaterThan((short)0).WithMessage("Kurum ID geçerli bir değer olmalıdır.");
            //RuleFor(x => x.PersonelAyrilisYeriId).NotEmpty().WithMessage("Personel Görevlendirilme Yeri boş olamaz.").GreaterThan((short)0).WithMessage("Personel Ayrılış Yeri ID geçerli bir değer olmalıdır.");
        }
    }
}
