using FluentValidation;
using Sabim.Domain.DTOs.MalzemeCinsiDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.MalzemeCinsiValidators
{
    public class UpdateMalzemeCinsiValidator : AbstractValidator<UpdateMalzemeCinsiDto>
    {
        private readonly IServiceManager _manager;

        public UpdateMalzemeCinsiValidator(IServiceManager manager)
        {
            _manager = manager;

            RuleFor(x => x.MalzemeCinsiAdi)
                .NotEmpty().WithMessage("Malzeme Cinsi adı boş bırakılamaz.")
                .MaximumLength(25).WithMessage("Malzeme Cinsi adı en fazla 25 karakter olmalıdır.")
                .MinimumLength(3).WithMessage("Malzeme Cinsi adı en az 3 karakter olmalıdır.")
                .Must((dto, malzemeCinsiAdi) => BeUniqueName(malzemeCinsiAdi, dto.MalzemeCinsiID)).WithMessage("Bu malzeme cinsi zaten kayıtlı!");

            RuleFor(x => x.MalzemeTuruId)
                .NotEmpty().WithMessage("Malzeme Türü ID boş bırakılamaz.");

            RuleFor(x => x.DurumId)
                .NotNull().WithMessage("Durum alanı boş olamaz.");
        }

        private bool BeUniqueName(string malzemeCinsiAdi, byte? excludeId)
        {
            return !_manager.MalzemeCinsiService.TIsAny(x => x.MalzemeCinsiAdi == malzemeCinsiAdi && x.MalzemeCinsiID != excludeId);
        }
    }
}
