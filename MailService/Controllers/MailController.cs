using MailService.JSONModels;
using MailService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MailService.Controllers
{
    public class MailController : ControllerBase
    {
        private readonly ILogMailService _logMailService;
        private readonly ISendEMailService _sendEMailService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logMailService">Service that save information in DB about sending email's messages</param>
        /// <param name="sendEMailService">Service that sends email's messages</param>
        public MailController(ILogMailService logMailService, ISendEMailService sendEMailService)
        {
            _logMailService = logMailService;
            _sendEMailService = sendEMailService;
        }

        /// <summary>
        /// Sending data about sent email's messages
        /// </summary>
        /// <returns>JSON file with information about sent email's messages</returns>
        [HttpGet("/api/mails/")]
        public string SendLogInfo()
        {
            return _logMailService.GetJSONLog();
        }

        /// <summary>
        /// Sending email's messages
        /// </summary>
        /// <param name="mailGetInfo">Information about email's message, that need to send</param>
        [HttpPost("/api/mails/")]
        public void SendEMails(MailJSONRequest mailGetInfo)
        {
            _sendEMailService.SendSeveralEmailMessages(mailGetInfo);
        }
    }
}

