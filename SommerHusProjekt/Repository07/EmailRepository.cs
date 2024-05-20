using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SommerHusProjekt.Repository07
{
    public class EmailRepository : IEmailRepository
    {
        private readonly SmtpClient _smtpClient;

        public EmailRepository()
        {
            // Initialize SmtpClient with your SMTP server settings
            _smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("SommerNSol@gmail.com", "SommerSol2000"),
                EnableSsl = true
            };
        }

        public void SendEmail(string to, string subject, string body)
        {
            var message = new MailMessage("SommerNSol@gmail.com", to, subject, body)
            {
                IsBodyHtml = true
            };

            _smtpClient.Send(message);
        }
    }
}
