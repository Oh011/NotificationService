using Microsoft.AspNetCore.Identity;
using NotificationService.Application.Abstractions.Services;
using NotificationService.Application.Dtos.Authentication;
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
        public async Task<Result<Unit>> RegisterAsync(RegisterRequestDto requestDto)
        {
          
            var userExists=await userManager.FindByEmailAsync(requestDto.Email);


            if (userExists != null)
            {

                return Result<Unit>.FailureUnit("User with this email already exists.",ErrorType.Conflict);
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


            return Result<Unit>.Success("User registered successfully.");
        }
    }
}
