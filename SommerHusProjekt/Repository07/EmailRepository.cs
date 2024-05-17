using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SommerHusProjekt.Repository07
{
    public class EmailRepository : IEmailRepository
    {
        public void SendEmail(string to, string subject, string body)
        {
            var message = new MailMessage("no-reply@Isakgm.com", to, subject, body);
            using (var smtpClient = new SmtpClient("smtp.yourdomain.com"))
            {
                smtpClient.Send(message);
            }
        }
    }
}
