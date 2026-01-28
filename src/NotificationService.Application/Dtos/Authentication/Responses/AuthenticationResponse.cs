using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Dtos.Authentication.Responses
{
    public record AuthenticationResponse(string Email , string UserId , string UserName , string AccessToken);
   
}
