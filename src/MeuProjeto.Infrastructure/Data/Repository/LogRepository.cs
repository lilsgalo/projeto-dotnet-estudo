using MeuProjeto.Business.Interfaces;
using MeuProjeto.Business.Models;
using MeuProjeto.Infrastructure.Data.Context;
using System.Collections.Generic;
using System.Linq;

namespace MeuProjeto.Infrastructure.Data.Repository
{
    public class LogRepository : Repository<Log>, ILogRepository
    {
        public LogRepository(MeuDbContext context, ICustomLogger logger) : base(context, logger) { }

        public List<string> GetPagedListTables()
        {
            return Db.Logs.Select(p => p.TableName).Distinct().ToList();
        }
    }
}