using FluentValidation;
using Sabim.Domain.DTOs.AppUserDtos;

namespace Sabim.Services.Validators.AppUserValidators
{
    public class CreateDurumDtoValidator : AbstractValidator<ForgotPasswordDto>
    {
        public CreateDurumDtoValidator()
        {
            RuleFor(x => x.Eposta).NotEmpty().
                WithMessage("Email adresinizi giriniz.")
                .WithName("E-Posta Adresiniz").EmailAddress().WithMessage("Lütfen uygun formatta e-posta giriniz.");
        }
    }
}
