using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Enums;
using MeuProjeto.Business.Interfaces;
using MeuProjeto.Business.Models;
using System.Threading.Tasks;


namespace MeuProjeto.Business.Interfaces
{
    public interface ISettingsRepository: IRepository<Settings>
    {
        public Task<IPagedList<Settings>> GetPagedList(PagedListParameters parameters);
        public Task<Settings> GetByType(SettingsTypeEnum type);
        public Task<Settings> GetByCode(string code);
    }
}