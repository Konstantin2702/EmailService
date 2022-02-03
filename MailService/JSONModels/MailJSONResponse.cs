namespace MailService.JSONModels
{
    /// <summary>
    /// Data sent by GET request
    /// </summary>
    public class MailJSONResponse
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
        /// Recipient of email message
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// Date of send email message
        /// </summary>
        public DateTime DateofCreation { get; set; }

        /// <summary>
        /// Result of sent email message
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Text of error sent email message
        /// </summary>
        public string FailedMessage { get; set; }
    }
}
