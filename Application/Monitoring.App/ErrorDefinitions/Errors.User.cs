using ErrorOr;

namespace Monitoring.App.ErrorDefinitions;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail",
            description: "The email address is already in use");
        
        public static Error UserNotFound => Error.NotFound(
            code: "User.NotFound",
            description: "User was not found");
        
        public static Error WrongPassword => Error.Unauthorized(
            code: "User.WrongPassword",
            description: "The password you entered is incorrect");
    }
}