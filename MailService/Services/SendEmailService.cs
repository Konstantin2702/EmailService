using MimeKit;
using MailKit.Net.Smtp;
using MailService.JSONModels;

namespace MailService.Services
{
    public class SendEmailService:ISendEMailService
    {
        private readonly ILogMailService _logMailService;
        private readonly ServerConfiguration _serverConfiguration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">Configuration data</param>
        /// <param name="logMailService">Service that save information in DB about sending email's messages</param>
        public SendEmailService(IConfiguration configuration, ILogMailService logMailService, ServerConfiguration serverConfiguration)
        {
            _logMailService = logMailService;
            _serverConfiguration =serverConfiguration;
        }

        ///<inheritdoc cref="ISendEMailService.CreatingEmailMessages(MailJSONRequest)/>
        public List<MimeMessage> CreatingEmailMessages(MailJSONRequest mailGetInfo)
        { 
            List<MimeMessage> messages = new();
            if (mailGetInfo.Recipients != null)
            {
                foreach (string address in mailGetInfo.Recipients)
                {
                    MimeMessage message = new();
                    message.To.Add(new MailboxAddress("", address));
                    message.From.Add(new MailboxAddress(_serverConfiguration.Name, _serverConfiguration.EmailFrom));
                    message.Subject = mailGetInfo.Subject ?? "";
                    message.Body = new TextPart("plain")
                    {
                        Text = mailGetInfo.Body ?? ""
                    };
                    messages.Add(message);
                }
            }
            return messages;
        }

        ///<inheritdoc cref="ISendEMailService.SendSeveralEmailMessages(MailJSONRequest)/>
        public void SendSeveralEmailMessages(MailJSONRequest mailGetInfo)
        {
            using SmtpClient client = new();

            try
            {
                List<MimeMessage> mails = CreatingEmailMessages(mailGetInfo);
                client.Connect(_serverConfiguration.ServerName, _serverConfiguration.Host, true);
                client.Authenticate(_serverConfiguration.EmailFrom, _serverConfiguration.Password);
                if (mails.Count == 0)
                {
                    _logMailService.Logging(mailGetInfo, "Failed", "Recipients is null");
                }
                else
                {
                    foreach (MimeMessage m in mails)
                    {
                        SendEmailMessage(m, client);
                    }
                }
            }
            catch (Exception e)
            {
                _logMailService.Logging(mailGetInfo, "Failed", e.Message);
            }
            finally
            {
                client.Disconnect(true);
            }
        }

        ///<inheritdoc cref="ISendEMailService.SendEmailMessage(MimeMessage, SmtpClient)/>
        public void SendEmailMessage(MimeMessage email, SmtpClient client)
        {
            MailJSONRequest logInfo = new ()
            {
                Body = email.Body.ToString(),
                Subject = email.Subject,
                Recipients = new string[] { email.To.ToString() }
            };
            try
            {
                client.Send(email);              
                _logMailService.Logging(logInfo, "OK", "");
            }
            catch(Exception e)
            {
                _logMailService.Logging(logInfo, "Failed", e.Message);
            }
        }
    }
}
