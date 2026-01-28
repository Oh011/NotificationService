using NotificationService.Application.Dtos.Authentication.Requests;
using NotificationService.Application.Dtos.Authentication.Responses;
using NotificationService.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Abstractions.Security
{
    public interface IAuthenticationService
    {



          Task<Result<LogInUserResponse>> LogInAsync(LoginUserRequest requestDto);


        Task<Result<Unit>> RegisterAsync(RegisterRequest requestDto);
    }
}
