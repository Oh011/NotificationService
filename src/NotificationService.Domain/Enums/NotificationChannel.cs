using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain.Enums
{
    [Flags]
    public enum NotificationChannel
    {

        None = 0,
        InApp = 1,
        Push = 2,
        Email = 4,
        SMS = 8

    }
}
