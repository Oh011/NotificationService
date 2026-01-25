using NotificationService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain.Entities
{
    public class UserNotificationPreference
    {

        public Guid Id { get; set; }

        public string UserId { get; set; }   // AspNetUsers.Id

        public NotificationCategory Category { get; set; }

        public int CategoryId { get; set; } 


        public NotificationCategory NotificationCategory { get; set; }
        // enum: PaymentFailed, OrderShipped, StockLow, etc.

        public NotificationChannel Channel { get; set; }
        // enum: InApp, Email, Push, SMS

        public bool IsEnabled { get; set; } = true;
    }
}
