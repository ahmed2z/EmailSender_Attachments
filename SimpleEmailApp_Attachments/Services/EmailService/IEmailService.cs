using SimpleEmailApp_Attachments.Models;

namespace SimpleEmailApp_Attachments.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}
