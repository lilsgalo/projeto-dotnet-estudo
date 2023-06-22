using System.Collections.Generic;
using System.Reflection;

namespace MeuProjeto.Business.Resources
{
    public static class LogResources
    {
        public static class Id
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "Id"; } }
        }
        public static class User
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "User"; } }
        }

        public static class Date
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "Date"; } }
        }
        public static class Type
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "Type"; } }
        }

        public static class Logger
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "Logger"; } }
        }

        public static class Headers
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "Headers"; } }
        }

        public static class IP
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "IP"; } }
        }
        public static class Exception
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "Exception"; } }
        }
        public static class StackTrace
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "StackTrace"; } }
        }
        public static class TableName
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "TableName"; } }
        }
        public static class OldValues
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "OldValues"; } }
        }
        public static class NewValues
        {
            public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
            public static string Value { get { return "NewValues"; } }
        }

        public static class Messages
        {
            public static class Added
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Added."; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value); } }
            }
            public static class Deleted
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Deleted."; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value); } }
            }
            public static class Update
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Update."; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value); } }
            }
        }

        public static class Errors
        {
            public static class NotFound
            {
                public static string Key { get { return ResourcesUtils.GetKey(MethodInfo.GetCurrentMethod().DeclaringType); } }
                public static string Value { get { return "Log not found."; } }
                public static List<string> Params { get { return null; } }
                public static string Formatted { get { return ResourcesUtils.Format(Key, Value, Params); } }
            }
        }
    }
}