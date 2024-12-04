using Microsoft.AspNetCore.Identity;

namespace Sabim.Infrastructure.Identity.CustomErrors
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DefaultError()
        {
            return new IdentityError
            {
                Code = "DefaultError",
                Description = "Bir hata oluştu. Lütfen tekrar deneyin."
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = "PasswordTooShort",
                Description = $"Parolanız en az {length} karakter uzunluğunda olmalıdır."
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = "PasswordRequiresLower",
                Description = "Parolanız en az bir küçük harf içermelidir."
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = "PasswordRequiresUpper",
                Description = "Parolanız en az bir büyük harf içermelidir."
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = "PasswordRequiresDigit",
                Description = "Parolanız en az bir rakam içermelidir."
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = "PasswordRequiresNonAlphanumeric",
                Description = "Parolanız en az bir özel karakter içermelidir."
            };
        }

        public override IdentityError InvalidEmail(string? email)
        {
            return new IdentityError
            {
                Code = "InvalidEmail",
                Description = $"{email} geçersiz bir e-posta adresidir."
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = "DuplicateEmail",
                Description = $"{email} zaten başka bir kullanıcı tarafından kullanılmaktadır."
            };
        }
    }

}
