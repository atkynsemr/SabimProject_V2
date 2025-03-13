using FluentValidation;
using Sabim.Domain.DTOs.MalzemeCinsiDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.MalzemeCinsiValidators
{
    public class CreateMalzemeCinsiValidator : AbstractValidator<CreateMalzemeCinsiDto>
    {
        private readonly IServiceManager _manager;

        public CreateMalzemeCinsiValidator(IServiceManager manager)
        {
            _manager = manager;

            RuleFor(x => x.MalzemeCinsiAdi)
                .NotEmpty().WithMessage("Malzeme Cinsi adı boş bırakılamaz.")
                .MaximumLength(25).WithMessage("Malzeme Cinsi adı en fazla 25 karakter olmalıdır.")
                .MinimumLength(3).WithMessage("Malzeme Cinsi adı en az 3 karakter olmalıdır.")
                .Must(BeUniqueName).WithMessage("Bu malzeme cinsi zaten kayıtlı!");

            RuleFor(x => x.MalzemeTuruId)
                .NotEmpty().WithMessage("Malzeme Türü ID boş bırakılamaz.");

            RuleFor(x => x.DurumId)
                .NotNull().WithMessage("Durum alanı boş olamaz.");
        }

        private bool BeUniqueName(string malzemeCinsiAdi)
        {
            return !_manager.MalzemeCinsiService.TIsAny(x => x.MalzemeCinsiAdi == malzemeCinsiAdi);
        }
    }
}
