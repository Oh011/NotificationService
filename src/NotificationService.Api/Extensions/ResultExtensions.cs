
using NotificationService.Api.Responses;
using NotificationService.Shared.Errors;
using NotificationService.Shared.Results;
using System.Net;

namespace NotificationService.Api.Extensions
{
    public static class ResultExtensions
    {

        private static HttpStatusCode MapError(ErrorType? error)
        {
            return error switch
            {
                ErrorType.NotFound => HttpStatusCode.NotFound,
                ErrorType.Unauthorized => HttpStatusCode.Unauthorized,
                ErrorType.Forbidden => HttpStatusCode.Forbidden,
                ErrorType.Conflict => HttpStatusCode.Conflict,
                ErrorType.Validation => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };
        }


        private static HttpStatusCode MapSuccess(SuccessType? successType) => successType switch
        {
            SuccessType.Created => HttpStatusCode.Created,
            SuccessType.NoContent => HttpStatusCode.NoContent,
            _ => HttpStatusCode.OK
        };

        public static IApiResponse ToApiResponse<T>(this Result<T> result)
        {
            var statusCode = HttpStatusCode.OK; 

            if (result.IsSuccess)
            {

                 statusCode = MapSuccess(result.SuccessType);

                if (result.Value is not null)
                    return ApiResponseFactory.Success(result.Value, statusCode);

                else if (result.Value is null && result.Message is not null)
                    return ApiResponseFactory.Success(result.Message, statusCode);

                    return ApiResponseFactory.Success(result.Message ?? "Operation completed successfully",statusCode);
            }

            statusCode = MapError(result.Error);

            if (result.ValidationErrors is not null && result.ValidationErrors.Any())
            {
                return ApiResponseFactory.Failure(
                    result.Message ?? "Validation failed",
                    result.ValidationErrors,
                    statusCode
                );
            }

            return ApiResponseFactory.Failure(
                result.Message ?? "Operation failed",
                statusCode
            );
        }

    }

}
