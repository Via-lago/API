using System.Net.Mail;
using System.Net;

namespace API.Utility
{
    public class MailService
    {
        public static void Send(string subject, string body, string email)
        {
            // Set the Papercut SMTP server settings
            string smtpServer = "localhost";
            int smtpPort = 25;

            MailMessage message = new MailMessage
            {
                From = new MailAddress("sender@example.com"),
                Subject = subject,
                Body = body
            };
            message.To.Add(email);

            // Create a SmtpClient and configure it to use Papercut
            SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = false,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("", "") // No authentication required for Papercut
            };

            // Send the email
            smtpClient.Send(message);

            // Clean up resources
            message.Dispose();
            smtpClient.Dispose();
        }
    }
}