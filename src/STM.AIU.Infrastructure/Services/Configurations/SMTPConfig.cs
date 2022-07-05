namespace STM.AIU.Infrastructure.Services.Configurations;
internal class SMTPConfig
{
    public DefaultSMTPConfig DefaultSMTP { get; set; } = default!;
    public StudentSMTPConfig StudentSMTP { get; set; } = default!;
    public ProspectSMTPConfig ProspectSMTP { get; set; } = default!;
}

internal class DefaultSMTPConfig
{
    public string? SmtpHost { get; set; }
    public int SmtpPort { get; set; }
    public string? SenderEmail { get; set; }
    public string? Account { get; set; }
    public string? Password { get; set; }
    public bool SecureSocketOptions { get; set; }
}
internal class StudentSMTPConfig
{
    public string? SmtpHost { get; set; }
    public int SmtpPort { get; set; }
    public string? SenderEmail { get; set; }
    public string? Account { get; set; }
    public string? Password { get; set; }
    public bool SecureSocketOptions { get; set; }
}
internal class ProspectSMTPConfig
{
    public string? SmtpHost { get; set; }
    public int SmtpPort { get; set; }
    public string? SenderEmail { get; set; }
    public string? Account { get; set; }
    public string? Password { get; set; }
    public bool SecureSocketOptions { get; set; }
}

