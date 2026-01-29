using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Api.Extensions;
using NotificationService.Api.Responses;
using NotificationService.Application.Abstractions.Security;
using NotificationService.Application.Dtos.Authentication.Requests;
using NotificationService.Application.Dtos.Authentication.Responses;
using NotificationService.Shared.Errors;
using NotificationService.Shared.Results;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace NotificationService.Api.Controllers
{


    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService authenticationService,ITokenService tokenService) : ControllerBase
    {


        /// <summary>
        /// Registers a new user with the provided email, username, and password.
        /// </summary>
        [HttpPost("register")]

      
        [ProducesResponseType(typeof(SuccessMessage), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(FailureWithErrors), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(FailureMessageOnly), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(FailureMessageOnly), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IApiResponse>> RegisterUser(RegisterRequest request)
        {


            var result=await authenticationService.RegisterAsync(request);

        
            var response=result.ToApiResponse();

            return StatusCode(response.StatusCode, response);
        }


        [HttpPost("LogIn")]


        [ProducesResponseType(typeof(SuccessMessage), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(FailureWithErrors), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(FailureMessageOnly), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(FailureMessageOnly), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IApiResponse>> LogInUser(LoginUserRequest request)
        {

            var result = await authenticationService.LogInAsync(request);

            var response = result.ToApiResponse();

            if (!result.IsSuccess)
                return StatusCode(response.StatusCode, result.ToApiResponse());

            SetCookie(14,result.Value.RefreshToken);

             response = ApiResponseFactory.Success(
                result.Value.AuthenticationResponse,
                HttpStatusCode.OK
            );

            return Ok(response);

        }

        [HttpPost("refresh-access")]
        public async Task<ActionResult<IApiResponse>> RefreshAccessToken([FromBody] string DeviceId)
        {

            var ExtractRefreshTokenResult = ExtractRefreshToken(Request);

            var response = ExtractRefreshTokenResult.ToApiResponse();



            if (ExtractRefreshTokenResult.IsSuccess)
            {


            var result = await tokenService.RefreshAccessTokenAsync(ExtractRefreshTokenResult.Value,DeviceId);

             response = result.ToApiResponse();

            if (result.IsSuccess)
            {

                response = ApiResponseFactory.Success<AuthenticationResponse>(result.Value.AuthenticationResponse
                   , HttpStatusCode.OK);

                SetCookie(14, result.Value.RefreshToken);


                return Ok(response);
            }
            }
         
            return StatusCode(response.StatusCode, response);


        }



        [Authorize]
        [HttpPost("logout")]
       
        public async Task<ActionResult<SuccessMessage>> LogOut([FromBody] string DeviceId)
        {


            var ExtractRefreshTokenResult = ExtractRefreshToken(Request);

            var response = ExtractRefreshTokenResult.ToApiResponse();



            if (ExtractRefreshTokenResult.IsSuccess)
            {


                var result = await tokenService.RevokeRefreshTokenByToken(ExtractRefreshTokenResult.Value, DeviceId);

                response = result.ToApiResponse();


                if(result.IsSuccess)
                    Response.Cookies.Delete("refreshToken");


            }

                  
            return StatusCode(response.StatusCode, response);
        }






        private Result<string> ExtractRefreshToken(HttpRequest request)
        {

            var refreshToken = request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return Result<string>.Failure("Refresh token missing.",ErrorType.Unauthorized);
                
            }

            return Result<string>.Success(value:refreshToken);
        }



        private void SetCookie(int days, string token)
        {

            Response.Cookies.Append("refreshToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Only if you're using HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(days)
            });


        }
    }
}
