using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using SimpleEmailApp_Attachments.Models;

namespace SimpleEmailApp_Attachments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;

        }


        [HttpPost]
        public IActionResult SendEmail([FromForm] EmailDto request)
        {
            //var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();
            //request.Attachments = (List<IFormFile>?)files;
            
            _emailService.SendEmail(request);

            return Ok();
        }
    }
}
