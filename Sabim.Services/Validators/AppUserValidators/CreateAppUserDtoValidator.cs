using FluentValidation;
using Sabim.Domain.DTOs.AppUserDtos;
using Sabim.Services.Contracts;

namespace Sabim.Services.Validators.AppUserValidators
{
    public class CreateAppUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        private readonly IServiceManager _manager;

        public CreateAppUserDtoValidator(IServiceManager manager)
        {
            _manager = manager;
            RuleFor(x => x.UserName)
                 .NotEmpty().WithMessage("Kullanıcı Adı boş bırakılamaz.")
                 .MinimumLength(5).WithMessage("Kullanıcı Adı en az 5 karakter olmalıdır.")
                 .MaximumLength(20).WithMessage("Kullanıcı Adı en fazla 20 karakter olmalıdır.")
                 .Matches("^[a-zA-Z0-9çÇğĞıİöÖşŞüÜ]+$").WithMessage("Kullanıcı adı yalnızca harf ve rakam içerebilir.");
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Email alanı boş bırakılamaz.")
                 .MinimumLength(5).WithMessage("Email en az 5 karakter olmalıdır.")
                 .MaximumLength(35).WithMessage("Email en fazla 35 karakter olmalıdır.")
                 .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre alanı boş bırakılamaz.")
                .MinimumLength(5).WithMessage("Şifre en az 5 karakter olmalıdır.")
                .MaximumLength(15).WithMessage("Şifre en fazla 15 karakter olmalıdır.")
                .Matches("^[a-zA-Z0-9çÇğĞıİöÖşŞüÜ.,\\-\\+]+$").WithMessage("Parola yalnızca harf, rakam, ve . , - + karakterlerini içerebilir.");
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Şifre(Tekrar) alanı boş bırakılamaz.")
                .MinimumLength(5).WithMessage("Şifre(Tekrar) en az 5 karakter olmalıdır.")
                .MaximumLength(15).WithMessage("Şifre(Tekrar) en fazla 15 karakter olmalıdır.")
                .Matches("^[a-zA-Z0-9çÇğĞıİöÖşŞüÜ.,\\-\\+]+$").WithMessage("Parola yalnızca harf, rakam, ve . , - + karakterlerini içerebilir.")
                .Equal(x => x.Password).WithMessage("Şifre ve Şifre(Tekrar) alanları birbiriyle uyuşmuyor.");
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Rol alanı boş bırakılamaz.");

        }
    }
}
