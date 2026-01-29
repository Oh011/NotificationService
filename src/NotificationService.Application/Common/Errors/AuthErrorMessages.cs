using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Common.Errors
{
    public static class AuthErrorMessages
    {
        public const string USER_ALREADY_EXISTS = "A user with this email already exists.";
        public const string INVALID_CREDENTIALS = "Invalid email or password.";
        public const string ACCOUNT_LOCKED = "This account has been locked.";
        public const string EMAIL_NOT_CONFIRMED = "Email is not confirmed.";
        public const string PASSWORD_RESET_FAILED = "Password reset failed.";
        public const string TOKEN_INVALID_OR_EXPIRED = "Authentication token is invalid or expired.";
        public const string REFRESH_TOKEN_INVALID_OR_EXPIRED = "Invalid or expired refresh token.";
        public const string REVOKED_REFRESH_TOKEN = "This refresh token has already been revoked.";
        public const string USER_NOT_FOUND = "User not found.";
    }
}
