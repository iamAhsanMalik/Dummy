using STM.AIU.Application.Enums;

namespace STM.AIU.Application.Models;
public class SMTPEmailRequest
{
    public string EmailSubject { get; set; } = null!;
    public string EmailBody { get; set; } = null!;
    public string ToEmail { get; set; } = null!;
    public string FromEmail { get; set; } = string.Empty;
    public SMTPServer SMTPServerName { get; set; } = SMTPServer.Default;
    public ICollection<IFormFile>? Attachments { get; set; }
}
