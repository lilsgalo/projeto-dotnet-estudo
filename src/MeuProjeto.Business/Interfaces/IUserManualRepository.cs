using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Models;

namespace MeuProjeto.Business.Interfaces
{
    public interface IUserManualRepository : IRepository<UserManual>
    {
        public Task<IPagedList<UserManual>> GetPagedList(FilteredPagedListParameters parameters);
        public Task<UserManual> GetByCode(string userManualCode);
    }
}