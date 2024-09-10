using Finance.Domain.Utils.Result;
using System.Net;
using static Finance.Domain.Constants.Constant;

namespace Finance.Domain.Errors
{
    public static class ExpenseError
    {
        public static CustomError InvalidUser => new CustomError(
            HttpStatusCode.Forbidden, ErrorCode.Expense.InvalidUser, ErrorMessage.Expense.InvalidUser);

        public static CustomError FailedToCreate => new CustomError(
            HttpStatusCode.InternalServerError, ErrorCode.Expense.FailedToCreate, ErrorMessage.Expense.FailedToCreate);
    }
}
