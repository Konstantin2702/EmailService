using MailKit.Net.Smtp;
using MailService.JSONModels;
using MimeKit;

namespace MailService.Services
{
    public interface ISendEMailService
    {
        /// <summary>
        /// Creates email's messages for sending    
        /// </summary>
        /// <param name="mailGetInfo">Information about email's message, that need to send</param>
        /// <returns>List email's messages that will send</returns>
        List<MimeMessage> CreatingEmailMessages(MailJSONRequest mailGetInfo);

        /// <summary>
        /// Sends email's messages to all recipients
        /// </summary>
        /// <param name="mailGetInfo">Information about email's message, that need to send</param>
        void SendSeveralEmailMessages(MailJSONRequest mailGetInfo);

        /// <summary>
        /// Sends email's message
        /// </summary>
        /// <param name="email">Email's message that will send</param>
        /// <param name="client">SMTP client that sends email's message</param>
        void SendEmailMessage(MimeMessage email, SmtpClient client);
    }
}
