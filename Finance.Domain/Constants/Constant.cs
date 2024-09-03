namespace Finance.Domain.Constants
{
    public static class Constant
    {
        public struct ErrorMessage
        {
            public struct User
            {
                public const string NotFound = "User not found.";
                public const string FailedToCreate = "Failed to create user.";
                public const string EmailAlreadyInUse = "Email already in use.";
            }

            public const string InvalidLogin = "Email or password does not match.";            
        }

        public struct ErrorCode
        {
            public struct User
            {
                public const string NotFound = "User.NotFound";
                public const string FailedToCreate = "User.FailedToCreate";
                public const string EmailAlreadyInUse = "User.EmailAlreadyInUse";
            }
        }
    }
}
