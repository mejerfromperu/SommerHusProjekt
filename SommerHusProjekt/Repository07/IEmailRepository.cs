using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SommerHusProjekt.Repository07
{
    public interface IEmailRepository
    {
        void SendEmail(string to, string subject, string body);
    }
}
