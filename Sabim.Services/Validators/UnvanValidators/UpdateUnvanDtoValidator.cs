using FluentValidation;
using Sabim.Domain.DTOs.UnvanDtos;

namespace Sabim.Services.Validators.UnvanValidators
{
    public class UpdateUnvanDtoValidator : AbstractValidator<UpdateUnvanDto>
    {
        public UpdateUnvanDtoValidator()
        {
            RuleFor(x => x.UnvanID).NotEmpty().WithMessage("Ünvan ID alanı boş olamaz.");
        }
    }
}
