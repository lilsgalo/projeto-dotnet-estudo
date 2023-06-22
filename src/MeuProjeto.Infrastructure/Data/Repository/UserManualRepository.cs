using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Threading.Tasks;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Interfaces;
using MeuProjeto.Business.Models;
using MeuProjeto.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Infrastructure.Data.Repository
{
    public class UserManualRepository : Repository<UserManual>, IUserManualRepository
    {
        public UserManualRepository(MeuDbContext context, ICustomLogger logger) : base(context, logger) { }

        public async Task<UserManual> GetByCode(string userManualCode)
        {
            userManualCode = userManualCode.ToUpper().Trim();
            return await Db.UserManuals.Include(i => i.LastReviewer).Include(i => i.Content).FirstOrDefaultAsync(p => p.Code.ToUpper().Trim() == userManualCode);
        }
        override
        public async Task Update(UserManual userManual)
        {
            DbSet.Update(userManual);
            await SaveChanges();
        }

        public async Task<IPagedList<UserManual>> GetPagedList(FilteredPagedListParameters parameters)
        {
            var dadosFiltrados = Db.UserManuals.Include(u => u.LastReviewer).Where(p =>
              (
                  parameters.Search == null ||
                  (
                      p.Code.Contains(parameters.Search)
                  )
              ));
            dadosFiltrados = dadosFiltrados.OrderBy(parameters.Sort);

            return await PagedList<UserManual>.ToPagedList(dadosFiltrados, parameters.CurrentPage, parameters.PageSize);
        }

    }
}