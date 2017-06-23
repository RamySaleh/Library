using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        public bool SendEmail(Email email)
        {
            //var body =  "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            //string.Format(body, model.FromName, model.FromEmail, model.Message);
            var message = new MailMessage();
            message.To.Add(new MailAddress(email.Recepient));  // replace with valid value 
            message.From = new MailAddress("library@outlook.com");  // replace with valid value
            message.Subject = "You borrowed a book from our library";
            message.Body = email.Message;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "user@outlook.com",  // replace with valid value
                    Password = "password"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp-mail.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);                
            }

            return true;
        }
    }
}
