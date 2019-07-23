using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace FinaroEngine.SMS
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            // Find your Account Sid and Token at twilio.com/console
            // DANGER! This is insecure. See http://twil.io/secure
            string accountSid = configuration["AcctSID"];
            string authToken = configuration["AuthToken"];

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "\n\rWelcome to trndx!\n\r#NYCHotDogs trading @ .70 TDX\n\rText 1 to buy 10 shares.",
                from: new Twilio.Types.PhoneNumber("+19177463774"),
                to: new Twilio.Types.PhoneNumber("+15515026572")
            );

            Console.WriteLine(message.Sid);
        }
    }
}

