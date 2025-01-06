using FluentValidation;
using Sabim.Domain.DTOs.SehirDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.SehirValidators
{
    public class CreateSehirDtoValidator : AbstractValidator<CreateSehirDto>
    {
        private readonly IServiceManager _manager;
        public CreateSehirDtoValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.SehirAdi)
                .NotEmpty().WithMessage("Şehir Adı boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Şehir Adı en az 3 karakter olmalıdır.")
                .MaximumLength(20).WithMessage("Şehir Adı en fazla 20 karakter olmalıdır.")
                .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ\\s]+$").WithMessage("Şehir Adı yalnızca harf içermelidir.")
                .Must(BeUniqueName).WithMessage("Aynı şehir adı zaten kayıtlı!");
            RuleFor(x => x.SehirKodu)
                .NotEmpty().WithMessage("Şehir Kodu boş bırakılamaz.")
                .Must(kodu => kodu >= 1 && kodu <= 81).WithMessage("Şehir Kodu 1 ile 81 arasında olmalıdır.")
                .Must(BeUniqueCode).WithMessage("Aynı Şehir Kodu zaten kayıtlı!");
        }
        private bool BeUniqueName(string sehirAdi)
        {
            return !_manager.SehirService.TIsAny(x => x.SehirAdi == sehirAdi);
        }
        private bool BeUniqueCode(byte sehirKodu)
        {
            return !_manager.SehirService.TIsAny(x => x.SehirKodu == sehirKodu);
        }
    }
}
