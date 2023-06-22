using System.Collections.Generic;
using MeuProjeto.Business.Enums;
using MeuProjeto.Business.Notifications;

namespace MeuProjeto.Business.Interfaces
{
    public interface ICustomLogger
    {
        public void RegistrationRecord<Type>(LogTypeEnum action, object oldState, object newState);
        public void Information(string message, LogTypeEnum action, string table = "", string oldState = "", string newState = "");
        public void Warning(string message, LogTypeEnum action, string table = "", string oldState = "", string newState = "");
        public void Error(string message, LogTypeEnum action, string table = "", string oldState = "", string newState = "");
        public void Critical(string message, LogTypeEnum action, string table = "", string oldState = "", string newState = "");
    }
}