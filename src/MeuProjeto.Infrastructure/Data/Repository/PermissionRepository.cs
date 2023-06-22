using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuProjeto.Business.Interfaces;
using MeuProjeto.Business.Models;
using MeuProjeto.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Infrastructure.Data.Repository
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        public PermissionRepository(MeuDbContext context, ICustomLogger logger) : base(context, logger) { }

        public bool PermissionExists(Permission permission)
        {
            return Db.Permissions.Any(p => permission.Type == p.Type && permission.Value == p.Value);
        }

        public override async Task<List<Permission>> GetAll()
        {
            return await Db.Permissions.OrderBy(p => p.Type).ToListAsync();
        }
    }
}