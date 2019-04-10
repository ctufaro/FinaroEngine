using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace FinaroEngine.Library
{
    public class Feedback
    {
        private Options opts;
        private int userId;

        public Feedback(Options opts, int userId)
        {
            this.opts = opts;
            this.userId = userId;
        }

        public async Task<string> Save(string apiKey, string recipients, string message)
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
                    From = new EmailAddress("feedback@swayx.me", "S'way Admin"),
                    Subject = "S'way Exchange User Feedback",
                    PlainTextContent = "Feedback: " + message,
                    HtmlContent = "<strong>Feedback: </strong><i>" + message+"</i>"
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
