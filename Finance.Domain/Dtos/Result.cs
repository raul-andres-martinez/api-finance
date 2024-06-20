namespace Finance.Domain.Dtos
{
    public class Result
    {
        protected Result(bool success, string error)
        {
            if (success && !string.IsNullOrEmpty(error))
                throw new InvalidOperationException("A successful result cannot have an error message.");

            if (!success && string.IsNullOrEmpty(error))
                throw new InvalidOperationException("A failure result must have an error message.");

            Success = success;
            Error = error;
        }

        public bool Success { get; set; }
        public string Error { get; set; }

        public static Result Failure(string message)
        {
            return new Result(false, message);
        }

        public static Result<T> Failure<T>(string message)
        {
            return new Result<T>(default(T), false, message);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }

        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }
    }

    public class Result<T> : Result
    {
        public T? Value { get; set; }

        protected internal Result(T? value, bool success, string error)
            : base(success, error)
        {
            Value = value;
        }
    }
}
