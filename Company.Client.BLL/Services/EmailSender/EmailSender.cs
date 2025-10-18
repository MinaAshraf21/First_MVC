using Company.Client.DAL.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Company.Client.BLL.Services.EmailSender
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(Email email)
        {
            // Protocol to send email -> SMTP
            // SMTP => Simple Mail Transfer Protocol
            /*
             SMTP is an application layer protocol used for sending and receiving email
                  messages over the internet.
             */

            // when you want to send email from your application you need to use SMTP server
            // SMTP server is provided by email service providers like Google , Yahoo , Outlook , etc...
            // you need to know the SMTP server address and port number to send email using SMTP
            //here are some examples of SMTP server details for popular email service providers:
            // noon@gmail.com | SMTP Server : smtp.gmail.com | Port : 465 for SSL , 587 for TLS ,  | Encryption : TLS/SSL
            // noon@yahoo.com | SMTP Server : smtp.mail.yahoo.com | Port : 465 for SSL , 587 for TLS | Encryption : TLS/SSL
            // noon@outlook.com | SMTP Server : smtp-mail.outlook.com OR smtp.office365.com | Port : 587 | Encryption : STARTTLS

            //SSL => Secure Sockets Layer
            //TLS => Transport Layer Security
            //SSL and TlS are cryptographic protocols that provide secure communication over a computer network
            // they are used to encrypt data transmitted between clients and servers to ensure privacy and Security

            // TLS is the modern successor to SSL and is considered more secure than SSL , SSL is now deprecated
            // both use certificates to enable encrypted and authenticated connections for web browsing [HTTPS]
            // STARTTLS is a command used to upgrade an existing insecure connection to a secure connection using TLS

            //when creating SmtpClient object you need to provide the SMTP server host and port number
            var client = new SmtpClient("smtp.gmail.com",587);
            //we need to enable ssl or tls encryption before sending email
            client.EnableSsl = true; // to enable SSL or TLS encryption
            // we need to provide credentials to authenticate with the SMTP server
            // when creating NetworkCredential object you need to provide email and password
            // the password can be the actual email password or an app password if 2FA is enabled
            // app password is a special password generated for third-party apps to access your email account securely
            client.Credentials = new NetworkCredential("minaadly275@gmail.com", "empzyshavunmfaou");

            client.Send("minaadly275@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
