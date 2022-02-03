using MailService.DBModels;
using MailService.JSONModels;
using System.Text.Json;

namespace MailService.Services
{
   
    public class LogMailService: ILogMailService
    {
        private readonly EMailMessageContext _db;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db">DB context/// </param>
        public LogMailService(EMailMessageContext db)
        {
            _db = db;
        }

        ///<inheritdoc cref="ILogMailService.Logging/>
        public void Logging(MailJSONRequest mailGet, string result, string failedMessage)
        {
            EMailMessageContent mailContent = new()
            {
                Body = mailGet.Body,
                Subject = mailGet.Subject
            };

            

            List<EMailMessageReport> reports = new();

            if (mailGet.Recipients != null)
            {
                foreach (string str in mailGet.Recipients)
                {
                    EMailMessageReport eMailMessageReport = new()
                    {
                        MailContent = mailContent,
                        DateofCreation = DateTime.Now.Date,
                        Result = result,
                    };

                    var existingRecipient = (from rec in _db.Recipient where rec.Email == str select rec).ToList();
                    var existingFailedMessage = (from f in _db.FailedMessage where f.Text == failedMessage select f).ToList();
                    if (existingRecipient.Count == 0)
                    {
                        Recipient recipientToAdd = new()
                        {
                            Email = str,
                        };
                        _db.Recipient.Add(recipientToAdd);
                        eMailMessageReport.Recipient = recipientToAdd;
                    }
                    else
                    {
                        eMailMessageReport.Recipient = existingRecipient[0];
                    }

                    if (existingFailedMessage.Count == 0)
                    {
                        FailedMessage failedMessageToAdd = new()
                        {
                            Text = failedMessage,
                        };
                        _db.FailedMessage.Add(failedMessageToAdd);
                        eMailMessageReport.FailedMessages = failedMessageToAdd;
                    }
                    else
                    {
                        eMailMessageReport.FailedMessages = existingFailedMessage[0];
                    }

                    _db.MailSendingReport.Add(eMailMessageReport);
                }
            }
            else
            {
                EMailMessageReport eMailMessageReport = new()
                {
                    MailContent = mailContent,
                    DateofCreation = DateTime.Now.Date,
                    Result = result,
                };
                var existingFailedMessage = (from f in _db.FailedMessage where f.Text == failedMessage select f).ToList();

                if (existingFailedMessage.Count == 0)
                {
                    FailedMessage failedMessageToAdd = new()
                    {
                        Text = failedMessage,
                    };
                    _db.FailedMessage.Add(failedMessageToAdd);
                    eMailMessageReport.FailedMessages = failedMessageToAdd;
                }
                else
                {
                    eMailMessageReport.FailedMessages = existingFailedMessage[0];
                }

                _db.MailSendingReport.Add(eMailMessageReport);
            }
            _db.SaveChanges();
        
        }

        ///<inheritdoc cref="ILogMailService.GetJSONLog/>
        public string GetJSONLog()
        {

            var mailsToSend = from report in _db.MailSendingReport
                             from content in _db.MailContent
                             from message in _db.FailedMessage
                             from recipient in _db.Recipient
                             where report.Recipient.Id == recipient.Id
                             && report.FailedMessages.Id == message.Id
                             && report.MailContent.Id == content.Id
                             select new MailJSONResponse
                             {
                                 Subject = content.Subject,
                                 Body = content.Body,
                                 DateofCreation = report.DateofCreation,
                                 Result = report.Result,
                                 FailedMessage = message.Text,
                                 Recipient = recipient.Email
                             };
            return JsonSerializer.Serialize(mailsToSend.ToArray());
        }
    }
}
