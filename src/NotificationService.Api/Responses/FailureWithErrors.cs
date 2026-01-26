using NotificationService.Shared.Errors;
using System.Net;

namespace NotificationService.Api.Responses
{
    public class FailureWithErrors : IApiResponseWithErrors
    {
        public bool Success { get; init; } = false;
        public int StatusCode { get; init; }
        public string Message { get; init; }
        public Dictionary<string, List<ValidationErrorDetail>> Errors { get; init; }

        public FailureWithErrors(string message, Dictionary<string, List<ValidationErrorDetail>> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            Message = message;
            Errors = errors;
            StatusCode = (int)statusCode;
        }
    }


}
