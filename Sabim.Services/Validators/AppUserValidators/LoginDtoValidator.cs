using FluentValidation;
using Sabim.Domain.DTOs.AppUserDtos;

namespace Sabim.Services.Validators.AppUserValidators
{
    public class LoginDtoValidator : AbstractValidator<LoginAppUserDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()/*.WithMessage("Kullanıcı adı gereklidir.")*/
                .MinimumLength(5).WithMessage("Kullanıcı adı en az 5 karakter olmalıdır.")
                .MaximumLength(15).WithMessage("Kullanıcı adı en fazla 15 karakter olabilir.")
                .Matches("^[abcçdefgğhıijklmnoöprsştuüvyzABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ0123456789._-]+$")
                .WithMessage("Kullanıcı adı yalnızca harf, rakam, '-', '.', '_' içerebilir.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre gereklidir.")
                .MinimumLength(5).WithMessage("Şifre en az 5 karakter uzunluğunda olmalıdır.")
                .MaximumLength(10).WithMessage("Şifre en fazla 10 karakter olabilir.");
        }
    }
}
