using System.Linq.Dynamic.Core;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MeuProjeto.Business.Models;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Enums;
using MeuProjeto.Infrastructure.Data.Context;
using MeuProjeto.Business.Interfaces;

namespace MeuProjeto.Infrastructure.Data.Repository
{
    public class SettingsRepository : Repository<Settings>, ISettingsRepository
    {
        public SettingsRepository(MeuDbContext context, ICustomLogger logger) : base(context, logger) { }

        public async Task<IPagedList<Settings>> GetPagedList(PagedListParameters parameters)
        {
            var dadosFiltrados = Db.Settings.AsQueryable();

            dadosFiltrados = dadosFiltrados.OrderBy(parameters.Sort);

            return await PagedList<Settings>.ToPagedList(dadosFiltrados, parameters.CurrentPage, parameters.PageSize);
        }

        public async Task<Settings> GetByType(SettingsTypeEnum type)
        {
            return await Db.Settings.AsNoTracking()
                            .FirstOrDefaultAsync(i => i.Type == type);
        }
        public async Task<Settings> GetByCode(string code)
        {
            return await Db.Settings.AsNoTracking()
                            .FirstOrDefaultAsync(i => i.Code == code);
        }
    }
}