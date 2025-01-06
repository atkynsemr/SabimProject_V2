using FluentValidation;
using Sabim.Domain.DTOs.CalismaDurumuDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.CalismaDurumuValidators
{
    public class CreateGorevlendirilmeTuruDtoValidator : AbstractValidator<CreateCalismaDurumuDto>
    {
        private readonly IServiceManager _manager;
        public CreateGorevlendirilmeTuruDtoValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.CalismaDurumAdi)
              .NotEmpty().WithMessage("Çalışma Durum Adı boş bırakılamaz.")
              .MaximumLength(85).WithMessage("Çalışma Durum Adı en fazla 85 karakter olmalıdır.")
              .MinimumLength(6).WithMessage("Çalışma Durum Adı en az 6 karakter olmalıdır.")
              .Must(BeUniqueName).WithMessage("Aynı çalışma durum  adı zaten kayıtlı!");
            RuleFor(x => x.KurumPersonelListesineDahilMi)
              .NotNull().WithMessage("Personel Listesinde Gösterilme Durumu alanı boş olamaz.")
              .Must(value => value == true || value == false).WithMessage("Personel Listesinde Gösterilme Durumu alanı aktif veya pasif olmalıdır.");
        }
        private bool BeUniqueName(string calismaDurumAdi)
        {
            return !_manager.CalismaDurumuService.TIsAny(x => x.CalismaDurumAdi == calismaDurumAdi);
        }
    }
}
