using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using SimpleEmailApp_Attachments.Models;
using MailKit.Net.Smtp;

namespace SimpleEmailApp_Attachments.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
            
        }

        public void SendEmail(EmailDto request)
        {
           
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            //email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            //***
            var bodybuilder = new BodyBuilder {HtmlBody = string.Format("<h1 style= 'color : red'> {0} </h1>", request.Body) };

            if(request.Attachments != null && request.Attachments.Any())
            {
                byte[] fileBytes;

                foreach(var attachment in request.Attachments)
                {
                    using(var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();

                    }

                    bodybuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }

            }


            email.Body = bodybuilder.ToMessageBody();


            //***


            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value , _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
