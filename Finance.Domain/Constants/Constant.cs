namespace Finance.Domain.Constants
{
    public static class Constant
    {
        public struct ErrorMessages
        {
            public struct Auth
            {
                public const string UserNotFound = "No user found.";
                public const string InvalidLogin = "Email or password does not match.";
            }
        }
    }
}
