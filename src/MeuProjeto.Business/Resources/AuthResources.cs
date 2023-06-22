using System.Collections.Generic;
using System.Reflection;

namespace MeuProjeto.Business.Resources
{
    public static class AuthResources
    {
        public static class Errors
        {
            public static class UserBlocked
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "User temporarily blocked by invalid attempts."; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value); } }
            }
            public static class IncorrectUserPassword
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Incorrect username or password."; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value); } }
            }

            public static class ConcurrencyFailure
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Optimistic concurrency failure, object has been modified."; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value); } }
            }
            public static class PasswordMismatch
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Incorrect password."; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value); } }
            }
            public static class InvalidToken
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Invalid token."; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value); } }
            }
            public static class UserLockoutNotEnabled
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Lockout is not enabled for this user."; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value); } }
            }
            public static class RefreshTokenNotFound
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Refresh token not found."; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value); } }
            }
            public static class RefreshTokenInactive
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Refresh token is no longer active."; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value); } }
            }
            public static class RefreshTokenRequired
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Refresh token is required."; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value); } }
            }
        }
    }
}