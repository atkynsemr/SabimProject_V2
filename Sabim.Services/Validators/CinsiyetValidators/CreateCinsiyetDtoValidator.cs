using FluentValidation;
using Sabim.Domain.DTOs.CinsiyetDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.CinsiyetValidators
{
    public class CreateCinsiyetDtoValidator : AbstractValidator<CreateCinsiyetDto>
    {
        private readonly IServiceManager _manager;
        public CreateCinsiyetDtoValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.CinsiyetAdi)
                .NotEmpty().WithMessage("Cinsiyet Adı boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Cinsiyet Adı en az 3 karakter olmalıdır.")
                .MaximumLength(15).WithMessage("Cinsiyet Adı en fazla 15 karakter olmalıdır.")
                .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ\\s]+$").WithMessage("Cinsiyet Adı yalnızca harf içermelidir.")
                .Must(BeUniqueName).WithMessage("Aynı cinsiyet adı zaten kayıtlı!");
        }
        private bool BeUniqueName(string cinsiyetAdi)
        {
            return !_manager.CinsiyetService.TIsAny(x => x.CinsiyetAdi == cinsiyetAdi);
        }
    }
}
