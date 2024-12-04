using FluentValidation;
using Sabim.Domain.DTOs.UnvanDtos;

namespace Sabim.Services.Validators.UnvanValidators
{
    public class UnvanBaseDtoValidator : AbstractValidator<UnvanBaseDto>
    {
        public UnvanBaseDtoValidator()
        {
            RuleFor(x => x.UnvanAdi)
                .NotEmpty().WithMessage("Ünvan Adı boş bırakılamaz.")
                .MaximumLength(60).WithMessage("Ünvan Adı en fazla 60 karakter olmalıdır.")
                .MinimumLength(4).WithMessage("Ünvan Adı en az 4 karakter olmalıdır.");
            RuleFor(x=>x.OncelikSirasi)
                .NotEmpty().WithMessage("Öncelik Sırası boş bırakılamaz.")
                .InclusiveBetween((byte)1, (byte)100).WithMessage("Öncelik Sırası 1 ile 100 arasında olmalıdır.");
            RuleFor(x => x.DurumId)
                .NotEmpty().WithMessage("Durum Id alanı boş olamaz.");
            RuleFor(x => x.DurumAdi)
                .NotEmpty().WithMessage("Durum Adı boş bırakılamaz.")
                .MaximumLength(30).WithMessage("Durum Adı en fazla 30 karakter olmalıdır.")
                .MinimumLength(5).WithMessage("Durum Adı en az 5 karakter olmalıdır.");
        }
    }
}
