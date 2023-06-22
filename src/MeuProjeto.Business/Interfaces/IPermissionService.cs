using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeuProjeto.Business.Models;

namespace MeuProjeto.Business.Interfaces
{
    public interface IPermissionService : IDisposable
    {
        Task<List<Permission>> GetAll();
    }
}