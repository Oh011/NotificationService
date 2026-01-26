using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Shared.Errors
{
    public class ValidationErrorDetail
    {
        public string Message { get; set; }

        public ValidationErrorDetail(string message)
        {
            Message = message;
        }
    }
}
