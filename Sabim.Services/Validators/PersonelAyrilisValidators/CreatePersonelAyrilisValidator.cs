using FluentValidation;
using Sabim.Domain.DTOs.PersonelAyrilisDtos;

namespace Sabim.Services.Validators.PersonelAyrilisValidators
{
    public class CreatePersonelAyrilisValidator : AbstractValidator<CreatePersonelAyrilisDto>
    {
        public CreatePersonelAyrilisValidator()
        {
            RuleFor(x => x.PersonelId).NotEmpty().WithMessage("Personel alanı boş bırakılamaz.");
            RuleFor(x => x.PersonelAyrilisNedenleriId).NotEmpty().WithMessage("Ayrılış nedeni alanı boş bırakılamaz.");
            RuleFor(x => x.BaslangicTarihi).NotEmpty().When(x => !x.BitisTarihi.HasValue)
                .WithMessage("Başlangıç veya Bitiş Tarihlerinden en az biri dolu olmalıdır.");
            RuleFor(x => x.BaslangicTarihi)
                .Must((model, baslangicTarihi) =>!baslangicTarihi.HasValue || !model.BitisTarihi.HasValue || baslangicTarihi < model.BitisTarihi)
                .WithMessage("İzin Başlangıç Tarihi, İzin Bitiş Tarihinden önceki bir tarih olmalıdır.");
            RuleFor(x => x.BitisTarihi).NotEmpty().When(x => !x.BaslangicTarihi.HasValue)
                .WithMessage("Başlangıç veya Bitiş Tarihlerinden en az biri dolu olmalıdır.");
        }
    }
}
