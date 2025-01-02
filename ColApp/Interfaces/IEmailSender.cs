using System.Net.Mail;
using System.Net;

namespace ColApp.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body);
    }

    public class EmailSender : IEmailSender
    {

        // Recherche : SmtpClient
        private readonly SmtpClient _smtpClient;
        private readonly string _fromEmail;

        public EmailSender(string smtpServer, int smtpPort, string fromEmail, string smtpPassword)
        {
            _smtpClient = new SmtpClient(smtpServer)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(fromEmail, smtpPassword),
                EnableSsl = true,
            };
            _fromEmail = fromEmail;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var mailMessage = new MailMessage(_fromEmail, to, subject, body);
                await _smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Log l'erreur ou affiche un message pour comprendre pourquoi l'email échoue
                Console.WriteLine($"Erreur lors de l'envoi de l'email: {ex.Message}");
            }
        }

    }
}
