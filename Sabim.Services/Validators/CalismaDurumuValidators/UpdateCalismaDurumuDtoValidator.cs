using FluentValidation;
using Sabim.Domain.DTOs.CalismaDurumuDtos;

namespace Sabim.Services.Validators.CalismaDurumuValidators
{
    public class UpdateCalismaDurumuDtoValidator : AbstractValidator<UpdateCalismaDurumuDto>
    {
        public UpdateCalismaDurumuDtoValidator()
        {
            RuleFor(x => x.CalismaDurumuID).NotEmpty().WithMessage("Çalışma Durumu ID alanı boş olamaz.");
        }
    }
}
