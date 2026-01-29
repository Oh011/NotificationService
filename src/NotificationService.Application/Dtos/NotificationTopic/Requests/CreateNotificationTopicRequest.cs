using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Dtos.NotificationTopic.Requests
{
    public class CreateNotificationTopicRequest
    {

        public string Name { get; set; }
        public string Description { get; set; } 
    }
}
