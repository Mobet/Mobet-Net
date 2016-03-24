using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Domain.Models;
using Mobet.Domain.UnitOfWork;
using Mobet.Domain.Repositories;
using System.Data;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq.Expressions;

namespace Mobet.EntityFramework
{
    public class Repository<TDbContext, TEntity> : Repository<TDbContext, TEntity, int>
        where TEntity : class, IEntity
        where TDbContext : DbContext
    {
        protected Repository(IEntityFrameworkDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
    public class Repository<TDbContext, TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TDbContext : DbContext
    {
        protected virtual TDbContext DbContext { get { return _dbContextProvider.DbContext; } }

        private readonly IEntityFrameworkDbContextProvider<TDbContext> _dbContextProvider;
        protected Repository(IEntityFrameworkDbContextProvider<TDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public IDbConnection Connection { get { return DbContext.Database.Connection; } }
        public DbSet<TEntity> Table { get { return DbContext.Set<TEntity>(); } }
        public IQueryable<TEntity> Models { get { return DbContext.Set<TEntity>().AsQueryable(); } }

        private void SaveChanges()
        {
            DbContext.SaveChanges();
        }
        private void SaveChangesAsync()
        {
            DbContext.SaveChangesAsync();
        }

        public TEntity Add(TEntity model)
        {
            return DbContext.Set<TEntity>().Add(model);
        }
        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> models)
        {
            return DbContext.Set<TEntity>().AddRange(models);
        }

        public TEntity Update(TEntity model)
        {
            AttachIfNot(model);
            DbContext.Entry(model).State = EntityState.Modified;
            return model;
        }
        public TEntity UpdateProperty(TEntity model, System.Linq.Expressions.Expression<Func<TEntity, object>> lambda)
        {
            ReadOnlyCollection<MemberInfo> memberInfos = ((dynamic)lambda.Body).Members;
            AttachIfNot(model);
            foreach (MemberInfo memberInfo in memberInfos)
            {
                DbContext.Entry(model).Property(memberInfo.Name).IsModified = true;
            }
            return model;
        }
        public TEntity UpdateCompare(TEntity model, TEntity source)
        {
            DbContext.Entry(source).CurrentValues.SetValues(model);
            return model;
        }

        public TEntity Remove(TEntity model)
        {
            return DbContext.Set<TEntity>().Remove(model);
        }
        public IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> models)
        {
            return DbContext.Set<TEntity>().RemoveRange(models);
        }
        public TEntity Remove(TPrimaryKey key)
        {
            return Remove(DbContext.Set<TEntity>().Find(key));
        }
        public IEnumerable<TEntity> RemoveRange(IEnumerable<TPrimaryKey> keys)
        {
            List<TEntity> range = new List<TEntity>();
            foreach (var key in keys)
            {
                var model = DbContext.Set<TEntity>().Find(key);
                range.Add(model);
            }
            return RemoveRange(range);
        }

        public bool Any(System.Linq.Expressions.Expression<Func<TEntity, bool>> lambda)
        {
            return DbContext.Set<TEntity>().Any(lambda);
        }

        public int Count()
        {
            return DbContext.Set<TEntity>().Count();
        }
        public int Count(System.Linq.Expressions.Expression<Func<TEntity, bool>> lambda)
        {
            return DbContext.Set<TEntity>().Count(lambda);
        }

        public TEntity Find(TPrimaryKey key)
        {
            return DbContext.Set<TEntity>().Find(key);
        }
        public TEntity FirstOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> lambda)
        {
            return DbContext.Set<TEntity>().FirstOrDefault(lambda);
        }
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, object>> includes)
        {
            IQueryable<TEntity> temp = DbContext.Set<TEntity>();
            foreach (MemberInfo me in ((dynamic)includes.Body).Members)
            {
                temp = temp.Include(me.Name);
            }
            return temp.FirstOrDefault(lambda);
        }
        protected virtual void AttachIfNot(TEntity model)
        {
            if (!Table.Local.Contains(model))
            {
                Table.Attach(model);
            }
        }
    }

}
