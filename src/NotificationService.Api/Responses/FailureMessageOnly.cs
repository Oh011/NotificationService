using System.Net;

namespace NotificationService.Api.Responses
{
    public class FailureMessageOnly : IApiResponseWithMessage
    {
        public bool Success { get; init; } = false;
        public int StatusCode { get; init; }
        public string Message { get; init; }

        public FailureMessageOnly(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            Message = message;
            StatusCode = (int)statusCode;
        }
    }
}
