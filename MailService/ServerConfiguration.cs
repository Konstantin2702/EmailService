using System.ComponentModel.DataAnnotations;

namespace MailService.JSONModels
{
    public class ServerConfiguration
    {
        private readonly IConfiguration _configuration;

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
            _configuration = configuration;
            Name = _configuration["SMTPServer:Name"];
            EmailFrom = _configuration["SMTPServer:EmailFrom"];
            Password = _configuration["SMTPServer:Password"];
            ServerName = _configuration["SMTPServer:ServerName"];

            if (int.TryParse(_configuration["SMTPServer:Host"], out int res))
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
