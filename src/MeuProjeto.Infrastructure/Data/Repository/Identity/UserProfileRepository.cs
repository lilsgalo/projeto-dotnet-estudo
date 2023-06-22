using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Interfaces;
using MeuProjeto.Business.Models;
using MeuProjeto.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using MeuProjeto.Business.Notifications;
using MeuProjeto.Business.Resources;
using MeuProjeto.Business.Models.Identity;
using System.Globalization;
using MeuProjeto.Business.Enums;
using MeuProjeto.Business.Extensions;

namespace MeuProjeto.Infrastructure.Data.Repository
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly RoleManager<CustomRole> _roleManager;
        private readonly IMapper _mapper;

        public UserProfileRepository(RoleManager<CustomRole> roleManager,
                                     IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> Create(UserProfile userProfile)
        {
            return await _roleManager.CreateAsync(_mapper.Map<CustomRole>(userProfile));
        }

        public async Task<IdentityResult> Update(UserProfile userProfile)
        {
            var customRole = await FindById(userProfile.Id);
            _mapper.Map(userProfile, customRole);

            return await _roleManager.UpdateAsync(customRole);
        }

        public async Task<IdentityResult> Delete(CustomRole customRole)
        {
            return await _roleManager.UpdateAsync(customRole);
        }

        public async Task<IdentityResult> Activate(CustomRole customRole)
        {
            return await _roleManager.UpdateAsync(customRole);
        }

        public async Task<IdentityResult> Deactivate(CustomRole customRole)
        {
            return await _roleManager.UpdateAsync(customRole);
        }

        public async Task<UserProfile> GetById(Guid id)
        {
            var customRole = await FindById(id);

            return _mapper.Map<UserProfile>(customRole);
        }

        public async Task<UserProfile> GetByIdWithClaims(Guid id)
        {
            var customUser = await _roleManager.Roles.Include(u => u.Claims).FirstOrDefaultAsync(u => u.Id == id);

            return _mapper.Map<UserProfile>(customUser);
        }

        public async Task<CustomRole> GetByIdWithUser(Guid id)
        {
            return await _roleManager.Roles.Include("UserRoles.User").FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<CustomRole> FindById(Guid id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }

        public async Task<CustomRole> FindByName(string name)
        {
            return await _roleManager.FindByNameAsync(name);
        }

        public async Task<IPagedList<CustomRole>> GetPagedList(FilteredPagedListParameters parameters)
        {
            var dadosFiltrados = _roleManager.Roles.Include("UserRoles.User").Where(p =>
                (parameters.Search == null || EF.Functions.Collate(p.Name, "Latin1_General_CI_AI").Contains(parameters.Search)) &&
                (p.Deleted == false)
            );

            dadosFiltrados = dadosFiltrados.OrderBy(parameters.Sort);

            return await PagedList<CustomRole>.ToPagedList(dadosFiltrados, parameters.CurrentPage, parameters.PageSize);
        }

        public async Task<IdentityResult> AddClaim(CustomRole customRole, Claim claim)
        {
            return await _roleManager.AddClaimAsync(customRole, claim);
        }

        public async Task<IdentityResult> DeleteClaim(CustomRole customRole, Claim claim)
        {
            return await _roleManager.RemoveClaimAsync(customRole, claim);
        }

        public async Task<IList<Claim>> GetClaimsByRole(Guid id)
        {
            var customRole = await FindById(id);
            return await _roleManager.GetClaimsAsync(customRole);
        }

        public void Dispose() { }
    }
}