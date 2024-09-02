using System.Net;
using System.Text.Json.Serialization;

namespace Finance.Domain.Utils.Result
{
    public class CustomError
    {
        public CustomError(HttpStatusCode statusCode, string code, string message, object? value = default)
        {
            StatusCode = statusCode;
            Code = code;
            Message = message;
            Value = value;
        }

        [JsonIgnore]
        public HttpStatusCode StatusCode { get; }
        public string Code { get; }
        public string Message { get; }
        public object? Value { get; }
    }
}
