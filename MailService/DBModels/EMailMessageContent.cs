using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailService.DBModels
{
    
    public class EMailMessageContent
    {
        /// <summary>
        /// Id of email 
        /// </summary> 
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Email's subject
        /// </summary>   
        public string? Subject { get; set; }

        /// <summary>
        /// Email's body
        /// </summary>      
        public string? Body { get; set; }

        /// <summary>
        /// Email delivery reports 
        /// </summary>        
        public List<EMailMessageReport> Reports { get; set; }

       
    }
    
}
