using MeuProjeto.Business.Enums;
using MeuProjeto.Business.Interfaces;
using MeuProjeto.Business.Resources;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MeuProjeto.Api.Extensions
{
    public class CustomLogger : ICustomLogger
    {
        private readonly ILogger<CustomLogger> _logger;

        public CustomLogger(ILogger<CustomLogger> logger)
        {
            _logger = logger;
        }

        public void RegistrationRecord<TypeTable>(LogTypeEnum type, object oldState, object newState)
        {
            var message = "";

            switch (type)
            {
                case LogTypeEnum.Add:
                    message = LogResources.Messages.Added.Formatted;
                    break;
                case LogTypeEnum.Update:
                    message = LogResources.Messages.Update.Formatted;
                    break;
                case LogTypeEnum.Delete:
                    message = LogResources.Messages.Deleted.Formatted;
                    break;
            }

            Information(message, type, typeof(TypeTable).Name.ToLower(), JsonConvert.SerializeObject(oldState), JsonConvert.SerializeObject(newState));
        }

        public void Information(string message, LogTypeEnum type, string tableName = "", string oldState = "", string newState = "")
        {
            _logger.LogInformation("[{message}] [{type}] [{tableName}] [{oldState}] [{newState}]", message, (int)type, tableName, oldState, newState);
        }

        public void Warning(string message, LogTypeEnum type, string tableName = "", string oldState = "", string newState = "")
        {
            _logger.LogWarning("[{message}] [{type}] [{tableName}] [{oldState}] [{newState}]", message, (int)type, tableName, oldState, newState);
        }

        public void Error(string message, LogTypeEnum type, string tableName = "", string oldState = "", string newState = "")
        {
            _logger.LogError("[{message}] [{type}] [{tableName}] [{oldState}] [{newState}]", message, (int)type, tableName, oldState, newState);
        }

        public void Critical(string message, LogTypeEnum type, string tableName = "", string oldState = "", string newState = "")
        {
            _logger.LogCritical("[{message}] [{type}] [{tableName}] [{oldState}] [{newState}]", message, (int)type, tableName, oldState, newState);
        }
    }
}