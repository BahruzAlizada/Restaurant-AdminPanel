using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace Restaurant.Helpers
{
    public static class SendEmail
    {
        public static async Task SendMailAsync(string messageSubject, string messageBody, string mailTo)
        {
            SmtpClient client = new SmtpClient("smtp.yandex.com", 587);
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("bahruz.a@itbrains.edu.az", "yeymbfptscnarvqt");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailMessage message = new MailMessage("bahruz.a@itbrains.edu.az", mailTo);
            message.Subject = messageSubject;
            message.Body = messageBody;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;

            await client.SendMailAsync(message);
        }
    }
}
