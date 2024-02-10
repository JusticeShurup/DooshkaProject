using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dooshka.Application.Infrastructure.Contracts
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string email, string message);
        public Task SendConfirmationCode(string recepient, int code);
    }
}
