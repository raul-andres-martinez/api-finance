namespace Finance.Domain.Enum
{
    public enum ErrorCode
    {
        NONE,
        INVALID_REQUEST,
        EMAIL_IN_USE,
        RESOURCE_NOT_FOUND,
        USER_NOT_FOUND,
        DATABASE_ERROR,
        INTERNAL_ERROR
    }

    public static class ErrorCodeExtensions
    {
        public static int ToStatusCode(this ErrorCode err)
        {
            return err switch
            {
                ErrorCode.NONE => 200,
                ErrorCode.INVALID_REQUEST => 400,
                ErrorCode.EMAIL_IN_USE => 409,
                ErrorCode.RESOURCE_NOT_FOUND => 404,
                ErrorCode.USER_NOT_FOUND => 401,
                ErrorCode.DATABASE_ERROR => 500,
                ErrorCode.INTERNAL_ERROR => 500,
                _ => 500
            };
        }
    }
}
