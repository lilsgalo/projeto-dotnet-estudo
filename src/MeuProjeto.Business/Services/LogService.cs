using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Extensions;
using MeuProjeto.Business.Interfaces;
using MeuProjeto.Business.Interfaces;
using MeuProjeto.Business.Models;
using MeuProjeto.Business.Resources;
using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Business.Services
{
    public class LogService : BaseService<Log>, ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(INotifier notificador,
                          ILogRepository logRepository) : base(notificador, logRepository)
        {
            _logRepository = logRepository;
        }

        public Task<Log> GetById(Guid id)
        {
            var log = _logRepository.GetById(id, null, p => p.User);

            if (log == null) Notify(LogResources.Errors.NotFound.Formatted);

            return log;
        }

        public List<string> GetPagedListTables()
        {
            return _logRepository.GetPagedListTables();
        }

        public Task<IPagedList<Log>> GetPagedList(FilteredPagedListParametersLog parameters)
        {
            return _logRepository.GetPagedList(entity =>
            (
                (parameters.FilterType == null || parameters.FilterType == entity.Type) &&
                (parameters.TableName == null || EF.Functions.Collate(entity.TableName, "Latin1_General_CI_AI").Contains(parameters.TableName))
            ), parameters, null, p => p.User);
        }

        public override void Dispose()
        {
            _logRepository?.Dispose();
        }
    }
}