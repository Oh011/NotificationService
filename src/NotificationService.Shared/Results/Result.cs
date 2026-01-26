using NotificationService.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Shared.Results
{
    public class Result
    {

        public bool IsSuccess { get; }
        public string? Message { get; }

        public ErrorType? Error { get; }


        protected Result(bool isSuccess, string? message, ErrorType? error)
        {
            IsSuccess = isSuccess;

            Message = message;
            Error = error;
        }


        public static Result Success(string? message = null)
            => new(true, message,null);


        public static Result Failure(ErrorType error , string? message = null)
            => new(false, message,error);


    }



}
