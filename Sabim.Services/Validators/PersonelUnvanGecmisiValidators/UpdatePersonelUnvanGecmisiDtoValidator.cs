using FluentValidation;
using Sabim.Domain.DTOs.PersonelUnvanGecmisiDtos;

namespace Sabim.Services.Validators.PersonelUnvanGecmisiValidators
{
    public class UpdatePersonelUnvanGecmisiDtoValidator : AbstractValidator<UpdatePersonelUnvanGecmisiDto>
    {
        public UpdatePersonelUnvanGecmisiDtoValidator()
        {
            RuleFor(x => x.PersonelUnvanGecmisiID).NotEmpty().WithMessage("Personel Unvan Gecmisi ID alanı boş bırakılamaz.");
            RuleFor(x => x.UnvanId).NotEmpty().WithMessage("Unvan alanı boş bırakılamaz.");
            RuleFor(x => x.PersonelId).NotEmpty().WithMessage("Personel Id alanı boş geçilemez.");
            RuleFor(x => x.UnvanaSahipOlduguTarih).NotNull().WithMessage("Unvana sahip olduğu tarih boş bırakılamaz.");
            RuleFor(x => x.UnvanDegisimTarihi)
            .Must((model, UnvanDegisimTarihi) =>
            !UnvanDegisimTarihi.HasValue || model.UnvanaSahipOlduguTarih < UnvanDegisimTarihi)
            .WithMessage("Unvana Sahip Olduğu Tarih, Unvan Değişim Tarihinden önce bir tarih olmalıdır.");
        }
    }
}
