using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Dtos.Authentication.Responses
{
    public record LogInUserResponse(AuthenticationResponse AuthenticationResponse,string RefreshToken);
    
}
