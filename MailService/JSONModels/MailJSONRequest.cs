namespace MailService.JSONModels
{
    /// <summary>
    /// Data received by POST request
    /// </summary>
    public class MailJSONRequest
    {
        /// <summary>
        /// Subject of email message
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Body of email message
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Recipients of email message
        /// </summary>
        public string[] Recipients { get; set; }        
    }
}
