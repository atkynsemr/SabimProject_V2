using FluentValidation;
using Sabim.Domain.DTOs.UnvanDtos;

namespace Sabim.Services.Validators.UnvanValidators
{
    public class ResultUnvanDtoValidator : AbstractValidator<ResultUnvanDto>
    {
        public ResultUnvanDtoValidator()
        {
            RuleFor(x=>x.UnvanID).NotEmpty().WithMessage("Ünvan ID alanı boş olamaz.");
        }
    }
}
