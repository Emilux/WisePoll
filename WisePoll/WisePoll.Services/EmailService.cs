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

namespace WisePoll.Services
{
    public class EmailService : IEmailService
    {

        public void SendMail()
        {
            string to = "stevevonnegri@gmail.com"; //To address
            string from = "WisepollProjet@outlook.com"; //From address

            MailMessage message = new MailMessage(from, to);

            message.Subject = "Sending Email Using Asp.Net & C#";
            message.Body = "In this article you will learn how to send a email using Asp.Net & C#";
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.live.com", 587);
            System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential("WisepollProjet@outlook.com", "WisePoll123");

            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;

            client.Send(message);
        }

        public void SendMail1()
        {
            string to = "stevevonnegri@gmail.com"; //To address    
            string from = "anaanacci@outlook.fr"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = "In this article you will learn how to send a email using Asp.Net & C#";
            message.Subject = "Sending Email Using Asp.Net & C#";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;


            SmtpClient client = new SmtpClient("smtp.office365.com", 587);

            System.Net.NetworkCredential basicCredential1 = new
                            System.Net.NetworkCredential("wisepollprojet@outlook.com", "WisePoll123");

            client.EnableSsl = true;
            client.UseDefaultCredentials = false;

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
