using NotificationService.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Shared.Results
{

    public class Result<T> : Result
    {

        public Dictionary<string, List<ValidationErrorDetail>>? ValidationErrors { get; }
        public T? Value { get; }
       

        private Result(bool isSuccess, T? value, string? message,
                       Dictionary<string, List<ValidationErrorDetail>>? validationErrors, SuccessType ?successType,
                       ErrorType? error) : base(isSuccess, message , successType , error)
        {


            ValidationErrors = validationErrors;
            this.Value= value;

        }

        public static Result<T> Success(T value, string? message = null,SuccessType ?successType=null)
            => new(true, value, message, null,successType, null);

        //  Success with no data (used in commands)

        public static Result<Unit> Success(string? message = null, SuccessType? successType=null)
           => new(true, null, message, null,successType, null);


        //  Failure returning the same T type (safe for queries)
        public static Result<T> Failure(string message, ErrorType error = ErrorType.Internal)
            => new(false, default, message, null,null, error);

        public static Result<T> Failure(string message,
                                        Dictionary<string, List<ValidationErrorDetail>> validationErrors,
                                        ErrorType error = ErrorType.Validation)
            => new(false, default, message, validationErrors,null, error);

        //  Failure with no data (safe for commands)
        public static Result<Unit> FailureUnit(string message, ErrorType error = ErrorType.Internal)
            => new(false, default, message, null,null, error);

        public static Result<Unit> FailureUnit(string message,
                                               Dictionary<string, List<ValidationErrorDetail>> validationErrors,
                                               ErrorType error = ErrorType.Validation)
            => new(false, default, message, validationErrors,null ,error);
    }

    public sealed class Unit
    {
        public static readonly Unit Value = new Unit();
        private Unit() { }
    }
}
