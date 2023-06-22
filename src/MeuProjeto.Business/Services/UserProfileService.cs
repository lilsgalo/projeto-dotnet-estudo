using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Enums;
using MeuProjeto.Business.Interfaces;
using MeuProjeto.Business.Models.Identity;
using MeuProjeto.Business.Models.Validations;
using MeuProjeto.Business.Resources;
using MeuProjeto.Business.Extensions;
using MeuProjeto.Business.Models;
using MeuProjeto.Business.Notifications;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MeuProjeto.Business.Services
{
    public class UserProfileService : IdentityBaseService<UserProfile>, IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IPermissionRepository _permissionRepository;
        protected readonly ICustomLogger _logger;

        public UserProfileService(IUserProfileRepository profileRepository,
                                  IPermissionRepository permissionRepository,
                                  ICustomLogger logger,
                                  INotifier notifier) : base(notifier)
        {
            _userProfileRepository = profileRepository;
            _permissionRepository = permissionRepository;
            _logger = logger;
        }

        public async Task Create(UserProfile userProfile)
        {
            if (!RunValidation(new UserProfileValidation(), userProfile)) return;

            var customRole = await _userProfileRepository.FindByName(userProfile.Name);
            if (customRole != null) Notify(UserProfileResources.Errors.NameIsTaken.Formatted);

            if (userProfile.Permissions != null) ValidatePermissions(userProfile.Permissions);

            if (userProfile.Id == Guid.Empty) userProfile.Id = Guid.NewGuid();

            if (ValidOperation()) NotifyErrors(await _userProfileRepository.Create(userProfile));
            if (ValidOperation()) await AddClaimsRole(userProfile.Id, userProfile.Permissions);

            var newValue = await _userProfileRepository.FindByName(userProfile.Name);
            if (ValidOperation()) _logger.RegistrationRecord<CustomRole>(LogTypeEnum.Add, null, newValue);
        }

        public async Task Update(UserProfile userProfile)
        {
            if (!RunValidation(new UserProfileValidation(), userProfile)) return;

            if (userProfile.Admin) Notify(UserProfileResources.Errors.AdminProfile.Formatted);

            var customRole = await _userProfileRepository.FindByName(userProfile.Name);
            if (customRole != null && customRole.Id != userProfile.Id) Notify(UserProfileResources.Errors.NameIsTaken.Formatted);

            if (userProfile.Permissions != null) ValidatePermissions(userProfile.Permissions);

            var oldValue = ManipulationVariables.VariableClone(await _userProfileRepository.FindById(userProfile.Id));

            if (ValidOperation()) NotifyErrors(await _userProfileRepository.Update(userProfile));
            if (ValidOperation()) await UpdadeClaimsRole(userProfile.Id, userProfile.Permissions);

            var newValue = await _userProfileRepository.FindById(userProfile.Id);

            if (ValidOperation()) _logger.RegistrationRecord<CustomRole>(LogTypeEnum.Update, oldValue, newValue);
        }

        public async Task Delete(Guid id)
        {
            var customRole = await _userProfileRepository.GetByIdWithUser(id);

            if (customRole == null) Notify(UserProfileResources.Errors.NotFound.Formatted);
            if (customRole != null && customRole.Admin) Notify(UserProfileResources.Errors.AdminProfile.Formatted);
            if (HaveRelationship(customRole)) Notify(UserProfileResources.Errors.HaveRelationship.Formatted);

            var oldValue = await _userProfileRepository.FindById(id);

            if (ValidOperation())
            {
                customRole.Deleted = true;
                NotifyErrors(await _userProfileRepository.Delete(customRole));
            }

            if (ValidOperation()) _logger.RegistrationRecord<CustomRole>(LogTypeEnum.Delete, oldValue, null);
        }

        public async Task Activate(Guid id)
        {
            var customRole = await _userProfileRepository.FindById(id);

            var oldValue = ManipulationVariables.VariableClone(customRole);

            if (customRole == null) Notify(UserProfileResources.Errors.NotFound.Formatted);

            if (ValidOperation())
            {
                customRole.Active = true;
                NotifyErrors(await _userProfileRepository.Activate(customRole));
            }

            var newValue = await _userProfileRepository.FindById(id);

            if (ValidOperation()) _logger.RegistrationRecord<CustomRole>(LogTypeEnum.Update, oldValue, newValue);
        }

        public async Task Deactivate(Guid id)
        {
            var customRole = await _userProfileRepository.FindById(id);

            var oldValue = ManipulationVariables.VariableClone(customRole);

            if (customRole == null) Notify(UserProfileResources.Errors.NotFound.Formatted);
            if (customRole != null && customRole.Admin) Notify(UserProfileResources.Errors.AdminProfile.Formatted);

            if (ValidOperation())
            {
                customRole.Active = false;
                await _userProfileRepository.Deactivate(customRole);
            }

            var newValue = await _userProfileRepository.FindById(id);

            if (ValidOperation()) _logger.RegistrationRecord<CustomRole>(LogTypeEnum.Update, oldValue, newValue);
        }

        public async Task<UserProfile> GetById(Guid id)
        {
            var userProfile = await _userProfileRepository.GetById(id);

            if (userProfile == null) Notify(UserProfileResources.Errors.NotFound.Formatted);

            return userProfile;
        }

        public async Task<UserProfile> GetByIdAD(Guid id)
        {
            var userProfile = await _userProfileRepository.GetById(id);

            return userProfile;
        }

        public async Task<UserProfile> GetByIdWithClaims(Guid id)
        {
            var userProfile = await _userProfileRepository.GetByIdWithClaims(id);

            if (userProfile == null) Notify(UserProfileResources.Errors.NotFound.Formatted);

            return userProfile;
        }

        public Task<IPagedList<CustomRole>> GetPagedList(FilteredPagedListParameters parameters)
        {
            return _userProfileRepository.GetPagedList(parameters);
        }

        private async Task AddClaimsRole(Guid id, List<Permission> permissions)
        {
            var customRole = await _userProfileRepository.FindById(id);
            var claims = permissions?.Select(c => new Claim(c.Type, c.Value)).ToList();

            foreach (var claim in claims ?? Enumerable.Empty<Claim>()) NotifyErrors(await _userProfileRepository.AddClaim(customRole, claim));
        }

        private async Task UpdadeClaimsRole(Guid id, List<Permission> permissions)
        {
            var customRole = await _userProfileRepository.FindById(id);
            var oldClaims = await _userProfileRepository.GetClaimsByRole(id);
            var newClaims = permissions.Select(c => new Claim(c.Type, c.Value)).ToList();

            foreach (var claim in oldClaims) NotifyErrors(await _userProfileRepository.DeleteClaim(customRole, claim));
            foreach (var claim in newClaims) NotifyErrors(await _userProfileRepository.AddClaim(customRole, claim));
        }

        private void ValidatePermissions(List<Permission> permissions)
        {
            var nonExistentPermissions = permissions.Where(p => !_permissionRepository.PermissionExists(p));

            foreach (var permission in nonExistentPermissions)
            {
                Notify(new Notification(UserProfileResources.Errors.PermissionGroupNotFound.Key,
                                        UserProfileResources.Errors.PermissionGroupNotFound.Value,
                                        new List<string> { permission.Key, permission.GroupKey }));
            }
        }

        private void NotifyErrors(IdentityResult result)
        {
            foreach (var error in result?.Errors)
            {
                Notify(new Notification(error.Description));
            }
        }

        private bool HaveRelationship(CustomRole userProfile)
        {
            if (userProfile != null && userProfile.UserRoles.Any(p => p.Role.Deleted == false)) return true;

            return false;
        }

        public virtual void Dispose()
        {
            _userProfileRepository?.Dispose();
        }
    }
}