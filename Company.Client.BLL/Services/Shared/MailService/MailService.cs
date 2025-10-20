using Company.Client.DAL.Common.Entities;
using Company.Client.PL.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;

namespace Company.Client.BLL.Services.Shared.MailService
{
    //IOptions is used to retrieve configured MailSettings instatnces
    //MailSettings is configured to read its values from appsettings , so we tell the CLR this info to get this values to an object of MailSettings and inject it
    public class MailService(IOptions<MailSettings> _options) : IMailService
    {

        public void SendEmail(Email email)
        {
            //Build Message
            MimeMessage mail = new();

            mail.Subject = email.Subject;

            mail.To.Add(MailboxAddress.Parse(email.To));
            mail.From.Add(new MailboxAddress(_options.Value.DisplayName,_options.Value.Email));

            var builder = new BodyBuilder();
            builder.TextBody = email.Body;
            mail.Body = builder.ToMessageBody();
           
            //Establish Connection
            using var smtp = new SmtpClient();

            //open connection with mail service , last parameter is to trust the connection
            smtp.Connect(_options.Value.Host, _options.Value.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_options.Value.Email, _options.Value.Password); // the email & password of the sender
            
            //Send Message
            smtp.Send(mail);
        }
    }
}
