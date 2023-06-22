using System.Collections.Generic;
using System.Reflection;

namespace MeuProjeto.Business.Resources
{
    public static class UserProfileResources
    {
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
        public static class Errors
        {
            public static class NameIsTaken
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "That name is taken. Try another."; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value); } }
            }
            public static class NotFound
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Profile not found."; } }
                public static List<string> Params { get { return null; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }

            public static class HaveRelationship
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Profile has relationship."; } }
                public static List<string> Params { get { return null; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }
            public static class PermissionGroupNotFound
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "The {0} permission of the {1} group was not found."; } }
                public static List<string> Params { get { return new List<string> { "{0}", "{1}" }; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }
            public static class AdminProfile
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Profile cannot be modified."; } }
                public static List<string> Params { get { return null; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }

        }
    }
}