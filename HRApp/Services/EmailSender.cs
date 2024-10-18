using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HRApp.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.example.com")
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("username", "password"),
                EnableSsl = true
            };

            return client.SendMailAsync(
                new MailMessage("no-reply@example.com", email, subject, message)
                {
                    IsBodyHtml = true
                });
        }
    }
}
