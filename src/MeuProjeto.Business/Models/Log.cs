using System;
using System.Collections.Generic;
using System.Text;
using MeuProjeto.Business.Enums;
using MeuProjeto.Business.Models.Identity;

namespace MeuProjeto.Business.Models
{
    public class Log : Entity
    {
        public DateTime Date { get; set; }
        public LogTypeEnum Type { get; set; }

        public string IP { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Header { get; set; }
        public string Exception { get; set; }
        public string StackTracking { get; set; }

        public string TableName { get; set; }
        public string Message { get; set; }
        public string OldState { get; set; }
        public string NewState { get; set; }

        //Entity Framework
        public Guid? UserId { get; set; }
        public CustomUser User { get; set; }
    }
}
