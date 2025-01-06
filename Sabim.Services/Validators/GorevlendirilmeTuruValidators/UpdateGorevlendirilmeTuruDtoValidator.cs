using FluentValidation;
using Sabim.Domain.DTOs.GorevlendirilmeTuruDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.GorevlendirilmeTuruValidators
{
    public class UpdateGorevlendirilmeTuruDtoValidator : AbstractValidator<UpdateGorevlendirilmeTuruDto>
    {
        private readonly IServiceManager _manager;
        public UpdateGorevlendirilmeTuruDtoValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.GorevlendirilmeTuruID).NotEmpty().WithMessage("Görevlendirilme Tür ID alanı boş olamaz.");
            RuleFor(x => x.GorevlendirilmeTuruAdi)
              .NotEmpty().WithMessage("Görevlendirilme Tur Adı boş bırakılamaz.")
              .MaximumLength(50).WithMessage("Görevlendirilme Tür Adı en fazla 50 karakter olmalıdır.")
              .MinimumLength(5).WithMessage("Görevlendirilme Tür Adı Adı en az 5 karakter olmalıdır.")
              .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ\\s]+$").WithMessage("Görevlendirilme Tür Adı yalnızca harf içermelidir.")
              .Must((dto, gorevlendirilmeTurAdi) => BeUniqueName(gorevlendirilmeTurAdi, dto.GorevlendirilmeTuruID));
            RuleFor(x => x.KurumPersonelListesineDahilMi)
              .NotNull().WithMessage("Personel Listesinde Gösterilme Durumu alanı boş olamaz.")
              .Must(value => value == true || value == false).WithMessage("Personel Listesinde Gösterilme Durumu alanı aktif veya pasif olmalıdır.");
        }
        private bool BeUniqueName(string gorevlendirilmeTurAdi, short? excludeId)
        {
            return !_manager.GorevlendirilmeTuruService.TIsAny(x => x.GorevlendirilmeTuruAdi == gorevlendirilmeTurAdi, excludeId);
        }
    }
}
