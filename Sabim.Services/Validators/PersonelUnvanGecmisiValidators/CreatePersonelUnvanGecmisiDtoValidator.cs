using FluentValidation;
using Sabim.Domain.DTOs.PersonelUnvanGecmisiDtos;

namespace Sabim.Services.Validators.PersonelUnvanGecmisiValidators
{
    public class CreatePersonelUnvanGecmisiDtoValidator : AbstractValidator<CreatePersonelUnvanGecmisiDto>
    {
        public CreatePersonelUnvanGecmisiDtoValidator()
        {
            RuleFor(x => x.UnvanId).NotEmpty().WithMessage("Unvan alanı boş bırakılamaz.");
            RuleFor(x => x.PersonelId).NotEmpty().WithMessage("Personel Id alanı boş geçilemez.");
            RuleFor(x => x.UnvanaSahipOlduguTarih).NotEmpty().WithMessage("Unvana sahip olduğu tarih boş bırakılamaz.");
            RuleFor(x => x.UnvanDegisimTarihi)
            .Must((model, UnvanDegisimTarihi) =>
            !UnvanDegisimTarihi.HasValue || model.UnvanaSahipOlduguTarih < UnvanDegisimTarihi)
            .WithMessage("Unvana Sahip Olduğu Tarih, Unvan Değişim Tarihinden önce bir tarih olmalıdır.");
        }
    }
}
