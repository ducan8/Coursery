using Application.Handle.HandleEmail;
using Application.IServices;
using Application.Payloads.ResponseModels.DataEmail;
using MailKit.Net.Smtp;
using MimeKit;


namespace Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfiguration;
        public EmailService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }



        public string SendEmail(EmailMessage emailMessage)
        {
            var message = CreateEmailMessage(emailMessage);
            Send(message);
            var recipients = string.Join(", ", message.To);

            return ResponseMessage.GetEmailSuccessMessage(recipients);
        }


        #region private method

        private MimeMessage CreateEmailMessage(EmailMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Coursery", _emailConfiguration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return emailMessage;
        }

        private async void Send(MimeMessage message)
        {
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_emailConfiguration.From, _emailConfiguration.Password);
                await client.SendAsync(message);

            }
            catch (Exception ex)
            {
                throw;
            }
            finally { await client.DisconnectAsync(true); client.Dispose(); }
        }

        #endregion
    }
}