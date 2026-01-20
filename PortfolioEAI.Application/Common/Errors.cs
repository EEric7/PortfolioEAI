

namespace PortfolioEAI.Application.Common
{
    public static class Errors
    {
        public static class Users
        {
            public const string NotFound = "Users.Not_found";
            public const string EmailAlreadyExists = "Users.Email_already_exists";
            public const string InvalidInput = "Users.Invalid_input";
        }

        public static class Validation
        {
            public const string Failed = "Validation.Failed";
        }
    }
}