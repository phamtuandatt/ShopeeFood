using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace ShopeeFood_Web.Services
{
    public class SendMailService
    {
        MailSettings _mailSettings { get; set; }
        public SendMailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task<string> SendMail(MailContent mailContent)
        {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));

            email.To.Add(new MailboxAddress(mailContent.To, mailContent.To));
            email.Subject = mailContent.Title;

            var builder = new BodyBuilder();

            builder.HtmlBody = mailContent.Content;

            email.Body = builder.ToMessageBody();

            using var smtpClient = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                await smtpClient.ConnectAsync(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
                await smtpClient.SendAsync(email);
            }
            catch (Exception ex)
            {
                return $"LOI [{ex.Message}]"; 
            }

            smtpClient.Disconnect(true);

            return "GUI THANH CONG";

        }
    }
}
