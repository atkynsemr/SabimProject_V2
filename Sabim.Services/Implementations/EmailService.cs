using Sabim.Infrastructure.Helper;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailHelper _emailHelper;
        public EmailService(EmailHelper emailHelper)
        {
            _emailHelper = emailHelper;
        }
        public async Task SendPasswordResetEmailAsync(string toEmail, string resetCode)
        {
            var subject = "Şifre Sıfırlama";
            var body = $"SabimWEB şifre sıfırlama kodunuz: <b>{resetCode}</b>.";
            await _emailHelper.SendEmailAsync(toEmail, subject, body);
        }
    }
}
