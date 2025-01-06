using FluentValidation;
using Sabim.Domain.DTOs.DurumDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.DurumValidators
{
    public class UpdateDurumDtoValidator : AbstractValidator<UpdateDurumDto>
    {
        private readonly IServiceManager _manager;
        public UpdateDurumDtoValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.DurumID).NotEmpty().WithMessage("Durum ID boş değer alamaz.");
            RuleFor(x => x.DurumAdi).NotEmpty().WithMessage("Durum Adı boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Durum Adı en az 3 karakter olmalıdır.")
                .MaximumLength(30).WithMessage("Durum Adı en fazla 30 karakter olmalıdır.")
                .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ\\s]+$")
                .WithMessage("Durum Adı yalnızca harf içermelidir.")
                .Must((dto, durumAdi) => BeUniqueName(durumAdi, dto.DurumID)).WithMessage("Aynı durum adı zaten kayıtlı!");             
        }
        private bool BeUniqueName(string durumAdi, short? excludeId)
        {
            return !_manager.DurumService.TIsAny(x => x.DurumAdi == durumAdi, excludeId);
        }
    }
}
