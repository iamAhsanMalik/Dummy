namespace STM.AIU.Infrastructure.Services.Communication.Email;

internal class EmailService : IEmailService
{
    private readonly MailJetConfig _mailJetConfig;
    private readonly SMTPConfig _smptConfig;

    public EmailService(IOptions<SMTPConfig> smptConfig, IOptions<MailJetConfig> mailJetConfig)
    {
        _mailJetConfig = mailJetConfig.Value;
        _smptConfig = smptConfig.Value;
    }
    #region SMTP
    public async Task<string> SMTPEmailSenderAsync(SMTPEmailRequest smtpEmailRequest)
    {
        try
        {
            MimeMessage email = new MimeMessage();
            string? smtpHost, senderEmail, smtpAccount, smtpPassword;
            int smtpPort;
            bool secureSocketOptions;
            
            GetSMTPConfigurations(smtpEmailRequest.FromEmail, smtpEmailRequest.SMTPServerName, out smtpHost, out smtpPort, out senderEmail, out smtpAccount, out smtpPassword, out secureSocketOptions);

            email.From.Add(MailboxAddress.Parse(senderEmail));
            email.To.Add(MailboxAddress.Parse(smtpEmailRequest.ToEmail));
            email.Subject = smtpEmailRequest.EmailSubject;
            email.Body = new TextPart(TextFormat.Html) { Text = smtpEmailRequest.EmailBody };

            var builder = new BodyBuilder();
            byte[] fileBytes;

            if (smtpEmailRequest.Attachments != null)
            {
                var files = smtpEmailRequest.Attachments;
                if (files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        //builder.Attachments.Add(file.FileName, fileBytes, MimeKit.ContentType.Parse(MediaTypeNames.Application.Pdf));
                        builder.Attachments.Add(file.FileName);
                    }
                }
            }
            string emailResult = "";
            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.Connect(smtpHost, smtpPort, secureSocketOptions);

                // Uncommit this line if you want to add authenticaiton. It is always recommended
                //smtp.Authenticate(smtpPassword, smtpAccount);
                emailResult = await smtp.SendAsync(email);
                smtp.Disconnect(true);
                await smtp.DisconnectAsync(true);
            }

            return emailResult;
        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to send email {ex.Message}");
        }
    }


    #endregion

    #region MailJet
    public async Task<TransactionalEmailResponse> MailJetEmailSenderAsync(MailJetEmailRequest mailJetEmailRequest)
    {
        MailjetClient _client = new MailjetClient(_mailJetConfig.ApiKey, _mailJetConfig.SecretKey);
        //ICollection<Attachment> formFiles = new List<Attachment>();

        //foreach(var file in formFiles)
        //{
        //    ContentType
        //}

        var email = new TransactionalEmailBuilder()
               .WithFrom(new SendContact(!string.IsNullOrEmpty(mailJetEmailRequest.FromEmail) ? mailJetEmailRequest.FromEmail : _mailJetConfig.SenderEmail))
               .WithSubject(mailJetEmailRequest.EmailSubject)
               .WithHtmlPart(mailJetEmailRequest.EmailBody)
               .WithTo(new SendContact(mailJetEmailRequest.ToEmail))
               .Build();
        return await _client.SendTransactionalEmailAsync(email);
    }
    #endregion

    #region SMTP Configurations Getter
    private void GetSMTPConfigurations(string? fromEmail, SMTPServer smtpServerName, out string? smtpHost, out int smtpPort, out string? senderEmail, out string? smtpAccount, out string? smtpPassword, out bool secureSocketOptions)
    {
        smtpHost = smtpServerName == SMTPServer.Student ? _smptConfig.StudentSMTP.SmtpHost : smtpServerName == SMTPServer.Prospect ? _smptConfig.ProspectSMTP.SmtpHost : _smptConfig.DefaultSMTP.SmtpHost;
        smtpPort = smtpServerName == SMTPServer.Student ? _smptConfig.StudentSMTP.SmtpPort : smtpServerName == SMTPServer.Prospect ? _smptConfig.ProspectSMTP.SmtpPort : _smptConfig.DefaultSMTP.SmtpPort;
        senderEmail = !string.IsNullOrEmpty(fromEmail) ? fromEmail : smtpServerName == SMTPServer.Student ? _smptConfig.StudentSMTP.SenderEmail : smtpServerName == SMTPServer.Prospect ? _smptConfig.ProspectSMTP.SenderEmail : _smptConfig.DefaultSMTP.SenderEmail;
        smtpAccount = smtpServerName == SMTPServer.Student ? _smptConfig.StudentSMTP.Account : smtpServerName == SMTPServer.Prospect ? _smptConfig.ProspectSMTP.Account : _smptConfig.DefaultSMTP.Account;
        smtpPassword = smtpServerName == SMTPServer.Student ? _smptConfig.StudentSMTP.Password : smtpServerName == SMTPServer.Prospect ? _smptConfig.ProspectSMTP.Password : _smptConfig.DefaultSMTP.Password;
        secureSocketOptions = smtpServerName == SMTPServer.Student ? _smptConfig.StudentSMTP.SecureSocketOptions : smtpServerName == SMTPServer.Prospect ? _smptConfig.ProspectSMTP.SecureSocketOptions : _smptConfig.DefaultSMTP.SecureSocketOptions;
    }
    #endregion
}
