using System.Net;
using System.Net.Mail;
using TourismFormsAPI.Interfaces.Services;

namespace TourismFormsAPI.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly string _emailSender;
        private readonly string _password;

        public EmailSenderService()
        {
            _emailSender = "ministerstvoTurizma@mail.ru";
            _password = "sdfghnjkl43Q.2Y";
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_emailSender, _password)
            };
            return client.SendMailAsync(
                new MailMessage(from: _emailSender,
                                to: email,
                                subject,
                                message
                ));
        }
    }
}
