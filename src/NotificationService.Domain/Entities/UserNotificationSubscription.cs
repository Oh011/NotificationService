using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain.Entities
{
    public class UserNotificationSubscription
    {

        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int TopicId { get; set; }

    

        public NotificationTopic Topic { get; set; }
        public DateTime SubscribedAt { get; set; }
        public DateTime? UnsubscribedAt { get; set; } // null if still subscribed
    }
}
