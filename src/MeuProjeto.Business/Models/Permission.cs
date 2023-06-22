using System;
using System.Collections.Generic;
using System.Text;

namespace MeuProjeto.Business.Models
{
    public class Permission : Entity
    {
        public Permission() : base()
        {
        }
        public Permission(string type, string value) : base()
        {
            Type = type;
            Value = value;
        }
        public string Type { get; set; }
        public string Value { get; set; }
        public string GroupKey
        {
            get
            {
                return Type.ToLower() + ".permissiongroup";
            }
        }
        public string Key
        {
            get
            {
                return Type.ToLower() + ".permissions." + Value.ToLower();
            }
        }
    }
}
