using FluentValidation;
using Sabim.Domain.DTOs.MalzemeDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators
{
    public class UpdateMalzemeValidator : AbstractValidator<UpdateMalzemeDto>
    {
        private readonly IServiceManager _manager;

        public UpdateMalzemeValidator(IServiceManager manager)
        {
            _manager = manager;

            RuleFor(x => x.SeriNumarasi)
                .NotEmpty().WithMessage("Seri Numarası boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Seri Numarası en fazla 50 karakter olmalıdır.")
                .MinimumLength(5).WithMessage("Seri Numarası en az 5 karakter olmalıdır.")
                .Must((dto, seriNumarasi) => BeUniqueSerialNumber(seriNumarasi, dto.MalzemeID)).WithMessage("Bu seri numarası zaten kayıtlı!");

            RuleFor(x => x.MalzemeModelId)
                .NotEmpty().WithMessage("Model ID boş bırakılamaz.");

            RuleFor(x => x.MalzemeDurumuId)
                .NotEmpty().WithMessage("Durum ID boş bırakılamaz.");

            RuleFor(x => x.DurumId)
                .NotNull().WithMessage("Durum alanı boş olamaz.");
        }

        private bool BeUniqueSerialNumber(string seriNumarasi, short? excludeId)
        {
            return !_manager.MalzemeService.TIsAny(x => x.SeriNumarasi == seriNumarasi && x.MalzemeID != excludeId);
        }
    }

}
