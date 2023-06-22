using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Threading.Tasks;
using MeuProjeto.Business.Interfaces;
using MeuProjeto.Business.Models;
using MeuProjeto.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Infrastructure.Data.Repository
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(MeuDbContext context, ICustomLogger logger) : base(context, logger) { }

        public async Task<RefreshToken> GetByToken(string token)
        {
            return await Db.RefreshTokens.Include(r => r.User).FirstOrDefaultAsync(r => r.Token == token);
        }

        public async Task<List<RefreshToken>> GetActiveByUserId(Guid userId)
        {
            return await Db.RefreshTokens.Where(r => r.UserId == userId && r.Revoked == null).ToListAsync();
        }

    }
}