using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Models;

namespace MeuProjeto.Business.Interfaces
{
    public interface IUserManualService : IDisposable
    {
        public Task Update(UserManual userManual);
        public Task<UserManual> GetById(Guid userManualId);
        public Task<UserManual> GetByCode(string userManualCode);
        public Task<IPagedList<UserManual>> GetPagedList(FilteredPagedListParameters parameters);
    }
}