using FluentValidation;
using Sabim.Domain.DTOs.CalismaDurumuDtos;

namespace Sabim.Services.Validators.CalismaDurumuValidators
{
    public class CalismaDurumuBaseDtoValidator : AbstractValidator<CalismaDurumuBaseDto>
    {
        public CalismaDurumuBaseDtoValidator()
        {
            RuleFor(x => x.CalismaDurumAdi)
               .NotEmpty().WithMessage("Çalışma Durum Adı boş bırakılamaz.")
               .MaximumLength(50).WithMessage("Çalışma Durum Adı en fazla 50 karakter olmalıdır.")
               .MinimumLength(6).WithMessage("Çalışma Durum Adı en az 6 karakter olmalıdır.");
            RuleFor(x => x.KurumPersonelListesineDahilMi)
                .NotNull().WithMessage("Kurum Personel Listesine Dahil Mi alanı boş olamaz.")
                .Must(value => value == true || value == false).WithMessage("Kurum Personel Listesine Dahil Mi alanı aktif veya pasif olmalıdır.");
            RuleFor(x => x.DurumId)
                .NotEmpty().WithMessage("Durum Id alanı boş olamaz.");
            RuleFor(x => x.DurumAdi)
                .NotEmpty().WithMessage("Durum Adı boş bırakılamaz.")
                .MaximumLength(30).WithMessage("Durum Adı en fazla 30 karakter olmalıdır.")
                .MinimumLength(5).WithMessage("Durum Adı en az 5 karakter olmalıdır."); 
        }
    }
}
