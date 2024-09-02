namespace SimpleEmailApp_Attachments.Models
{
    public class EmailDto
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

        //public IFormFileCollection ? Attachments { get; set; }

        public List<IFormFile>? Attachments { get; set; } = new List<IFormFile>();

    }
}
