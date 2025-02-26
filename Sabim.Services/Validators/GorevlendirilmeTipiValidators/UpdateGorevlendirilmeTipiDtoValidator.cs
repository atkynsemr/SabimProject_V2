using FluentValidation;
using Sabim.Domain.DTOs.GorevlendirilmeTipiDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.GorevlendirilmeTipiValidators
{
    public class UpdateGorevlendirilmeTipiDtoValidator : AbstractValidator<UpdateGorevlendirilmeTipiDto>
    {
        private readonly IServiceManager _manager;

        public UpdateGorevlendirilmeTipiDtoValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.GorevlendirilmeTipiAciklama)
            .NotEmpty().WithMessage("Görevlendirilme Tipi Adı boş bırakılamaz.")
            .MaximumLength(40).WithMessage("Görevlendirilme Tipi Adı en fazla 40 karakter olmalıdır.")
            .MinimumLength(5).WithMessage("Görevlendirilme Tipi Adı en az 5 karakter olmalıdır.")
            .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ\\s]+$").WithMessage("Görevlendirilme Tür Adı yalnızca harf içermelidir.")
            .Must((dto, gorevlendirilmeTipiAciklama) => BeUniqueName(gorevlendirilmeTipiAciklama, (byte)dto.GorevlendirilmeTipiID)).WithMessage("Aynı ayrılış nedeni zaten kayıtlı!");
            RuleFor(x => x.DurumId).NotEmpty().WithMessage("Durum alanı boş bırakılamaz.");
        }
        private bool BeUniqueName(string gorevlendirilmeTipiAciklama, byte? excludeId)
        {
            return !_manager.GorevlendirilmeTipiService.TIsAny(x => x.GorevlendirilmeTipiAciklama == gorevlendirilmeTipiAciklama && x.GorevlendirilmeTipiID != excludeId);
        }
    }
}
