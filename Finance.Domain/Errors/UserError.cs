using Finance.Domain.Utils.Result;
using System.Net;
using static Finance.Domain.Constants.Constant;

namespace Finance.Domain.Errors
{
    public static class UserError
    {
        public static CustomError NotFound => new CustomError(
            HttpStatusCode.NotFound, ErrorCode.User.NotFound, ErrorMessage.User.NotFound);

        public static CustomError FailedToCreate => new CustomError(
            HttpStatusCode.InternalServerError, ErrorCode.User.FailedToCreate, ErrorMessage.User.FailedToCreate);

        public static CustomError EmailAlreadyInUse => new CustomError(
            HttpStatusCode.BadRequest, ErrorCode.User.EmailAlreadyInUse, ErrorMessage.User.EmailAlreadyInUse);
    }
}