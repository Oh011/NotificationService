using NotificationService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain.Entities
{
    public class NotificationChannelStatus
    {

        public Guid Id { get; set; }                 // PK
        public Guid UserNotificationId { get; set; } // FK to UserNotification

        public UserNotification UserNotification { get; set; }

        public NotificationChannel Channel { get; set; } // Enum: InApp, Push, Email, SMS
        public ChannelDeliveryStatus Status { get; set; } = ChannelDeliveryStatus.Pending;
        public DateTime? DeliveredAt { get; set; }
        public int RetryCount { get; set; } = 0;    // Optional: for retry logic

        public string? FailureReason { get; set; }

    }
}
