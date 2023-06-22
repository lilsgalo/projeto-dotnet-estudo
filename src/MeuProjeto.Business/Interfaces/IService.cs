using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MeuProjeto.Business.Models;

namespace MeuProjeto.Business.Interfaces
{
    public interface IService<TEntity> : IDisposable where TEntity : Entity
    {
    }
}