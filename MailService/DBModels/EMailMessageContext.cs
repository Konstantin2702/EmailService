using Microsoft.EntityFrameworkCore;

namespace MailService.DBModels
{
    public class EMailMessageContext:DbContext
    {
        /// <summary>
        /// Context of MailContent
        /// </summary>
        public DbSet<EMailMessageContent> MailContent => Set<EMailMessageContent>();

        /// <summary>
        /// Context of MailSendingReport
        /// </summary>
        public DbSet<EMailMessageReport> MailSendingReport => Set<EMailMessageReport>();

        public DbSet<FailedMessage> FailedMessage => Set<FailedMessage>();


        public DbSet<Recipient> Recipient => Set<Recipient>();


        public EMailMessageContext(DbContextOptions<EMailMessageContext> options)
        : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
