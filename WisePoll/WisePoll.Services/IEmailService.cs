using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisePoll.Services
{
    public interface IEmailService
    {
        void SendMail(string toMail, string Subject, string Body);
    }
}
