namespace Sabim.Services.Contracts
{
    public interface IEmailService
    {
        Task SendPasswordResetEmailAsync(string toEmail, string resetCode);
    }
}
