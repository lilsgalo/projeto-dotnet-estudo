using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Models;

namespace MeuProjeto.Business.Interfaces
{
    public interface ILogRepository : IRepository<Log>
    {
        List<string> GetPagedListTables();
    }
}