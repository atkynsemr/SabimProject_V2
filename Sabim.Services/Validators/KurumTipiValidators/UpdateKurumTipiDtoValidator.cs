using FluentValidation;
using Sabim.Domain.DTOs.KurumTipiDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.KurumTipiValidators
{
    public class UpdateKurumTipiDtoValidator : AbstractValidator<UpdateKurumTipiDto>
    {
        private readonly IServiceManager _manager;

        public UpdateKurumTipiDtoValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.KurumTipiAdi)
               .NotEmpty().WithMessage("Kurum Tipi Adı boş bırakılamaz.")
               .MaximumLength(50).WithMessage("Kurum Tipi Adı en fazla 50 karakter olmalıdır.")
               .MinimumLength(5).WithMessage("Kurum Tipi Adı en az 5 karakter olmalıdır.")
               .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ\\s]+$").WithMessage("Kurum Tipi yalnızca harf içermelidir.")
               .Must((dto, kurumTipiAdi) =>BeUniqueName(kurumTipiAdi, dto.KurumTipiID)).WithMessage("Aynı kurum tipi adı zaten kayıtlı!");
        }
        private bool BeUniqueName(string kurumTipiAdi, short? excludeId)
        {
            return !_manager.KurumTipiService.TIsAny(x => x.KurumTipiAdi == kurumTipiAdi,excludeId);
        }
    }
}
