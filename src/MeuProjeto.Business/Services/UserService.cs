using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Enums;
using MeuProjeto.Business.Extensions;
using MeuProjeto.Business.Interfaces;
using MeuProjeto.Business.Models;
using MeuProjeto.Business.Models.Identity;
using MeuProjeto.Business.Models.Validations;
using MeuProjeto.Business.Notifications;
using MeuProjeto.Business.Resources;
using Newtonsoft.Json;

namespace MeuProjeto.Business.Services
{
    public class UserService : IdentityBaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;
        protected readonly ICustomLogger _logger;

        public UserService(IUserRepository userRepository,
                           IPermissionRepository permissionRepository,
                           IMapper mapper,
                           ICustomLogger logger,
                           INotifier notifier) : base(notifier)
        {
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Create(User user, string password)
        {
            if (!RunValidation(new UserValidation(), user)) return;
            await _userRepository.ValidateNewUser(user);

            if (user.Permissions != null) ValidatePermissions(user.Permissions);
            if (ValidOperation()) await _userRepository.Create(user, password);
            if (ValidOperation()) await _userRepository.AddRoleByUserName(user.UserName, user.ProfileId);
            if (ValidOperation()) await _userRepository.AddClaimsByUserName(user.UserName, user.Permissions);

            var newValue = ManipulationVariables.VariableClone(await _userRepository.GetByUserName(user.UserName));
            if (ValidOperation()) _logger.RegistrationRecord<CustomUser>(LogTypeEnum.Add, null, newValue);
        }

        public async Task CreateMany(List<DefaultUser> users)
        {
            foreach (var defaultUser in users)
            {
                var user = _mapper.Map<User>(defaultUser);
                await Create(user, defaultUser.Password);
            }
        }

        public async Task Update(UpdateUser user)
        {
            if (!RunValidation(new UpdateUserValidation(), user)) return;
            await _userRepository.ValidateUpdateUser(user);

            if (user.Permissions != null) ValidatePermissions(user.Permissions);

            var oldValue = ManipulationVariables.VariableClone(await _userRepository.GetById(user.Id));

            if (ValidOperation()) await _userRepository.Update(user);
            if (ValidOperation()) await _userRepository.UpdateRole(user.Id, user.ProfileId);
            if (ValidOperation()) await _userRepository.UpdateClaims(user.Id, user.Permissions);

            var newValue = await _userRepository.GetById(user.Id);

            if (ValidOperation()) _logger.RegistrationRecord<CustomUser>(LogTypeEnum.Update, oldValue, newValue);
        }

        public async Task UpdateCurrentUser(UpdateCurrentUser user)
        {
            if (!RunValidation(new UpdateCurrentUserValidation(), user)) return;
            await _userRepository.ValidateUpdateCurrentUser(user);
            if (ValidOperation()) await _userRepository.UpdateCurrentUser(user);
        }

        public async Task Activate(Guid userId)
        {
            var oldValue = ManipulationVariables.VariableClone(await _userRepository.GetById(userId));

            await _userRepository.Activate(userId);

            var newValue = await _userRepository.GetById(userId);

            if (ValidOperation()) _logger.RegistrationRecord<CustomUser>(LogTypeEnum.Update, oldValue, newValue);
        }

        public async Task Deactivate(Guid userId)
        {
            var oldValue = ManipulationVariables.VariableClone(await _userRepository.GetById(userId));

            await _userRepository.Deactivate(userId);

            var newValue = await _userRepository.GetById(userId);

            if (ValidOperation()) _logger.RegistrationRecord<CustomUser>(LogTypeEnum.Update, oldValue, newValue);
        }


        public async Task Delete(Guid userId)
        {
            var oldValue = await _userRepository.GetById(userId);

            await _userRepository.Delete(userId);

            if (ValidOperation()) _logger.RegistrationRecord<CustomUser>(LogTypeEnum.Delete, oldValue, null);
        }

        public async Task ChangeUserPassword(Guid userId, string newPassword)
        {
            await _userRepository.ChangeUserPassword(userId, newPassword);
        }

        public async Task ChangeCurrentUserPassword(Guid userId, string password, string newPassword)
        {
            await _userRepository.ChangeCurrentUserPassword(userId, password, newPassword);
        }

        public async Task<List<Claim>> GetClaims(CustomUser user)
        {
            return await _userRepository.GetClaims(user);
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _userRepository.GetById(id);

            if (user == null) Notify(UserResources.Errors.NotFound.Formatted);

            return user;
        }

        public async Task<CustomUser> GetByUserName(string userName)
        {
            var user = ManipulationVariables.VariableClone(await _userRepository.GetByUserName(userName));

            if (user == null) Notify(UserResources.Errors.NotFound.Formatted);

            return user;
        }

        public async Task<User> GetByIdWithRoleClaims(Guid id)
        {
            var user = await _userRepository.GetByIdWithRoleClaims(id);

            if (user == null) Notify(UserResources.Errors.NotFound.Formatted);

            return user;
        }

        public async Task<User> GetByIdCurrentUser(Guid id)
        {
            var user = await _userRepository.GetByIdCurrentUser(id);

            if (user == null) Notify(UserResources.Errors.NotFound.Formatted);

            return user;
        }

        public async Task<User> GetByIdForUpdate(Guid id)
        {
            var user = await _userRepository.GetByIdForUpdate(id);

            if (user == null) Notify(UserResources.Errors.NotFound.Formatted);

            return user;
        }

        public Task<IPagedList<User>> GetPagedList(UserFilteredPagedListParameters parameters)
        {
            return _userRepository.GetPagedList(parameters);
        }
        public Task<IPagedList<User>> GetSimplePagedList(UserFilteredPagedListParameters parameters)
        {
            return _userRepository.GetSimplePagedList(parameters);
        }
        public Task<IPagedList<User>> GetResponsibleSimplePagedList(UserFilteredPagedListParameters parameters)
        {
            return _userRepository.GetResponsibleSimplePagedList(parameters);
        }
        public async Task<bool> CanLogin(string userName)
        {
            var customUser = await _userRepository.GetByUserName(userName);

            if (customUser == null || customUser.Deleted || !customUser.Active)
                return false;

            return true;
        }

        private void ValidatePermissions(List<Permission> permissions)
        {
            var nonExistentPermissions = permissions.Where(p => !_permissionRepository.PermissionExists(p));

            foreach (var permission in nonExistentPermissions)
            {
                _notifier.Handle(new Notification(UserProfileResources.Errors.PermissionGroupNotFound.Key,
                                                  UserProfileResources.Errors.PermissionGroupNotFound.Value,
                                                  new List<string> { permission.Key, permission.GroupKey }));
            }
        }

        public virtual void Dispose()
        {
            _userRepository?.Dispose();
        }
    }
}