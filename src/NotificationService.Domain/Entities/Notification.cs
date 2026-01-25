using NotificationService.Domain.Common;
using NotificationService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain.Entities
{
    public class Notification : BaseEntity
    {

        public Guid Id { get; set; }

        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;

        public NotificationChannel Channels { get; set; } // enum

        public DateTime? ScheduledAt { get; set; }

        public int CategoryId { get; set; }

        public NotificationCategory Category { get; set; }


        public NotificationTopic ? Topic { get; set; }

        public string? RecipientUserId { get; set; }  // null if broadcast
        public int ? TopicId { get; set; }


    }
}
