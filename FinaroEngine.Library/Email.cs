using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace FinaroEngine.Library
{
    public class Email
    {
        public static async Task<string> SendAsync(string apiKey, string recipients, string email, string subject, string message)
        {
            try
            {
                List<EmailAddress> emails = new List<EmailAddress>();
                foreach (string s in recipients.Split(new char[] { ';' }))
                {
                    emails.Add(new EmailAddress(s));
                }
                var client = new SendGridClient(apiKey);
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("contact@trndx.co", "trndx mailer"),
                    Subject = $"Contact Form - {subject.Trim()}",
                    PlainTextContent = $"{message.Trim()} - sent from {email}",
                    HtmlContent = $"{message.Trim()} - sent from {email}"
                };
                msg.AddTos(emails);
                var response = await client.SendEmailAsync(msg);
                return response.StatusCode.ToString() + ":" + response.Headers.ToString();
            }
            catch (Exception e)
            {
                return e.ToString();
            }


        }
    }
}
