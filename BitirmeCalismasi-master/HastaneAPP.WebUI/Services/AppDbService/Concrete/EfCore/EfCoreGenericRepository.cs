using System.Collections.Generic;
using System.Linq;
using HastaneAPP.WebUI.Services.AppDbService.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HastaneAPP.WebUI.Services.AppDbService.Concrete.EfCore
{
    public class EfCoreGenericRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext, new()
    {
        public void Create(TEntity entity)
        {
            using (var _context = new TContext())
            {
                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using(var _context = new TContext())
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public List<TEntity> GetAll()
        {
            using (var _context = new TContext())
            {
                return _context.Set<TEntity>().ToList();
            }
        }

        public TEntity GetById(int id)
        {
            using (var _context = new TContext())
            {
                return _context.Set<TEntity>().Find(id);
            }
        }

        public virtual void Update(TEntity entity)
        {
            using (var _context = new TContext())
            {
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}