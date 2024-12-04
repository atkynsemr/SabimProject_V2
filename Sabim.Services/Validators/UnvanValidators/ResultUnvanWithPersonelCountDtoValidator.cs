using FluentValidation;
using Sabim.Domain.DTOs.UnvanDtos;

namespace Sabim.Services.Validators.UnvanValidators
{
    public class ResultUnvanWithPersonelCountDtoValidator : AbstractValidator<ResultUnvanWithPersonelCountDto>
    {
        public ResultUnvanWithPersonelCountDtoValidator()
        {
            RuleFor(x => x.UnvanID).NotEmpty().WithMessage("Ünvan ID alanı boş olamaz.");
            RuleFor(x => x.PersonelSayisi).NotEmpty().WithMessage("Personel Sayı alanı boş olamaz.");
        }
    }
}
