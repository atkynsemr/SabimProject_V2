using FluentValidation;
using Sabim.Domain.DTOs.CinsiyetDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.CinsiyetValidators
{
    public class UpdateCinsiyetDtoValidator : AbstractValidator<UpdateCinsiyetDto>
    {
        private readonly IServiceManager _manager;
        public UpdateCinsiyetDtoValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.CinsiyetID).NotEmpty().WithMessage("Cinsiyet ID boş değer alamaz.");
            RuleFor(x => x.CinsiyetAdi)
                .NotEmpty().WithMessage("Cinsiyet Adı boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Cinsiyet Adı en az 3 karakter olmalıdır.")
                .MaximumLength(15).WithMessage("Cinsiyet Adı en fazla 15 karakter olmalıdır.")
                .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ\\s]+$").WithMessage("Cinsiyet Adı yalnızca harf içermelidir.")
                .Must((dto, cinsiyetAdi) => BeUniqueName(cinsiyetAdi, dto.CinsiyetID)).WithMessage("Aynı cinsiyet adı zaten kayıtlı!");
        }
        private bool BeUniqueName(string cinsiyetAdi, short? excludeId)
        {
            return !_manager.CinsiyetService.TIsAny(x => x.CinsiyetAdi == cinsiyetAdi, excludeId);
        }
    }
}
