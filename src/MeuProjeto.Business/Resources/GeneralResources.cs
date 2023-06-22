using System.Collections.Generic;
using System.Reflection;

namespace MeuProjeto.Business.Resources
{
    public static class GeneralResources
    {
        public static class Errors
        {
            public static class DefaultError
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "An unknown failure has occurred."; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value); } }
            }
        }
        public static class Validations
        {
            public static class Required
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "The field {0} is required."; } }
                public static List<string> Params { get { return new List<string> { "{0}" }; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }
            public static class StringLength
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "The field {0} must be a string with a minimum length of {2} and a maximum length of {1}."; } }
                public static List<string> Params { get { return new List<string> { "{0}", "{2}", "{1}" }; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }
            public static class EmailAddress
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "The {0} field is not a valid e-mail address."; } }
                public static List<string> Params { get { return new List<string> { "{0}" }; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }
            public static class Compare
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "'{0}' and '{1}' do not match."; } }
                public static List<string> Params { get { return new List<string> { "{0}", "{1}" }; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }

            public static class NotEmpty
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "The field {PropertyName} is required."; } }
                public static List<string> Params { get { return new List<string> { "{PropertyName}" }; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }
            public static class Length
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "The field {PropertyName} must be a string with a minimum length of {MinLength} and a maximum length of {MaxLength}."; } }
                public static List<string> Params { get { return new List<string> { "{PropertyName}", "{MinLength}", "{MaxLength}" }; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }
        }
    }
}
