using FluentValidation;
using Sabim.Domain.DTOs.AppUserDtos;

namespace Sabim.Services.Validators.AppUserValidators
{
    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(x => x.NewPassword)
           .NotEmpty().WithMessage("Şifre alanını doldururunuz.")
           .MinimumLength(5).WithMessage("Şifre en az 5 karakter olmalıdır.")
           .MaximumLength(10).WithMessage("Şifre en fazla 10 karakter olabilir.");

            RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Şifre tekrarı alanını doldururunuz.")
            .MinimumLength(5).WithMessage("Şifre en az 5 karakter olmalıdır.")
            .Equal(x => x.NewPassword).WithMessage("Şifreler eşleşmemektedir!");
        }
    }
}
