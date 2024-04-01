using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace HBHB.Utility
{
    public class MailingUtility
    {

        public static void SendEmail(string Message)
        {

            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var config = builder.Build();
            var smtpClient = new SmtpClient(config["Smtp:Host"])
            {
                Port = int.Parse(config["Smtp:Port"]),
                Credentials = new NetworkCredential(config["Smtp:Username"], config["Smtp:Password"]),
                EnableSsl = true,
            };
            smtpClient.Send(config["Smtp:Username"], config["Smtp:Username"], "subject", "body");
        }

    }
}
