using Azure.Core;
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

namespace NotificationService.Infrastructure.Identity.Services
{
    public class AuthenticationService(UserManager<ApplicationUser> userManager,ITokenService tokenService) : IAuthenticationService
    {
        public async Task<Result<LogInUserResponse>> LogInAsync(LoginUserRequest requestDto)
        {
            
            var user = await userManager.FindByEmailAsync(requestDto.Email);


            


            if(user==null) return Result<LogInUserResponse>.Failure(AuthErrorMessages.INVALID_CREDENTIALS,ErrorType.Unauthorized);



            if(await userManager.IsLockedOutAsync(user))
            {
                return Result<LogInUserResponse>.Failure(AuthErrorMessages.ACCOUNT_LOCKED,ErrorType.Forbidden);
            }

            var passwordValid=await userManager.CheckPasswordAsync(user,requestDto.Password);
            


            if(!passwordValid)
            {
                await userManager.AccessFailedAsync(user);

                if (await userManager.IsLockedOutAsync(user))
                    return Result<LogInUserResponse>.Failure(AuthErrorMessages.ACCOUNT_LOCKED, ErrorType.Forbidden);

                return Result<LogInUserResponse>.Failure(AuthErrorMessages.INVALID_CREDENTIALS,ErrorType.Unauthorized);
            }

            await userManager.ResetAccessFailedCountAsync(user);

            var userRoles=await userManager.GetRolesAsync(user);

            var RefreshToken=await tokenService.CreateRefreshTokenForDevice(user.Id,requestDto.DeviceId);
            var AccessToken= tokenService.GenerateAccessToken(user.Id,user.Email,user.UserName,userRoles);   




            var authenticationResponse = new AuthenticationResponse
           (
             user.Email, user.Id, user.UserName, AccessToken
           );
            var response = new LogInUserResponse
            (

               authenticationResponse, RefreshToken
            );


            return Result<LogInUserResponse>.Success(response);

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
