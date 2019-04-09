﻿using Microsoft.Extensions.Configuration;
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
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            string myKey1 = configuration["myKey1"];
            Console.WriteLine(myKey1);

            string foo = configuration.GetSection("foo").Value;
            Console.WriteLine(foo);


            // Find your Account Sid and Token at twilio.com/console
            // DANGER! This is insecure. See http://twil.io/secure
            const string accountSid = "";
            const string authToken = "";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Welcome to the S'Way Exchange!\n\rNEW YORK GIANTS trading @ .70 SWAY Tokens\n\rText 1 to go long 10 Units.",
                from: new Twilio.Types.PhoneNumber("+19177463774"),
                to: new Twilio.Types.PhoneNumber("+1646")
            );

            Console.WriteLine(message.Sid);
        }
    }
}

