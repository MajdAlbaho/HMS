using HMS.Api.Repositories.HMSDb;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Api.Repositories
{
    public interface IRepositoryBase<TEntity, TKey> where TEntity : class
    {
        TEntity Add(TEntity t);
        Task<TEntity> AddAsync(TEntity t);
        int Count();
        Task<int> CountAsync();
        void Delete(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
        void Delete(TKey Id);
        Task<int> DeleteAsync(TKey Id);
        void Dispose();
        TEntity Find(Expression<Func<TEntity, bool>> match);
        ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match);
        Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        Task<ICollection<TEntity>> FindByAsyn(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(TKey id);
        IQueryable<TEntity> GetAll();
        Task<ICollection<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetAsync(TKey id);
        void Save();
        Task<int> SaveAsync();
        TEntity Update(TEntity item, TKey key);
        Task<TEntity> UpdateAsyn(TEntity item, TKey key);
    }

    public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {
        protected HMSContext Context;

        public RepositoryBase(HMSContext context) {
            Context = context;
        }

        public IQueryable<TEntity> GetAll() {
            return Context.Set<TEntity>();
        }

        public virtual async Task<ICollection<TEntity>> GetAllAsync() {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public virtual TEntity Get(TKey id) {
            return Context.Set<TEntity>().Find(id);
        }

        public virtual async Task<TEntity> GetAsync(TKey id) {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public virtual TEntity Add(TEntity item) {
            var properyInfo = item.GetType().GetProperty("LastModifiedDate");
            if (properyInfo != null) {
                properyInfo.SetValue(item, DateTime.Now);
            }

            Context.Set<TEntity>().Add(item);
            Context.SaveChanges();
            return item;
        }

        public virtual async Task<TEntity> AddAsync(TEntity item) {
            var properyInfo = item.GetType().GetProperty("LastModifiedDate");
            if (properyInfo != null) {
                properyInfo.SetValue(item, DateTime.Now);
            }

            Context.Set<TEntity>().Add(item);
            await Context.SaveChangesAsync();
            return item;

        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> match) {
            return Context.Set<TEntity>().SingleOrDefault(match);
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match) {
            return await Context.Set<TEntity>().SingleOrDefaultAsync(match);
        }

        public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match) {
            return Context.Set<TEntity>().Where(match).ToList();
        }

        public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match) {
            return await Context.Set<TEntity>().Where(match).ToListAsync();
        }

        public virtual void Delete(TEntity entity) {
            Context.Set<TEntity>().Remove(entity);
            Context.SaveChanges();
        }

        public virtual void Delete(TKey Id) {
            var item = Get(Id);
            Context.Set<TEntity>().Remove(item);
            Context.SaveChanges();
        }

        public virtual async Task<int> DeleteAsync(TEntity entity) {
            Context.Set<TEntity>().Remove(entity);
            return await Context.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteAsync(TKey Id) {
            var item = await GetAsync(Id);
            Context.Set<TEntity>().Remove(item);
            return await Context.SaveChangesAsync();
        }

        public virtual TEntity Update(TEntity item, TKey key) {
            if (item == null)
                return null;
            TEntity exist = Context.Set<TEntity>().Find(key);
            if (exist != null) {
                Context.Entry(exist).CurrentValues.SetValues(item);
                Context.SaveChanges();
            }
            return exist;
        }

        public virtual async Task<TEntity> UpdateAsyn(TEntity item, TKey key) {
            if (item == null)
                return null;
            TEntity exist = await Context.Set<TEntity>().FindAsync(key);
            if (exist != null) {
                Context.Entry(exist).CurrentValues.SetValues(item);
                await Context.SaveChangesAsync();
            }
            return exist;
        }

        public int Count() {
            return Context.Set<TEntity>().Count();
        }

        public async Task<int> CountAsync() {
            return await Context.Set<TEntity>().CountAsync();
        }

        public virtual void Save() {

            Context.SaveChanges();
        }

        public virtual async Task<int> SaveAsync() {
            return await Context.SaveChangesAsync();
        }

        public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate) {
            IQueryable<TEntity> query = Context.Set<TEntity>().Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<TEntity>> FindByAsyn(Expression<Func<TEntity, bool>> predicate) {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties) {

            IQueryable<TEntity> queryable = GetAll();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties) {

                queryable = queryable.Include<TEntity, object>(includeProperty);
            }

            return queryable;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    Context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
