using FluentValidation;
using Sabim.Domain.DTOs.GorevlendirilmeTipiDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.GorevlendirilmeTipiValidators
{
    public class GorevlendirilmeTipiBaseDtoValidator : AbstractValidator<GorevlendirilmeTipiBaseDto>
    {
        private readonly IServiceManager _manager;
        public GorevlendirilmeTipiBaseDtoValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.GorevlendirilmeTipiAciklama)
               .NotEmpty().WithMessage("Görevlendirilme Tipi Adı boş bırakılamaz.")
               .MaximumLength(40).WithMessage("Görevlendirilme Tipi Adı en fazla 40 karakter olmalıdır.")
               .MinimumLength(5).WithMessage("Görevlendirilme Tipi Adı en az 5 karakter olmalıdır.")
               .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ\\s]+$").WithMessage("Görevlendirilme Tür Adı yalnızca harf içermelidir.")
               .Must(BeUniqueName).WithMessage("Aynı görevlendirilme tür adı zaten kayıtlı!");
        }
        private bool BeUniqueName(string gorevlendirilmeTipiAciklama)
        {
            return !_manager.GorevlendirilmeTipiService.TIsAny(x => x.GorevlendirilmeTipiAciklama == gorevlendirilmeTipiAciklama);
        }
    }
}
