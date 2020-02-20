using Dotz.Core.Domain.Models;
using Dotz.Core.Domain.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dotz.Infra.Data.EFCore
{
  public abstract class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
  {
    public DataContext _context;
    public Repository(DataContext context)
    {
      _context = context;
    }
    public IQueryable<TEntity> Get()
    {
      return _context.Set<TEntity>();
    }
    public IQueryable<TEntity> Get(Func<TEntity, bool> predicate, Expression<Func<TEntity, object>> includes)
    {
      return Get().Include(includes).Where(predicate).AsQueryable();
    }
    public IQueryable<TEntity> Get(Func<TEntity, bool> predicate)
    {
      return Get().Where(predicate).AsQueryable();
    }
    public TEntity Find(params object[] key)
    {
      return _context.Set<TEntity>().Find(key);
    }
    public virtual TEntity Add(TEntity obj)
    {
      _context.Set<TEntity>();
      _context.Add(obj);
      _context.SaveChanges();
      return obj;
    }

    public virtual TEntity Update(TEntity obj)
    {
      _context.Set<TEntity>();
      _context.Update(obj);
      _context.SaveChanges();

      return obj;
    }

    public void AddAll(IQueryable<TEntity> List)
    {
      _context.Set<TEntity>().AddRange(List);
      _context.SaveChanges();
    }
    public virtual void Delete(Func<TEntity, bool> predicate)
    {
      _context.Set<TEntity>().RemoveRange(Get(predicate));
      _context.SaveChanges();
    }
    public virtual void Delete(TEntity obj)
    {
      _context.Remove(obj);
      _context.SaveChanges();
    }
    public void DeleteAll()
    {
      _context.Set<TEntity>().RemoveRange(_context.Set<TEntity>().AsQueryable());
      _context.SaveChanges();
    }
    public void Dispose()
    {
      _context.Dispose();
    }
  }
}