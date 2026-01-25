using NotificationService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain.Entities
{
    public class NotificationCategory:BaseEntity
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public ICollection<Notification> Notifications { get; set; }=new List<Notification>();  
        public bool IsActive { get; set; } = true; // Allow deactivating old categories
       
    }
}
