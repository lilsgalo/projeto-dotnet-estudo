using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeuProjeto.Business.Models;

namespace MeuProjeto.Business.Interfaces
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        public Task<RefreshToken> GetByToken(string token);

        public Task<List<RefreshToken>> GetActiveByUserId(Guid userId);
    }
}