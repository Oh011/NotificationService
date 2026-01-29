using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Shared.Dtos
{
    public class EmailAttachment
    {

        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = "application/octet-stream"; // Default

        //--> is the default MIME type for unknown binary data.
        public byte[] Content { get; set; } = Array.Empty<byte>();
    }
}
