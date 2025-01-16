using FluentValidation;
using Sabim.Domain.DTOs.UnvanDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.UnvanValidators
{
    public class CreateUnvanDtoValidator : AbstractValidator<CreateUnvanDto>
    {
        private readonly IServiceManager _manager;
        public CreateUnvanDtoValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.UnvanAdi)
              .NotEmpty().WithMessage("Ünvan Adı boş bırakılamaz.")
              .MaximumLength(60).WithMessage("Ünvan Adı en fazla 60 karakter olmalıdır.")
              .MinimumLength(4).WithMessage("Ünvan Adı en az 4 karakter olmalıdır.")
              .Must(BeUniqueName).WithMessage("Aynı ünvan adı zaten kayıtlı!"); 
            RuleFor(x => x.OncelikSirasi)
                .NotEmpty().WithMessage("Öncelik Sırası boş bırakılamaz.")
                .InclusiveBetween((byte)1, (byte)100).WithMessage("Öncelik Sırası 1 ile 100 arasında olmalıdır.");
        }
        private bool BeUniqueName(string unvanAdi)
        {
            return !_manager.UnvanService.TIsAny(x => x.UnvanAdi == unvanAdi);
        }
    }
}
