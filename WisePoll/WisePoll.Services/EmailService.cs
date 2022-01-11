using WisePoll.Services.ViewModels;
using WisePoll.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WisePoll.Data.Models;
using Identity.PasswordHasher;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace WisePoll.Services
{
    public class EmailService : IEmailService
    {
        public EmailService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void SendMail(string toMail, string Subject, string Body)
        {

            string to = toMail;

            string from = Configuration["ConnectionMail:mailfrom"];
            string mdpMail = Configuration["ConnectionMail:passwordmail"];

            MailMessage message = new();
            message.From = new MailAddress(from);
            message.CC.Add(from);
            message.Bcc.Add(to);

            message.Subject = Subject;
            message.Body = Body;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.live.com", 25);
                NetworkCredential basicCredential1 = new
                    (from, mdpMail);

            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;

            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
