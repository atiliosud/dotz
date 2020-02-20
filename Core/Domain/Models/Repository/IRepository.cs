using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dotz.Core.Domain.Models.Repository
{
  public interface IRepository<TEntity>
  {
    IQueryable<TEntity> Get();
    IQueryable<TEntity> Get(Func<TEntity, bool> predicate, Expression<Func<TEntity, object>> includes);
    IQueryable<TEntity> Get(Func<TEntity, bool> predicate);
    TEntity Find(params object[] key);
    TEntity Add(TEntity obj);
    TEntity Update(TEntity obj);
    void Delete(TEntity obj);
    void Delete(Func<TEntity, bool> predicate);
  }
}