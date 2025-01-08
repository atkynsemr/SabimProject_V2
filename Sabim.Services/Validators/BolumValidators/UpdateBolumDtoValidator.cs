using FluentValidation;
using Sabim.Domain.DTOs.BolumDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.BolumValidators
{
    public class UpdateBolumDtoValidator : AbstractValidator<UpdateBolumDto>
    {
        private readonly IServiceManager _manager;
        public UpdateBolumDtoValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.BolumAdi)
               .NotEmpty().WithMessage("Bölüm Adı boş bırakılamaz.")
               .MaximumLength(50).WithMessage("Bölüm Adı en fazla 50 karakter olmalıdır.")
               .MinimumLength(5).WithMessage("Bölüm Adı en az 5 karakter olmalıdır.")
               .Must((dto,bolumAdi)=>BeUniqueName(bolumAdi,dto.BolumID)).WithMessage("Aynı bölüm adı zaten kayıtlı!");
        }
        private bool BeUniqueName(string bolumAdi, short? excludeId)
        {
            return !_manager.BolumService.TIsAny(x => x.BolumAdi == bolumAdi, excludeId);
        }
    }
}
