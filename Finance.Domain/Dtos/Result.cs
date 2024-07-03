using Finance.Domain.Enum;

namespace Finance.Domain.Dtos
{
    public class Result
    {
        protected Result(bool success, string error, ErrorCode errorCode)
        {
            if (success && !string.IsNullOrEmpty(error))
                throw new InvalidOperationException("A successful result cannot have an error message.");

            if (!success && string.IsNullOrEmpty(error))
                throw new InvalidOperationException("A failure result must have an error message.");

            if(success && errorCode != ErrorCode.NONE)
                throw new InvalidOperationException("A successful result cannot have an error code other than NONE.");

            Success = success;
            Error = error;
            ErrorCode = errorCode;
        }

        public bool Success { get; set; }
        public string Error { get; set; }
        public ErrorCode ErrorCode { get; set; }

        public static Result Failure(string message, ErrorCode errorCode)
        {
            return new Result(false, message, errorCode);
        }

        public static Result<T> Failure<T>(string message, ErrorCode errorCode)
        {
            return new Result<T>(default(T), false, message, errorCode);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, string.Empty, ErrorCode.NONE);
        }

        public static Result Ok()
        {
            return new Result(true, string.Empty, ErrorCode.NONE);
        }
    }

    public class Result<T> : Result
    {
        public T? Value { get; set; }

        protected internal Result(T? value, bool success, string error, ErrorCode errorCode)
            : base(success, error, errorCode)
        {
            Value = value;
        }
    }
}
