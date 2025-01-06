using FluentValidation;
using Sabim.Domain.DTOs.DurumDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.DurumValidators
{
    public class CreateSehirDtoValidator : AbstractValidator<CreateDurumDto>
    {
        private readonly IServiceManager _manager;
        public CreateSehirDtoValidator(IServiceManager manager )
        {
            _manager = manager;
            // Validation kurallarını burada tanımlıyoruz.
            RuleFor(x => x.DurumAdi)
                .NotEmpty().WithMessage("Durum Adı boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Durum Adı en az 3 karakter olmalıdır.")
                .MaximumLength(30).WithMessage("Durum Adı en fazla 30 karakter olmalıdır.")
                .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ\\s]+$").WithMessage("Durum Adı yalnızca harf içermelidir.")
                .Must(BeUniqueName).WithMessage("Aynı durum adı zaten kayıtlı!");
        }
        private bool BeUniqueName(string durumAdi)
        {
            return ! _manager.DurumService.TIsAny(x => x.DurumAdi == durumAdi);
        }
    }
}
