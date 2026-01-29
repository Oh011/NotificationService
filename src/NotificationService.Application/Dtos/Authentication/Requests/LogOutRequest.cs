using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Dtos.Authentication.Requests
{
    public class LogOutRequest
    {

        public string? RefreshToken { get; set; }
        public string DeviceId { get; set; } = default!;
    }
}
