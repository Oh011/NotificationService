using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Shared.Dtos
{
    public class EmailMessage
    {

        public string To { get; set; }  

        public string Subject { get; set; }


        public string Body { get; set; }

        public bool IsHtml { get; set; } = true;
    


        public List<EmailAttachment> Attachments { get; set; } = new();
    }
}
