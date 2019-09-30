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
        Task DeleteAsync(TEntity entity);
        void Delete(TKey id);
        Task DeleteAsync(TKey id);
        void Dispose();
        TEntity Find(Expression<Func<TEntity, bool>> match);
        ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match);
        Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        Task<ICollection<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(TKey id);
        IQueryable<TEntity> GetAll();
        Task<ICollection<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetAsync(TKey id);
        void Save();
        Task SaveAsync();
        TEntity Update(TEntity item, TKey key);
        Task<TEntity> UpdateAsync(TEntity item, TKey key);
    }

    public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {
        protected HMSDbContext Context;

        public RepositoryBase(HMSDbContext context) {
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
            using (var transaction = Context.Database.BeginTransaction()) {
                try {
                    var propertyInfo = item.GetType().GetProperty("LastModifiedDate");
                    if (propertyInfo != null) {
                        propertyInfo.SetValue(item, DateTime.Now);
                    }

                    Context.Set<TEntity>().Add(item);
                    Context.SaveChanges();

                    transaction.Commit();
                    return item;
                } catch (Exception ex) {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public virtual async Task<TEntity> AddAsync(TEntity item) {
            using (var transaction = Context.Database.BeginTransaction()) {
                try {
                    var propertyInfo = item.GetType().GetProperty("LastModifiedDate");
                    if (propertyInfo != null) {
                        propertyInfo.SetValue(item, DateTime.Now);
                    }

                    Context.Set<TEntity>().Add(item);
                    await Context.SaveChangesAsync();

                    transaction.Commit();
                    return item;
                } catch (Exception ex) {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
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
            using (var transaction = Context.Database.BeginTransaction()) {
                try {
                    Context.Set<TEntity>().Remove(entity);
                    Context.SaveChanges();

                    transaction.Commit();
                } catch (Exception ex) {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }

        }

        public virtual void Delete(TKey id) {
            using (var transaction = Context.Database.BeginTransaction()) {
                try {
                    var item = Get(id);
                    Context.Set<TEntity>().Remove(item);
                    Context.SaveChanges();

                    transaction.Commit();
                } catch (Exception ex) {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public virtual async Task DeleteAsync(TEntity entity) {
            using (var transaction = Context.Database.BeginTransaction()) {
                try {
                    Context.Set<TEntity>().Remove(entity);
                    await Context.SaveChangesAsync();

                    transaction.Commit();
                } catch (Exception ex) {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public virtual async Task DeleteAsync(TKey id) {
            using (var transaction = Context.Database.BeginTransaction()) {
                try {
                    var item = await GetAsync(id);
                    Context.Set<TEntity>().Remove(item);
                    await Context.SaveChangesAsync();

                    transaction.Commit();
                } catch (Exception ex) {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public virtual TEntity Update(TEntity item, TKey key) {
            using (var transaction = Context.Database.BeginTransaction()) {
                try {
                    if (item == null)
                        return null;
                    TEntity exist = Context.Set<TEntity>().Find(key);
                    if (exist != null) {
                        Context.Entry(exist).CurrentValues.SetValues(item);
                        Context.SaveChanges();
                        transaction.Commit();
                    }
                    return exist;
                } catch (Exception ex) {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity item, TKey key) {
            using (var transaction = Context.Database.BeginTransaction()) {
                try {

                    if (item == null)
                        return null;
                    TEntity exist = await Context.Set<TEntity>().FindAsync(key);
                    if (exist != null) {
                        Context.Entry(exist).CurrentValues.SetValues(item);
                        await Context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    return exist;
                } catch (Exception ex) {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public int Count() {
            return Context.Set<TEntity>().Count();
        }

        public async Task<int> CountAsync() {
            return await Context.Set<TEntity>().CountAsync();
        }

        public virtual void Save() {
            using (var transaction = Context.Database.BeginTransaction()) {
                try {
                    Context.SaveChanges();
                    transaction.Commit();
                } catch (Exception ex) {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public virtual async Task SaveAsync() {
            using (var transaction = Context.Database.BeginTransaction()) {
                try {
                    await Context.SaveChangesAsync();

                    transaction.Commit();
                } catch (Exception ex) {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate) {
            IQueryable<TEntity> query = Context.Set<TEntity>().Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate) {
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
            if (!disposed) {
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
