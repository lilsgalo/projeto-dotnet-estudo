using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Models;
using MeuProjeto.Business.Models.Identity;

namespace MeuProjeto.Business.Interfaces
{
    public interface IUserService : IDisposable
    {
        public Task Create(User user, string password);
        public Task CreateMany(List<DefaultUser> users);
        public Task Update(UpdateUser user);
        public Task UpdateCurrentUser(UpdateCurrentUser user);
        public Task Delete(Guid userId);
        public Task Activate(Guid userId);
        public Task Deactivate(Guid userId);
        public Task ChangeUserPassword(Guid userId, string newPassword);
        public Task ChangeCurrentUserPassword(Guid userId, string password, string newPassword);
        public Task<List<Claim>> GetClaims(CustomUser user);
        public Task<User> GetById(Guid id);
        public Task<CustomUser> GetByUserName(string userName);
        public Task<User> GetByIdWithRoleClaims(Guid id);
        public Task<User> GetByIdCurrentUser(Guid id);
        public Task<User> GetByIdForUpdate(Guid id);
        public Task<IPagedList<User>> GetPagedList(UserFilteredPagedListParameters parameters);

        public Task<IPagedList<User>> GetSimplePagedList(UserFilteredPagedListParameters parameters);
        public Task<IPagedList<User>> GetResponsibleSimplePagedList(UserFilteredPagedListParameters parameters);
        public Task<bool> CanLogin(string userName);
    }
}