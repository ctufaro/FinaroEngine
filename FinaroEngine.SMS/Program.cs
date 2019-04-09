using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace FinaroEngine.SMS
{
    class Program
    {
        static void Main(string[] args)
        {
            // Find your Account Sid and Token at twilio.com/console
            // DANGER! This is insecure. See http://twil.io/secure
            const string accountSid = "AC1d82bb84dad1f5871452030ac6e22250";
            const string authToken = "acb5782d7799db9658955bfd11c2b715";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Welcome to the S'Way Exchange!\n\rNEW YORK GIANTS trading @ .70 SWAY Tokens\n\rText 1 to go long 10 Units.",
                from: new Twilio.Types.PhoneNumber("+19177463774"),
                to: new Twilio.Types.PhoneNumber("+16462461515")
            );

            Console.WriteLine(message.Sid);
        }
    }
}

