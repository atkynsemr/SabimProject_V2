using FluentValidation;
using Sabim.Domain.DTOs.MalzemeTuruDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.MalzemeTuruValidators
{
    public class UpdateMalzemeTuruValidator : AbstractValidator<UpdateMalzemeTuruDto>
    {
        private readonly IServiceManager _manager;

        public UpdateMalzemeTuruValidator(IServiceManager manager)
        {
            _manager = manager;

            RuleFor(x => x.TurAdi)
                .NotEmpty().WithMessage("Malzeme Türü adı boş bırakılamaz.")
                .MaximumLength(30).WithMessage("Malzeme Türü adı en fazla 30 karakter olmalıdır.")
                .MinimumLength(3).WithMessage("Malzeme Türü adı en az 3 karakter olmalıdır.")
                .Must((dto, turAdi) => BeUniqueName(turAdi, dto.MalzemeTuruID)).WithMessage("Bu malzeme türü zaten kayıtlı!");

            RuleFor(x => x.DurumId)
                .NotNull().WithMessage("Durum alanı boş olamaz.");
        }

        private bool BeUniqueName(string turAdi, byte? excludeId)
        {
            return !_manager.MalzemeTuruService.TIsAny(x => x.TurAdi == turAdi && x.MalzemeTuruID != excludeId);
        }
    }
}
