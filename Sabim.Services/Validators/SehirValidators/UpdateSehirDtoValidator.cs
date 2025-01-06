using FluentValidation;
using Sabim.Domain.DTOs.SehirDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.SehirValidators
{
    public class UpdateSehirDtoValidator : AbstractValidator<UpdateSehirDto>
    {
        private readonly IServiceManager _manager;

        public UpdateSehirDtoValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.SehirID).NotEmpty().WithMessage("Şehir ID boş değer alamaz.");
            RuleFor(x => x.SehirAdi)
                .NotEmpty().WithMessage("Şehir Adı boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Şehir Adı en az 3 karakter olmalıdır.")
                .MaximumLength(20).WithMessage("Şehir Adı en fazla 20 karakter olmalıdır.")
                .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ\\s]+$").WithMessage("Şehir Adı yalnızca harf içermelidir.")
                .Must((dto, sehirAdi) => BeUniqueName(sehirAdi, dto.SehirID)).WithMessage("Aynı şehir adı zaten kayıtlı!");
            RuleFor(x => x.SehirKodu)
                .NotEmpty().WithMessage("Şehir Kodu boş bırakılamaz.")
                .Must(kodu => kodu >= 1 && kodu <= 81).WithMessage("Şehir Kodu 1 ile 81 arasında olmalıdır.")
                .Must((dto, sehirKodu) => BeUniqueCode(sehirKodu,dto.SehirID)).WithMessage("Aynı Şehir Kodu zaten kayıtlı!");
        }
        private bool BeUniqueName(string sehirAdi, short? excludeId)
        {
            return !_manager.SehirService.TIsAny(x => x.SehirAdi == sehirAdi, excludeId);
        }
        private bool BeUniqueCode(byte sehirKodu, short? excludeId)
        {
            return !_manager.SehirService.TIsAny(x => x.SehirKodu == sehirKodu, excludeId);
        }
    }
}
