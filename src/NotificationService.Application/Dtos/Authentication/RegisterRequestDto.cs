using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Dtos.Authentication
{
    public record RegisterRequestDto(string Email, string Password, string UserName);
  
}
