using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Enums;
using MeuProjeto.Business.Models;
using MeuProjeto.Business.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace MeuProjeto.Business.Interfaces
{
    public interface IUserProfileRepository
    {
        public Task<IdentityResult> Create(UserProfile userProfile);
        public Task<IdentityResult> Update(UserProfile userProfile);
        public Task<IdentityResult> Delete(CustomRole customRole);
        public Task<IdentityResult> Activate(CustomRole customRole);
        public Task<IdentityResult> Deactivate(CustomRole customRole);
        public Task<UserProfile> GetById(Guid id);
        public Task<UserProfile> GetByIdWithClaims(Guid id);
        public Task<CustomRole> GetByIdWithUser(Guid id);
        public Task<CustomRole> FindById(Guid id);
        public Task<CustomRole> FindByName(string name);
        public Task<IPagedList<CustomRole>> GetPagedList(FilteredPagedListParameters parameters);
        public Task<IdentityResult> AddClaim(CustomRole customRole, Claim claim);
        public Task<IdentityResult> DeleteClaim(CustomRole customRole, Claim claim);
        public Task<IList<Claim>> GetClaimsByRole(Guid id);
        public void Dispose();
    }
}