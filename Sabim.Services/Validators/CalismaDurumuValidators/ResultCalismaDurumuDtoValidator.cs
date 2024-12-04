using FluentValidation;
using Sabim.Domain.DTOs.CalismaDurumuDtos;

namespace Sabim.Services.Validators.CalismaDurumuValidators
{
    public class ResultCalismaDurumuDtoValidator : AbstractValidator<ResultCalismaDurumuDto>
    {
        public ResultCalismaDurumuDtoValidator()
        {
            RuleFor(x => x.CalismaDurumuID).NotEmpty().WithMessage("Çalışma Durumu ID alanı boş olamaz.");
        }
    }
}
