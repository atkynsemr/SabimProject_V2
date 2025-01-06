using FluentValidation;
using Sabim.Domain.DTOs.KadroTuruDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.KadroTuruValidators
{
    public class CreateKadroTuruDtoValidator : AbstractValidator<CreateKadroTuruDto>
    {
        private readonly IServiceManager _manager;
        public CreateKadroTuruDtoValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.KadroTuruAdi)
                   .NotEmpty().WithMessage("Kadro Türü Adı boş bırakılamaz.")
                   .MaximumLength(50).WithMessage("Kadro Türü Adı en fazla 50 karakter olmalıdır.")
                   .MinimumLength(5).WithMessage("Kadro Türü Adı en az 5 karakter olmalıdır.")
                   .Must(BeUniqueName).WithMessage("Aynı kadro türü  adı zaten kayıtlı!");
        }
        private bool BeUniqueName(string kadroTuruAdi)
        {
            return !_manager.KadroTuruService.TIsAny(x => x.KadroTuruAdi == kadroTuruAdi);
        }
    }
}
