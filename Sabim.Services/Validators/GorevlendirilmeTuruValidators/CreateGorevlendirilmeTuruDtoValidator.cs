using FluentValidation;
using Sabim.Domain.DTOs.GorevlendirilmeTuruDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.GorevlendirilmeTuruValidators
{
    public class CreateGorevlendirilmeTuruDtoValidator : AbstractValidator<CreateGorevlendirilmeTuruDto>
    {
        private readonly IServiceManager _manager;
        public CreateGorevlendirilmeTuruDtoValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.GorevlendirilmeTuruAdi)
              .NotEmpty().WithMessage("Görevlendirilme Tür Adı boş bırakılamaz.")
              .MaximumLength(50).WithMessage("Görevlendirilme Tür Adı en fazla 50 karakter olmalıdır.")
              .MinimumLength(5).WithMessage("Görevlendirilme Tür Adı Adı en az 5 karakter olmalıdır.")
              .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ\\s]+$").WithMessage("Görevlendirilme Tür Adı yalnızca harf içermelidir.")
              .Must(BeUniqueName).WithMessage("Aynı görevlendirilme tür adı zaten kayıtlı!");
            RuleFor(x => x.KurumPersonelListesineDahilMi)
              .NotNull().WithMessage("Personel Listesinde Gösterilme Durumu alanı boş olamaz.")
              .Must(value => value == true || value == false).WithMessage("Personel Listesinde Gösterilme Durumu alanı aktif veya pasif olmalıdır.");
        }
        private bool BeUniqueName(string gorevlendirilmeTurAdi)
        {
            return !_manager.GorevlendirilmeTuruService.TIsAny(x => x.GorevlendirilmeTuruAdi == gorevlendirilmeTurAdi);
        }
    }
}
