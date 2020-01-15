using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkScheduler.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string subject, string body, List<string> recipients);
    }
}
