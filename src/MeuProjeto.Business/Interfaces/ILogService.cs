using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Models;

namespace MeuProjeto.Business.Interfaces
{
    public interface ILogService : IDisposable
    {
        Task<Log> GetById(Guid id);
        List<string> GetPagedListTables();
        Task<IPagedList<Log>> GetPagedList(FilteredPagedListParametersLog parameters);
    }
}