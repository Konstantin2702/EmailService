
namespace MailService
{
    public class ServerConfiguration
    {
        /// <summary>
        /// Username email
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User email from which email's messages are sent
        /// </summary>
        public string EmailFrom { get; set; }

        /// <summary>
        /// User password of email
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// SMTP server name
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Host of SMTP server
        /// </summary>
        public int Host { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">Configuration data</param>
        public ServerConfiguration(IConfiguration configuration)
        {         
            Name = configuration["SMTPServer:Name"];
            EmailFrom = configuration["SMTPServer:EmailFrom"];
            Password = configuration["SMTPServer:Password"];
            ServerName = configuration["SMTPServer:ServerName"];

            if (int.TryParse(configuration["SMTPServer:Host"], out int res))
            {
                Host = res;
            }
            else
            {
                Host = 465;
            }
        }
    }
}
