using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Models;
using MeuProjeto.Business.Models.Identity;

namespace MeuProjeto.Business.Interfaces
{
    public interface IUserRepository
    {
        public Task Create(User user, string password);
        public Task UpdateRole(Guid userId, Guid roleId);
        public Task DropRoles(Guid userId);
        public Task AddRoleByUserName(string userName, Guid roleId);
        public Task UpdateClaims(Guid userId, List<Permission> customClaims);
        public Task DropClaims(Guid userId);
        public Task AddClaimsByUserName(string userName, List<Permission> customClaims);
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
        public Task ValidateNewUser(User user);
        public Task ValidateUpdateUser(UpdateUser user);
        public Task ValidateUpdateCurrentUser(UpdateCurrentUser user);
        public void Dispose();
    }
}