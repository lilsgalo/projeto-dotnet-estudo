using System.Collections.Generic;
using System.Reflection;

namespace MeuProjeto.Business.Resources
{
    public static class SettingsResources
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

        public static class Contents
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "Value"; } }
        }

        public static class Errors
        {
            public static class NotFound
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Settings not found."; } }
                public static List<string> Params { get { return null; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }

            public static class SomeNotFound
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Some settings were not found."; } }
                public static List<string> Params { get { return null; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }
        }
    }
}