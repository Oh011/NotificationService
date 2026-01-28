using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Dtos.Authentication.Requests
{
    public record LoginUserRequest(string Email , string Password , string DeviceId);
   
}
