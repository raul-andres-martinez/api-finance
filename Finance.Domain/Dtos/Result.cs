namespace Finance.Domain.Dtos
{
    public class Result
    {
        protected Result(bool success, string error)
        {
            if(success && error != string.Empty)
                throw new InvalidOperationException();

            if(success && error == string.Empty) 
                throw new InvalidOperationException();

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
            var obj = default(T) ?? throw new InvalidOperationException();

            return new Result<T>(obj, false, message);
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
        public T Value { get; set; }

        protected internal Result(T value, bool success, string error) 
            : base(success, error) 
        {
            Value = value;
        }
    }
}
