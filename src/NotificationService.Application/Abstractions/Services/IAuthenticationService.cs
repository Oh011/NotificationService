using NotificationService.Application.Dtos.Authentication;
using NotificationService.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Abstractions.Services
{
    public interface IAuthenticationService
    {



        public Task<Result<Unit>> RegisterAsync(RegisterRequestDto requestDto);
    }
}
