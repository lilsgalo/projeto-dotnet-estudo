using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Enums;
using MeuProjeto.Business.Models;
using System;
using System.Threading.Tasks;

namespace MeuProjeto.Business.Interfaces
{
    public interface ISettingsService : IDisposable
    {
        public Task Update(Settings settings);
        public Task<Settings> GetById(Guid settingsId);
        public Task<IPagedList<Settings>> GetPagedList(FilteredPagedListParameters parameters);
        public Task<Settings> GetByCode(string code);
        public Task<Settings> GetByType(SettingsTypeEnum type);
    }
}