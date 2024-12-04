using FluentValidation;
using Sabim.Domain.DTOs.KurumDtos;

namespace Sabim.Services.Validators.KurumValidators
{
    public class KurumBaseDtoValidator : AbstractValidator<KurumBaseDto>
    {
        public KurumBaseDtoValidator()
        {
            RuleFor(x => x.KurumAdi)
               .NotEmpty().WithMessage("Kurum Adı boş bırakılamaz.")
               .MaximumLength(100).WithMessage("Kurum Adı en fazla 100 karakter olmalıdır.")
               .MinimumLength(10).WithMessage("Kurum Adı en az 10 karakter olmalıdır.");
            RuleFor(x => x.SehirId)
                .NotEmpty().WithMessage("Şehir Id alanı boş olamaz.");
            RuleFor(x => x.SehirAdi)
               .NotEmpty().WithMessage("Şehir Adı boş bırakılamaz.")
               .MaximumLength(25).WithMessage("Şehir Adı en fazla 25 karakter olmalıdır.")
               .MinimumLength(3).WithMessage("Şehir Adı en az 3 karakter olmalıdır.");
            RuleFor(x => x.KurumTipiId)
                .NotEmpty().WithMessage("Kurum Tipi Id alanı boş olamaz.");
            RuleFor(x => x.KurumTipiAdi)
                .NotEmpty().WithMessage("Kurum Tipi Adı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Kurum Tipi Adı en fazla 50 karakter olmalıdır.")
                .MinimumLength(6).WithMessage("Kurum Tipi Adı en az 6 karakter olmalıdır.");
            RuleFor(x => x.DurumId)
                .NotEmpty().WithMessage("Durum Id alanı boş olamaz.");
            RuleFor(x => x.DurumAdi)
                .NotEmpty().WithMessage("Durum Adı boş bırakılamaz.")
                .MaximumLength(30).WithMessage("Durum Adı en fazla 30 karakter olmalıdır.")
                .MinimumLength(5).WithMessage("Durum Adı en az 5 karakter olmalıdır.");
        }
    }
}
