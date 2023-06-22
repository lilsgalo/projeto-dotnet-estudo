using System.Collections.Generic;
using System.Reflection;

namespace MeuProjeto.Business.Resources
{
    public static class UserManualResources
    {
        public static class Id
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "Id"; } }
        }
        public static class Code
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "Code"; } }
        }

        public static class Date
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "Date"; } }
        }
        public static class Content
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "Content"; } }
        }

        public static class Revision
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "Revision"; } }
        }

        public static class ReviewDate
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "Review Date"; } }
        }

        public static class LastReviewer
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "Last Reviewer"; } }
        }

        public static class Errors
        {
            public static class NotFound
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "User Manual not found."; } }
                public static List<string> Params { get { return null; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }
        }
    }
}