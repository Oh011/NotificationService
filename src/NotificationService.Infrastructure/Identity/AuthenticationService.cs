using Microsoft.AspNetCore.Identity;
using NotificationService.Application.Abstractions.Security;
using NotificationService.Application.Common.Errors;
using NotificationService.Application.Common.Messages;
using NotificationService.Application.Dtos.Authentication.Requests;
using NotificationService.Application.Dtos.Authentication.Responses;
using NotificationService.Infrastructure.Identity.Models;
using NotificationService.Shared.Errors;
using NotificationService.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure.Identity
{
    public class AuthenticationService(UserManager<ApplicationUser> userManager) : IAuthenticationService
    {
        public Task<Result<LogInUserResponse>> LogInAsync(LoginUserRequest requestDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Unit>> RegisterAsync(RegisterRequest requestDto)
        {
          
            var userExists=await userManager.FindByEmailAsync(requestDto.Email);


            if (userExists != null)
            {

                return Result<Unit>.FailureUnit(AuthErrorMessages.USER_ALREADY_EXISTS,ErrorType.Conflict);
            }


            var user = new ApplicationUser
            {
                UserName = requestDto.UserName,
                Email = requestDto.Email,
                      
            };  


            var result = await userManager.CreateAsync(user, requestDto.Password);


            if (!result.Succeeded)
            {

                var errors=result.Errors
                    .GroupBy(e => e.Code)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => new ValidationErrorDetail(e.Description)
                        ).ToList()
                    );  

                return Result<Unit>.FailureUnit("User registration failed due to validation errors.", errors, ErrorType.Validation);
            }


            return Result<Unit>.Success(AuthSuccessMessages.USER_REGISTERED,SuccessType.Created);
        }
    }
}
