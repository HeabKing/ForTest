using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2016_09_14_AspNetCore_Default_Identity.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
