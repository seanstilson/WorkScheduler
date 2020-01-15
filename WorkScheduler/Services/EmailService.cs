using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace WorkScheduler.Services
{
    public class EmailService : IEmailService
    {
        public EmailService()
        {
        }

        public async Task<bool> SendEmail(string subject, string body, List<string> recipients)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
                await Email.ComposeAsync(message);
                return true;
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                Debug.WriteLine($"Email client not supported on this device {fbsEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
