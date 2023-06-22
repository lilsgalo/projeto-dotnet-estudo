using System.Collections.Generic;
using System.Reflection;

namespace MeuProjeto.Business.Resources
{
    public static class UserResources
    {
        public static class ConfirmNewPassword
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "Confirm New Password"; } }
        }
        public static class ConfirmPassword
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "Confirm Password"; } }
        }
        public static class Email
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "Email"; } }
        }
        public static class Id
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "Id"; } }
        }
        public static class Name
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "Name"; } }
        }
        public static class NewPassword
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "New Password"; } }
        }
        public static class Password
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "Password"; } }
        }
        public static class PhoneNumber
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "Phone Number"; } }
        }
        public static class Profile
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "Profile"; } }
        }
        public static class UserName
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "UserName"; } }
        }
        public static class Errors
        {
            public static class EmailIsTaken
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "This email address is taken."; } }
                public static List<string> Params { get { return null; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }
            public static class InvalidUserNamePassword
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Invalid UserName or Password."; } }
                public static List<string> Params { get { return null; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }
            public static class NotFound
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "User not found."; } }
                public static List<string> Params { get { return null; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }
            public static class UserNameIsTaken
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "That username is taken. Try another."; } }
                public static List<string> Params { get { return null; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }





            public static class LoginAlreadyAssociated
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "A user with this login already exists."; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value); } }
            }
            public static class InvalidUserName
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "User name '{userName}' is invalid, can only contain letters or digits."; } }
                public static List<string> Params { get { return new List<string> { "{userName}" }; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value); } }
            }
            public static class InvalidEmail
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Email '{email}' is invalid."; } }
                public static List<string> Params { get { return new List<string> { "{email}" }; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value); } }
            }
            public static class DuplicateUserName
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "User Name '{userName}' is already taken."; } }
                public static List<string> Params { get { return new List<string> { "{userName}" }; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }
            public static class DuplicateEmail
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Email '{email}' is already taken."; } }
                public static List<string> Params { get { return new List<string> { "{email}" }; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }
            public static class InvalidRoleName
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Role name '{role}' is invalid."; } }
                public static List<string> Params { get { return new List<string> { "{role}" }; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }
            public static class DuplicateRoleName
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Role name '{role}' is already taken."; } }
                public static List<string> Params { get { return new List<string> { "{role}" }; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }
            public static class UserAlreadyHasPassword
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "User already has a password set."; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value); } }
            }
            public static class UserAlreadyInRole
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "User already in role '{role}'."; } }
                public static List<string> Params { get { return new List<string> { "{role}" }; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }
            public static class UserNotInRole
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "User is not in role '{role}'."; } }
                public static List<string> Params { get { return new List<string> { "{role}" }; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }
            public static class PasswordTooShort
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Passwords must be at least {length} characters."; } }
                public static List<string> Params { get { return new List<string> { "{length}" }; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }
            public static class PasswordRequiresNonAlphanumeric
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Passwords must have at least one non alphanumeric character."; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value); } }
            }
            public static class PasswordRequiresDigit
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Passwords must have at least one digit ('0'-'9')."; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value); } }
            }
            public static class PasswordRequiresLower
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Passwords must have at least one lowercase ('a'-'z')."; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value); } }
            }
            public static class PasswordRequiresUpper
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Passwords must have at least one uppercase ('A'-'Z')."; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value); } }
            }
        }
    }
}