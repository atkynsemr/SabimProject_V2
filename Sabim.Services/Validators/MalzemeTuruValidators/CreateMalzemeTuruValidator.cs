using FluentValidation;
using Sabim.Domain.DTOs.MalzemeTuruDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.MalzemeTuruValidators
{
    public class CreateMalzemeTuruValidator : AbstractValidator<CreateMalzemeTuruDto>
    {
        private readonly IServiceManager _manager;

        public CreateMalzemeTuruValidator(IServiceManager manager)
        {
            _manager = manager;

            RuleFor(x => x.TurAdi)
                .NotEmpty().WithMessage("Malzeme Türü adı boş bırakılamaz.")
                .MaximumLength(30).WithMessage("Malzeme Türü adı en fazla 30 karakter olmalıdır.")
                .MinimumLength(3).WithMessage("Malzeme Türü adı en az 3 karakter olmalıdır.")
                .Must(BeUniqueName).WithMessage("Bu malzeme türü zaten kayıtlı!");
                RuleFor(x => x.DurumId).NotEmpty().WithMessage("Durum alanı boş bırakılamaz.");
        }

        private bool BeUniqueName(string turAdi)
        {
            return !_manager.MalzemeTuruService.TIsAny(x => x.TurAdi == turAdi);
        }
    }
}
