using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Models;
using MeuProjeto.Business.Models.Identity;

namespace MeuProjeto.Business.Interfaces
{
    public interface IUserProfileService : IDisposable
    {
        public Task Create(UserProfile userProfile);
        public Task Update(UserProfile userProfile);
        public Task Delete(Guid userProfileId);
        public Task Activate(Guid userProfileId);
        public Task Deactivate(Guid userProfileId);
        public Task<UserProfile> GetById(Guid id);
        public Task<UserProfile> GetByIdAD(Guid id);
        public Task<UserProfile> GetByIdWithClaims(Guid id);
        public Task<IPagedList<CustomRole>> GetPagedList(FilteredPagedListParameters parameters);
    }
}