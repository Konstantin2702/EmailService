using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailService.DBModels
{
    public class EMailMessageReport
    {
        /// <summary>
        /// Id of report
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// ref on table MailContent
        /// </summary>
        public EMailMessageContent MailContent { get; set; }

        /// <summary>
        /// Email's recipients
        /// </summary>
        public Recipient Recipient { get; set; }

        /// <summary>
        /// Date of send email message
        /// </summary>
        public DateTime DateofCreation { get; set; }

        /// <summary>
        /// Result of sent email messages
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Text of error sent email messages
        /// </summary>
        public FailedMessage FailedMessages { get; set; }
    }
}

