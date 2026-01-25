using NotificationService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain.Entities
{
    public class UserNotification:BaseEntity
    {

        public Guid Id { get; set; }


        public string UserId { get; set; }  

        public Guid NotificationId { get; set; }
        public Notification Notification { get; set; }

        public bool IsRead { get; set; } = false;

        public ICollection<NotificationChannelStatus> NotificationChannelStatuses { get; set; } = new List<NotificationChannelStatus>();

        public DateTime? ReadAt { get; set; }       // Optional timestamp


    }
}
