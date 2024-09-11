using Finance.Domain.Utils.Result;
using System.Net;

namespace Finance.Domain.Errors
{
    public static class ExceptionError
    {
        public static CustomError InternalError(HttpStatusCode statusCode, string message) => new CustomError(
            statusCode, "Exception.InternalError", message);
    }
}