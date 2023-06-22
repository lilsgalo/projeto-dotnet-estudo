using MeuProjeto.Business.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace MeuProjeto.Business.Notifications
{
    public class Notification
    {
        public Notification()
        {
        }

        public Notification(string message)
        {
            var itens = message.Split(ResourcesUtils.Separator);
            Key = itens.Length >= 1 ? itens[0] : null;
            Value = itens.Length >= 2 ? itens[1] : null;
            Parameters = itens.Length == 3 ? itens[2]?.Split(ResourcesUtils.ParamsSeparator).ToList() : null;
        }

        public Notification(string key, string value, List<string> _params = null)
        {
            Key = key;
            Value = value;
            Parameters = _params;
        }

        public string Key { get; set; }
        public string Value { get; }
        private List<string> Parameters { get; set; }
        public object Params
        {
            get
            {
                if (Parameters != null)
                {
                    try
                    {
                        var json = String.Join(",", Parameters.Select((p, i) => String.Format("\"{0}\":\"{1}\"", i, p)));
                        return JsonSerializer.Deserialize<object>(String.Format("{{ {0} }}", json));
                    }
                    catch (Exception e)
                    {

                    }
                }

                return null;
            }
        }
    }
}