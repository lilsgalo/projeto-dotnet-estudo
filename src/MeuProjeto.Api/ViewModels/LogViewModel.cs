using MeuProjeto.Business.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace MeuProjeto.Api.ViewModels
{
    public class LogViewModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string TableName { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public LogTypeEnum Type { get; set; }

        public string IP { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Header { get; set; }
        public string Exception { get; set; }
        public string StackTracking { get; set; }

        public dynamic OldState { get; set; }
        public dynamic NewState { get; set; }
    }

    public class LogListViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string TableName { get; set; }
        public DateTime Date { get; set; }
        public LogTypeEnum Type { get; set; }
        public string Level { get; set; }
    }
}