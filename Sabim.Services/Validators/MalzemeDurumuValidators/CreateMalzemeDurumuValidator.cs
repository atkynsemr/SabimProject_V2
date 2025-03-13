using FluentValidation;
using Sabim.Domain.DTOs.MalzemeDurumuDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.MalzemeDurumuValidators
{
    public class CreateMalzemeDurumuValidator : AbstractValidator<CreateMalzemeDurumuDto>
    {
        private readonly IServiceManager _manager;

        public CreateMalzemeDurumuValidator(IServiceManager manager)
        {
            _manager = manager;

            RuleFor(x => x.MalzemeDurumuAdi)
                .NotEmpty().WithMessage("Malzeme Durumu adı boş bırakılamaz.")
                .MaximumLength(20).WithMessage("Malzeme Durumu adı en fazla 20 karakter olmalıdır.")
                .MinimumLength(3).WithMessage("Malzeme Durumu adı en az 3 karakter olmalıdır.")
                .Must(BeUniqueName).WithMessage("Bu malzeme durumu zaten kayıtlı!");

            RuleFor(x => x.DurumId)
                .NotNull().WithMessage("Durum alanı boş olamaz.");
        }

        private bool BeUniqueName(string malzemeDurumuAdi)
        {
            return !_manager.MalzemeDurumuService.TIsAny(x => x.MalzemeDurumuAdi == malzemeDurumuAdi);
        }
    }
}
