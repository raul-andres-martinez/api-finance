namespace Finance.Domain.Constants
{
    public static class Constant
    {
        public static class TokenType
        {
            public const string Bearer = "Bearer";
        }

        public static class ErrorMessage
        {
            public static class User
            {
                public const string NotFound = "User not found.";
                public const string FailedToCreate = "Failed to create user.";
                public const string EmailAlreadyInUse = "Email already in use.";
                public const string InvalidLogin = "Email or password does not match.";
            }

            public static class Expense
            {
                public static string InvalidUser = "User not authorized.";
                public const string FailedToCreate = "Failed to create expense.";
                public const string NotFound = "Expense not found.";
                public const string FailedToDelete = "Failed to delete expense.";
            }
        }

        public static class ErrorCode
        {
            public static class User
            {
                public const string NotFound = "User.NotFound";
                public const string FailedToCreate = "User.FailedToCreate";
                public const string EmailAlreadyInUse = "User.EmailAlreadyInUse";
                public const string InvalidLogin = "User.InvalidLogin";
            }

            public static class Expense
            {
                public static string InvalidUser = "Expense.InvalidUser";
                public const string FailedToCreate = "Expense.FailedToCreate";
                public const string NotFound = "Expense.NotFound";
                public const string FailedToDelete = "Expense.FailedToDelete";
            }
        }
    }
}