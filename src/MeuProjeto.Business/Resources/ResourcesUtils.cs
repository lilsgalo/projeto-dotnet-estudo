using System;
using System.Collections.Generic;

namespace MeuProjeto.Business.Resources
{
    public static class ResourcesUtils
    {
        public const string Separator = "|";
        public const string ParamsSeparator = ";";
        public static string Format(string key, string value, List<string> parameters = null)
        {
            var r = key + Separator + value;
            if(parameters != null)
            {
                r += Separator;
                for (int i = 0; i < parameters.Count; i++)
                {
                    r += parameters[i] + (i == parameters.Count - 1 ? "" : ParamsSeparator);
                }
            }
            return r;
        }
        public static string GetKey(Type type)
        {
            return type.FullName.Substring(type.Namespace.Length + 1).Replace("+", ".").ToLower().Replace("resources.", ".");            
        }
    }
}