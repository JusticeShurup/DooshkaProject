using Dooshka.Application.Infrastructure.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Dooshka.Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public EmailSender(IHttpContextAccessor contextAccessor) 
        {
            _contextAccessor = contextAccessor;
        }

        public async Task SendEmailAsync(string recepient, string message)
        {
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("dooshkaservice@mail.ru"),

            };

            mailMessage.To.Add(recepient);
            mailMessage.Subject = "Подтверждение почты";
            mailMessage.Body = message;

            SmtpClient client = new SmtpClient
            {
                Host = "smtp.mail.ru",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("dooshkaservice@mail.ru", "ZxuEqzmywfF5NgzJwVhE")
            };
            client.Send(mailMessage);
            client.Dispose();
        
        }

        public async Task SendConfirmationCode(string recepient, int code)
        {
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("dooshkaservice@mail.ru"),
            };

            mailMessage.To.Add(recepient);
            mailMessage.Subject = "Подтверждение почты";
            var url = $"http://{_contextAccessor.HttpContext.Request.Host.Value}/api/Auth/ConfirmEmail?email={recepient}&confirmationCode={code}";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = $"<a href='{url}'>Нажмите тут для подтверждения почты</a>";

            SmtpClient client = new SmtpClient
            {
                Host = "smtp.mail.ru",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("dooshkaservice@mail.ru", "ZxuEqzmywfF5NgzJwVhE")
            };

            client.Send(mailMessage);
            client.Dispose();
        }
    }
}
