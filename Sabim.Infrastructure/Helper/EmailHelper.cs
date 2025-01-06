using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Helper
{
    public class EmailHelper
    {
        private readonly MailSettings _mailSettings;

        public EmailHelper(MailSettings mailSettings)
        {
            _mailSettings = mailSettings;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                // MimeMessage oluşturuluyor
                var mimeMessage = new MimeMessage();

                // Gönderen mail adresi
                mimeMessage.From.Add(new MailboxAddress("SABIM", _mailSettings.SenderEmail));

                // Alıcı mail adresi
                mimeMessage.To.Add(new MailboxAddress("Receiver", toEmail));

                // Mail başlığı
                mimeMessage.Subject = subject;

                // HTML gövde oluşturuluyor
                var bodyBuilder = new BodyBuilder { HtmlBody = body };
                mimeMessage.Body = bodyBuilder.ToMessageBody();

                // MailKit SMTP client yapılandırması
                using (var client = new SmtpClient())
                {
                    // Sertifika iptal kontrolünü kapatma
                    client.CheckCertificateRevocation = false;

                    // Bağlantı yapma, SSL kullanma

                    client.Connect(_mailSettings.SmtpServer, _mailSettings.Port, SecureSocketOptions.Auto);

                    // Kimlik doğrulama
                    client.Authenticate(_mailSettings.SenderEmail, _mailSettings.Password);

                    // Mail gönderme
                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                // Hata loglama işlemi yapılabilir
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
