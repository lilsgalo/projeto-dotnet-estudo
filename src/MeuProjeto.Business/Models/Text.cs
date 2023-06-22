using System;
using System.Collections.Generic;
using System.Text;

namespace MeuProjeto.Business.Models
{
    public class Text : Entity
    {
        public Text()
        {
            Value = "";
        }
        public string Value { get; set; }

    }
}
