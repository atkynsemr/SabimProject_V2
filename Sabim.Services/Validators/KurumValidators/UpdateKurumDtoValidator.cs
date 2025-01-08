using FluentValidation;
using Sabim.Domain.DTOs.KurumDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.KurumValidators
{
    public class UpdateKurumDtoValidator : AbstractValidator<UpdateKurumDto>
    {
        private readonly IServiceManager _manager;
        public UpdateKurumDtoValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.KurumID).NotEmpty().WithMessage("Kurum ID boş değer alamaz.");
            RuleFor(x => x.KurumAdi)
               .NotEmpty().WithMessage("Kurum Adı boş bırakılamaz.")
               .MaximumLength(100).WithMessage("Kurum Adı en fazla 100 karakter olmalıdır.")
               .MinimumLength(10).WithMessage("Kurum  Adı en az 10 karakter olmalıdır.")
               .Must((dto, kurumAdi) => BeUniqueName(kurumAdi, dto.KurumID, dto.SehirId))  
               .WithMessage("Aynı şehirde bu kurum adı zaten kayıtlı!");
            RuleFor(x => x.SehirId).NotEmpty().WithMessage("Şehir alanı boş bırakılamaz.");
            RuleFor(x => x.KurumTipiId).NotEmpty().WithMessage("Kurum Tipi alanı boş bırakılamaz.");
        }
        private bool BeUniqueName(string kurumAdi, short? excludeId, short sehirId)
        {
            return !_manager.KurumService.TIsKurumExists(kurumAdi, sehirId, excludeId);
        }
    }
}