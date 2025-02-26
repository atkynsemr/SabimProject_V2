using FluentValidation;
using Sabim.Domain.DTOs.PersonelAyrilisNedenleriDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.PersonelAyrilisNedenleriValidators
{
    public class CreatePersonelAyrilisNedenleriValidator : AbstractValidator<CreatePersonelAyrilisNedenleriDto>
    {
        private readonly IServiceManager _manager;
        public CreatePersonelAyrilisNedenleriValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.Aciklama)
              .NotEmpty().WithMessage("Ayrılış Nedeni boş bırakılamaz.")
              .MaximumLength(50).WithMessage("Ayrılış Nedeni en fazla 50 karakter olmalıdır.")
              .MinimumLength(5).WithMessage("Ayrılış Nedeni en az 4 karakter olmalıdır.")
              .Must(BeUniqueName).WithMessage("Aynı ayrılış nedeni zaten kayıtlı!");
            RuleFor(x => x.KaliciAyrilisMi)
              .NotNull().WithMessage("Kalıcı/Geçici Ayrılış Durumu alanı boş olamaz.")
              .Must(value => value == true || value == false).WithMessage("Kalıcı/Geçici Ayrılış Durumu alanı aktif veya pasif olmalıdır.");
            RuleFor(x => x.DonanimUyarisi)
              .NotNull().WithMessage("Donanım Uyarısı Verme Durumu alanı boş olamaz.")
              .Must(value => value == true || value == false).WithMessage("Donanım Uyarısı Verme Durumu alanı aktif veya pasif olmalıdır.");
            RuleFor(x => x.DurumId).NotEmpty().WithMessage("Durum alanı boş bırakılamaz.");
        }
        private bool BeUniqueName(string aciklama)
        {
            return !_manager.PersonelAyrilisNedenleriService.TIsAny(x => x.Aciklama == aciklama);
        }
    }
}
