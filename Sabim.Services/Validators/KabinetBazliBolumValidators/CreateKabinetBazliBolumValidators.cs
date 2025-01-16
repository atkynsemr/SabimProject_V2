using FluentValidation;
using Sabim.Domain.DTOs.KabinetBazliBolumDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.KabinetBazliBolumValidators
{
    public class CreateKabinetBazliBolumValidators : AbstractValidator<CreateKabinetBazliBolumDto>
    {
        private readonly IServiceManager _manager;
        public CreateKabinetBazliBolumValidators(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.KabinetBazliBolumAdi)
                         .NotEmpty().WithMessage("Kabinet Bazlı Bölüm Adı boş bırakılamaz.")
                         .MaximumLength(35).WithMessage("Kabinet Bazlı Bölüm Adı en fazla 35 karakter olmalıdır.")
                         .MinimumLength(5).WithMessage("Kabinet Bazlı Bölüm Adı en az 5 karakter olmalıdır.")
                         .Must(BeUniqueName).WithMessage("Aynı kabinet bazlı bölüm adı zaten kayıtlı!");
        }
        private bool BeUniqueName(string kabinetBazliBolumAdi)
        {
            return !_manager.KabinetBazliBolumService.TIsAny(x => x.KabinetBazliBolumAdi == kabinetBazliBolumAdi);
        }
    }
}
