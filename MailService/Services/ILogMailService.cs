using MailService.JSONModels;

namespace MailService.Services
{
    public interface ILogMailService
    {
        /// <summary>
        /// Saves information about sent email's message in DB
        /// </summary>
        /// <param name="mailGet">Information about email's message, that need to send</param>
        /// <param name="result">Result of sent email message</param>
        /// <param name="failedMessage">Text of error sent email message</param>
        public void Logging(MailJSONRequest mailGet, string result, string failedMessage);

        /// <summary>
        /// Gets information about sent email's messages from DB
        /// </summary>
        /// <returns>JSON data in string</returns>
        public string GetJSONLog();
    }
}
