using Mailjet.Client.TransactionalEmails.Response;

namespace STM.AIU.Application.Contracts.Infrastructure;
public interface IEmailService
{
    Task<TransactionalEmailResponse> MailJetEmailSenderAsync(MailJetEmailRequest mailJetEmailRequest);
    Task<string> SMTPEmailSenderAsync(SMTPEmailRequest smtpEmailRequest);
}