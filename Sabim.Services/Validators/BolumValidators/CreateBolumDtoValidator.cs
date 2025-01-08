using FluentValidation;
using Sabim.Domain.DTOs.BolumDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.BolumValidators
{
    public class CreateBolumDtoValidator : AbstractValidator<CreateBolumDto>
    {
        private readonly IServiceManager _manager;
        public CreateBolumDtoValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.BolumAdi)
               .NotEmpty().WithMessage("Bölüm Adı boş bırakılamaz.")
               .MaximumLength(50).WithMessage("Bölüm Adı en fazla 50 karakter olmalıdır.")
               .MinimumLength(5).WithMessage("Bölüm Adı en az 5 karakter olmalıdır.")
               .Must(BeUniqueName).WithMessage("Aynı bölüm adı zaten kayıtlı!");
        }
        private bool BeUniqueName(string bolumAdi)
        {
            return !_manager.BolumService.TIsAny(x => x.BolumAdi == bolumAdi);
        }
    }
}
