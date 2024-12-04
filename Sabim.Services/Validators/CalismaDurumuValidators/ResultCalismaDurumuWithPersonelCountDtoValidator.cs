using FluentValidation;
using Sabim.Domain.DTOs.CalismaDurumuDtos;

namespace Sabim.Services.Validators.CalismaDurumuValidators
{
    public class ResultCalismaDurumuWithPersonelCountDtoValidator : AbstractValidator<ResultCalismaDurumuWithPersonelCountDto>
    {
        public ResultCalismaDurumuWithPersonelCountDtoValidator()
        {
            RuleFor(x => x.CalismaDurumuID).NotEmpty().WithMessage("Çalışma Durumu ID alanı boş olamaz.");
            RuleFor(x => x.PersonelSayisi).NotEmpty().WithMessage("Personel Sayı alanı boş olamaz.");
        }
    }
}
