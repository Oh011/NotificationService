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


        public SuccessType ? SuccessType { get;  }


        protected Result(bool isSuccess, string? message, SuccessType ? successType, ErrorType? error)
        {
            IsSuccess = isSuccess;

            Message = message;
            Error = error;
            this.SuccessType=successType;
        }


        public static Result Success(string? message = null,SuccessType ?successType=null)
            => new(true, message,null,null);


        public static Result Failure(ErrorType error , string? message = null)
            => new(false, message,null,error);


    }



}
