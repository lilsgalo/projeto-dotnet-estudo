using MeuProjeto.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuProjeto.Business.Models
{
    public class Settings : Entity
    {
        public SettingsTypeEnum Type { get; set; }
        public string Code { get; set; }
        public string Contents { get; set; }
    }
}
