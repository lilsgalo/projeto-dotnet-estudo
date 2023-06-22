using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Interfaces;
using MeuProjeto.Business.Models;
using MeuProjeto.Business.Models.Validations;
using MeuProjeto.Business.Resources;

namespace MeuProjeto.Business.Services
{
    public class UserManualService : BaseService<UserManual>, IUserManualService
    {
        private readonly IUserManualRepository _UserManualRepository;
        private readonly IUser _user;

        public UserManualService(IUserManualRepository UserManualRepository,
                              INotifier notificador,
                              IUser user) : base(notificador, UserManualRepository)
        {
            _UserManualRepository = UserManualRepository;
            _user = user;
        }

        public async Task Update(UserManual userManual)
        {
            if (!RunValidation(new UserManualValidation(), userManual)) return;

            userManual.ReviewDate = DateTime.Now;
            userManual.Revision++;

            await _UserManualRepository.Update(userManual);
        }

        public async Task<UserManual> GetById(Guid id)
        {
            var userManual = await _UserManualRepository.GetById(id, null, i => i.LastReviewer, i => i.Content);

            if (userManual == null) Notify(UserManualResources.Errors.NotFound.Formatted);

            return userManual;
        }

        public async Task<UserManual> GetByCode(string code)
        {
            var userManual = await _UserManualRepository.GetByCode(code);

            if (userManual == null) Notify(UserManualResources.Errors.NotFound.Formatted);

            return userManual;
        }

        public Task<IPagedList<UserManual>> GetPagedList(FilteredPagedListParameters parameters)
        {
            return _UserManualRepository.GetPagedList(parameters);
        }

        public override void Dispose()
        {
            _UserManualRepository?.Dispose();
        }
    }
}