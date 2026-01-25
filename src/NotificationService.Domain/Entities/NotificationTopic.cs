using NotificationService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain.Entities
{
    public class NotificationTopic:BaseEntity
    {

        public int Id { get; set; }
        public string Name { get; set; }  // e.g., "Marketing Updates"
        public string Description { get; set; }

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        public ICollection<UserNotificationSubscription> Subscriptions { get; set; } = new List<UserNotificationSubscription>();    
        public bool IsActive { get; set; } = true;
    }
}
