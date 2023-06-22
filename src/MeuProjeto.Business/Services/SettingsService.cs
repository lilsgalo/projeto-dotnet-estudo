using System;
using System.Threading.Tasks;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Enums;
using MeuProjeto.Business.Interfaces;
using MeuProjeto.Business.Models;
using MeuProjeto.Business.Models.Validations;
using MeuProjeto.Business.Resources;
using MeuProjeto.Business.Services;

namespace MeuProjeto.Business.Services
{
    public class SettingsService : BaseService<Settings>, ISettingsService
    {
        private readonly ISettingsRepository _settingsRepository;
        public SettingsService(ISettingsRepository settingsRepository,
                               INotifier notificador) : base(notificador, settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public async Task Update(Settings settings)
        {
            if (!RunValidation(new SettingsValidation(), settings)) return;

            await _settingsRepository.Update(settings);
        }

        public async Task<Settings> GetById(Guid id)
        {
            var settings = await _settingsRepository.GetById(id);

            if (settings == null) Notify(SettingsResources.Errors.NotFound.Formatted);

            return settings;
        }

        public Task<IPagedList<Settings>> GetPagedList(FilteredPagedListParameters parameters)
        {
            return _settingsRepository.GetPagedList(parameters);
        }

        public async Task<Settings> GetByCode(string code)
        {
            var settings = await _settingsRepository.GetByCode(code);

            if (settings == null) Notify(SettingsResources.Errors.NotFound.Formatted);

            return settings;
        }

        public async Task<Settings> GetByType(SettingsTypeEnum type)
        {
            var settings = await _settingsRepository.GetByType(type);

            if (settings == null) Notify(SettingsResources.Errors.NotFound.Formatted);

            return settings;
        }
    }
}