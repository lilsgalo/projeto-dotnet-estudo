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
using MeuProjeto.Business.Extensions;

namespace MeuProjeto.Infrastructure.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly MeuDbContext Db;
        private readonly UserManager<CustomUser> _userManager;
        private readonly RoleManager<CustomRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly INotifier _notifier;

        public UserRepository(MeuDbContext db, UserManager<CustomUser> userManager, RoleManager<CustomRole> roleManager, INotifier notifier, IMapper mapper)
        {
            Db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _notifier = notifier;
        }

        public async Task ValidateNewUser(User user)
        {
            var customUser = await _userManager.FindByNameAsync(user.UserName);
            if (customUser != null)
                _notifier.Handle(UserResources.Errors.UserNameIsTaken.Formatted);

            customUser = await _userManager.FindByEmailAsync(user.Email);
            if (customUser != null)
                _notifier.Handle(UserResources.Errors.EmailIsTaken.Formatted);
        }
        public async Task ValidateUpdateUser(UpdateUser user)
        {
            var customUser = await _userManager.FindByEmailAsync(user.Email);
            if (customUser != null && customUser.Id != user.Id)
                _notifier.Handle(UserResources.Errors.EmailIsTaken.Formatted);
        }
        public async Task ValidateUpdateCurrentUser(UpdateCurrentUser user)
        {
            var customUser = await _userManager.FindByEmailAsync(user.Email);
            if (customUser != null && customUser.Id != user.Id)
                _notifier.Handle(UserResources.Errors.EmailIsTaken.Formatted);
        }


        public async Task UpdateRole(Guid userId, Guid roleId)
        {
            await DropRoles(userId);
            await AddRoleByUserId(userId, roleId);
        }

        public async Task DropRoles(Guid userId)
        {
            var customUser = await FindById(userId);
            if (!_notifier.HasNotification())
            {
                var roles = await _userManager.GetRolesAsync(customUser);
                NotifyErrors(await _userManager.RemoveFromRolesAsync(customUser, roles.ToArray()));
            }
        }

        public async Task AddRole(CustomUser customUser, Guid roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (customUser != null && role != null)
                NotifyErrors(await _userManager.AddToRoleAsync(customUser, role.Name));
        }

        public async Task AddRoleByUserId(Guid userId, Guid roleId)
        {
            var customUser = await FindById(userId);
            if (!_notifier.HasNotification())
                await AddRole(customUser, roleId);
        }
        public async Task AddRoleByUserName(string userName, Guid roleId)
        {
            var customUser = await FindByName(userName);
            if (!_notifier.HasNotification())
            {
                await AddRole(customUser, roleId);
            }
        }

        public async Task UpdateClaims(Guid userId, List<Permission> customClaims)
        {
            await DropClaims(userId);
            await AddClaimsByUserId(userId, customClaims);
        }
        public async Task DropClaims(Guid userId)
        {
            var customUser = await FindById(userId);
            if (!_notifier.HasNotification())
            {
                var claims = await _userManager.GetClaimsAsync(customUser);
                NotifyErrors(await _userManager.RemoveClaimsAsync(customUser, claims.ToArray()));
            }
        }
        public async Task AddClaims(CustomUser customUser, List<Permission> customClaims)
        {
            var claims = customClaims?.Select(c => new Claim(c.Type, c.Value));
            if (customUser != null && claims != null)
                NotifyErrors(await _userManager.AddClaimsAsync(customUser, claims));
        }
        public async Task AddClaimsByUserId(Guid userId, List<Permission> customClaims)
        {
            var customUser = await FindById(userId);
            if (!_notifier.HasNotification())
                await AddClaims(customUser, customClaims);
        }
        public async Task AddClaimsByUserName(string userName, List<Permission> customClaims)
        {
            var customUser = await FindByName(userName);
            if (!_notifier.HasNotification())
                await AddClaims(customUser, customClaims);
        }

        public async Task Create(User user, string password)
        {
            var customUser = _mapper.Map<CustomUser>(user);
            await this.Create(customUser, password);
        }

        private async Task Create(CustomUser customUser, string password)
        {
            if (customUser.Id == Guid.Empty)
            {
                customUser.Id = Guid.NewGuid();
            }
            customUser.Deleted = false;
            NotifyErrors(await _userManager.CreateAsync(customUser, password));
        }

        public async Task Update(UpdateUser user)
        {
            var customUserDB = await _userManager.Users.Include(u => u.Image).Include(u => u.Icon).FirstOrDefaultAsync(u => u.Id == user.Id);
            if (!_notifier.HasNotification())
            {
                _mapper.Map<UpdateUser, CustomUser>(user, customUserDB);
                NotifyErrors(await _userManager.UpdateAsync(customUserDB));
            }
        }

        public async Task UpdateCurrentUser(UpdateCurrentUser user)
        {
            var customUserDB = await _userManager.Users.Include(u => u.Image).Include(u => u.Icon).FirstOrDefaultAsync(u => u.Id == user.Id);
            if (!_notifier.HasNotification())
            {
                Picture image = customUserDB.Image, icon = customUserDB.Icon;
                //Só atualiza a imagem se for enviada uma nova.
                if (!String.IsNullOrEmpty(user.Image))
                {
                    image.Value = user.Image;
                    icon.Value = user.Icon;
                }
                var customUser = _mapper.Map<UpdateCurrentUser, CustomUser>(user, customUserDB);
                customUser.Image = image;
                customUser.Icon = icon;
                customUser.ImageId = image.Id;
                customUser.IconId = icon.Id;
                NotifyErrors(await _userManager.UpdateAsync(customUser));
            }
        }

        public async Task Delete(Guid userId)
        {
            var customUser = await FindById(userId);
            if (!_notifier.HasNotification())
            {
                customUser.Deleted = true;
                customUser.UserName = Guid.NewGuid() + "__" + customUser.UserName;
                customUser.Email = Guid.NewGuid() + "__" + customUser.Email;

                NotifyErrors(await _userManager.UpdateAsync(customUser));
            }
        }

        public async Task Activate(Guid userId)
        {
            var customUser = await FindById(userId);
            if (!_notifier.HasNotification())
            {
                customUser.Active = true;

                NotifyErrors(await _userManager.UpdateAsync(customUser));
            }
        }

        public async Task Deactivate(Guid userId)
        {
            var customUser = await FindById(userId);
            if (!_notifier.HasNotification())
            {
                customUser.Active = false;

                NotifyErrors(await _userManager.UpdateAsync(customUser));
            }
        }

        public async Task ChangeUserPassword(Guid userId, string newPassword)
        {
            var customUser = await FindById(userId);
            if (!_notifier.HasNotification())
            {
                NotifyErrors(await _userManager.RemovePasswordAsync(customUser));
                NotifyErrors(await _userManager.AddPasswordAsync(customUser, newPassword));
            }
        }

        public async Task ChangeCurrentUserPassword(Guid userId, string password, string newPassword)
        {
            var customUser = await FindById(userId);
            if (!_notifier.HasNotification())
                NotifyErrors(await _userManager.ChangePasswordAsync(customUser, password, newPassword));
        }

        public async Task<string> GeneratePasswordResetToken(CustomUser customUser)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(customUser);
        }
        public async Task ResetPassword(CustomUser customUser, string token, string newPassword)
        {
            NotifyErrors(await _userManager.ResetPasswordAsync(customUser, token, newPassword));
        }

        public async Task<List<Claim>> GetClaims(CustomUser user)
        {
            var claims = new List<Claim>();

            var claimsIdentity = new ClaimsIdentityOptions();
            claims.Add(new Claim(claimsIdentity.UserIdClaimType, user.Id.ToString()));
            claims.Add(new Claim(claimsIdentity.UserNameClaimType, user.UserName));
            claims.Add(new Claim(claimsIdentity.SecurityStampClaimType, user.SecurityStamp));
            claims.Add(new Claim(claimsIdentity.RoleClaimType, user.UserRoles.Select(r => r.Role.Name).FirstOrDefault()));

            claims.AddRange(await _userManager.GetClaimsAsync(user));

            var roles = user.UserRoles
                .Where(r => r.Role.Active && !r.Role.Deleted)
                .Select(userRole => userRole.Role).ToList();
            foreach (var role in roles)
            {
                claims.AddRange(await _roleManager.GetClaimsAsync(role));
            }

            return claims.DistinctBy(c => new { c.Type, c.Value }).ToList();
        }

        public async Task<User> GetById(Guid id)
        {
            var customUser = await FindById(id);

            var user = _mapper.Map<User>(customUser);
            return user;
        }

        public async Task<CustomUser> GetByUserName(string userName)
        {
            var customUser = await _userManager.Users.Include(i => i.UserRoles).FirstAsync(u => u.UserName == userName);
            //if (customUser == null)
            //    _notifier.Handle(UserResources.Errors.NotFound.Formatted);
            return customUser;
        }

        private async Task<CustomUser> FindById(Guid id)
        {
            var customUser = await _userManager.FindByIdAsync(id.ToString());
            if (customUser == null)
                _notifier.Handle(UserResources.Errors.NotFound.Formatted);
            return customUser;
        }
        private async Task<CustomUser> FindByName(string userName)
        {
            var customUser = await _userManager.FindByNameAsync(userName);
            if (customUser == null)
                _notifier.Handle(UserResources.Errors.NotFound.Formatted);
            return customUser;
        }

        public async Task<User> GetByIdWithRoleClaims(Guid id)
        {
            var customUser = await _userManager.Users.Include("UserRoles.Role").Include(u => u.Claims).FirstOrDefaultAsync(u => u.Id == id);

            var user = _mapper.Map<User>(customUser);
            return user;
        }

        public async Task<User> GetByIdCurrentUser(Guid id)
        {
            var customUser = await _userManager.Users.Include("UserRoles.Role").FirstOrDefaultAsync(u => u.Id == id);

            var user = _mapper.Map<User>(customUser);
            return user;
        }

        public async Task<User> GetByIdForUpdate(Guid id)
        {
            var customUser = await _userManager.Users.Include("UserRoles.Role").Include(u => u.Image).Include(u => u.Claims).AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            var user = _mapper.Map<User>(customUser);
            return user;
        }

        public async Task<IPagedList<User>> GetPagedList(UserFilteredPagedListParameters parameters)
        {
            var dadosFiltrados = _userManager.Users.Where(p =>
            (
                (p.Deleted == false) &&
                (
                    parameters.Search == null ||
                    p.Name.Contains(parameters.Search) ||
                    p.Email.Contains(parameters.Search) ||
                    p.UserName.Contains(parameters.Search) ||
                    p.PhoneNumber.Contains(parameters.Search)
                )
                &&
                (parameters.ExcludedIds == null || !parameters.ExcludedIds.Any(i => i == p.Id))

            ));
            dadosFiltrados = dadosFiltrados.OrderBy(parameters.Sort);

            var data = await PagedList<CustomUser>.ToPagedList(dadosFiltrados, parameters.CurrentPage, parameters.PageSize);
            return _mapper.Map<IPagedList<User>>(data);
        }
        public async Task<IPagedList<User>> GetSimplePagedList(UserFilteredPagedListParameters parameters)
        {
            var dadosFiltrados = _userManager.Users.Where(p =>
            (
                (parameters.Search == null || p.Name.Contains(parameters.Search)) &&
                (parameters.ExcludedIds == null || !parameters.ExcludedIds.Any(i => i == p.Id)) &&
                (p.Deleted == false)
            ));

            dadosFiltrados = dadosFiltrados.OrderBy(parameters.Sort);

            var data = await PagedList<CustomUser>.ToPagedList(dadosFiltrados, parameters.CurrentPage, parameters.PageSize);
            return _mapper.Map<IPagedList<User>>(data);
        }
        public async Task<IPagedList<User>> GetResponsibleSimplePagedList(UserFilteredPagedListParameters parameters)
        {
            var dadosFiltrados = _userManager.Users.Where(p =>
            (
                (parameters.Search == null || p.Name.Contains(parameters.Search)) &&
                (parameters.ExcludedIds == null || !parameters.ExcludedIds.Any(i => i == p.Id)) &&
                (p.Deleted == false) &&
                (p.Claims.Any(o => o.ClaimType == "projectproposal" && o.ClaimValue == "create") ||
                p.UserRoles.Any(u => u.Role.Claims.Any(o => o.ClaimType == "projectproposal" && o.ClaimValue == "create")))
            ));

            dadosFiltrados = dadosFiltrados.OrderBy(parameters.Sort);

            var data = await PagedList<CustomUser>.ToPagedList(dadosFiltrados, parameters.CurrentPage, parameters.PageSize);
            return _mapper.Map<IPagedList<User>>(data);
        }

        private void NotifyErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                _notifier.Handle(error.Description);
            }
        }

        public void Dispose()
        {
            Db?.Dispose();
        }

    }
}