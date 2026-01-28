using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Api.Extensions;
using NotificationService.Api.Responses;
using NotificationService.Application.Abstractions.Security;
using NotificationService.Application.Dtos.Authentication.Requests;
using Swashbuckle.AspNetCore.Annotations;

namespace NotificationService.Api.Controllers
{


    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
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
    }
}
