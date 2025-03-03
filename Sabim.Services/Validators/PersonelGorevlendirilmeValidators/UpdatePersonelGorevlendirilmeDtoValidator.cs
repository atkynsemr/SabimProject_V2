using FluentValidation;
using Sabim.Domain.DTOs.PersonelGorevlendirilmeDtos;

namespace Sabim.Services.Validators.PersonelGorevlendirilmeValidators
{
    public class UpdatePersonelGorevlendirilmeDtoValidator : AbstractValidator<UpdatePersonelGorevlendirilmeDto>
    {
        public UpdatePersonelGorevlendirilmeDtoValidator()
        {
            RuleFor(x => x.PersonelGorevlendirilmeID).NotEmpty().WithMessage("Personel Görevlendirilme ID alanı boş bırakılamaz.");
            RuleFor(x => x.PersonelId).NotEmpty().WithMessage("Personel alanı boş bırakılamaz.");
            RuleFor(x => x.KisimId).NotEmpty().WithMessage("Kısım alanı boş bırakılamaz.");
            RuleFor(x => x.GorevlendirilmeTipiId).NotEmpty().WithMessage("Görevlendirilme Tipi boş bırakılamaz.");
            RuleFor(x => x.DurumId).NotEmpty().WithMessage("Durum alanı boş bırakılamaz.");
            RuleFor(x => x.AsilGorevlendirilmeYeriMi).NotNull().WithMessage("Asıl görevlendirilme yeri mi? bilgisi boş olamaz.");
            RuleFor(x => x.GorevlendirilmeAktifMi).NotNull().WithMessage("Görevlendirilme aktif mi? bilgisi boş olamaz.");
            RuleFor(x => x.GorevlendirilmeBaslangicTarihi).NotEmpty().WithMessage("Görevlendirilme başlangıç tarihi boş olamaz.");
            RuleFor(x => x.GorevlendirilmeBaslangicTarihi).LessThanOrEqualTo(x => x.GorevlendirilmeBitisTarihi.Value).When(x => x.GorevlendirilmeBitisTarihi.HasValue).WithMessage("Başlangıç tarihi, bitiş tarihinden sonra olamaz.");
        }
    }
}
