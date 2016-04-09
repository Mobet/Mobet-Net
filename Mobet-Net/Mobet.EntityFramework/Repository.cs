using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Domain.Entities;
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
        protected virtual TDbContext _dbContext { get { return _dbContextProvider.DbContext; } }

        private readonly IEntityFrameworkDbContextProvider<TDbContext> _dbContextProvider;
        protected Repository(IEntityFrameworkDbContextProvider<TDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public IDbConnection Connection { get { return _dbContext.Database.Connection; } }
        public DbSet<TEntity> Table { get { return _dbContext.Set<TEntity>(); } }
        public IQueryable<TEntity> Models { get { return _dbContext.Set<TEntity>().AsQueryable(); } }


        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> lambda)
        {
            return _dbContext.Set<TEntity>().Where(lambda);
        }
        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, object>> includes)
        {
            IQueryable<TEntity> temp = _dbContext.Set<TEntity>();
            foreach (MemberInfo me in ((dynamic)includes.Body).Members)
            {
                temp = temp.Include(me.Name);
            }
            return temp.Where(lambda);
        }
        private void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
        private void SaveChangesAsync()
        {
            _dbContext.SaveChangesAsync();
        }

        public TEntity Add(TEntity model)
        {
            return _dbContext.Set<TEntity>().Add(model);
        }
        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> models)
        {
            return _dbContext.Set<TEntity>().AddRange(models);
        }

        public TEntity Update(TEntity model)
        {
            AttachIfNot(model);
            _dbContext.Entry(model).State = EntityState.Modified;
            return model;
        }
        public TEntity UpdateProperty(TEntity model, System.Linq.Expressions.Expression<Func<TEntity, object>> lambda)
        {
            ReadOnlyCollection<MemberInfo> memberInfos = ((dynamic)lambda.Body).Members;
            AttachIfNot(model);
            foreach (MemberInfo memberInfo in memberInfos)
            {
                _dbContext.Entry(model).Property(memberInfo.Name).IsModified = true;
            }
            return model;
        }
        public TEntity UpdateCompare(TEntity model, TEntity source)
        {
            _dbContext.Entry(source).CurrentValues.SetValues(model);
            return model;
        }

        public TEntity Remove(TEntity model)
        {
            return _dbContext.Set<TEntity>().Remove(model);
        }
        public IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> models)
        {
            return _dbContext.Set<TEntity>().RemoveRange(models);
        }
        public TEntity Remove(TPrimaryKey key)
        {
            return Remove(_dbContext.Set<TEntity>().Find(key));
        }
        public IEnumerable<TEntity> RemoveRange(IEnumerable<TPrimaryKey> keys)
        {
            List<TEntity> range = new List<TEntity>();
            foreach (var key in keys)
            {
                var model = _dbContext.Set<TEntity>().Find(key);
                range.Add(model);
            }
            return RemoveRange(range);
        }

        public bool Any(System.Linq.Expressions.Expression<Func<TEntity, bool>> lambda)
        {
            return _dbContext.Set<TEntity>().Any(lambda);
        }

        public int Count()
        {
            return _dbContext.Set<TEntity>().Count();
        }
        public int Count(System.Linq.Expressions.Expression<Func<TEntity, bool>> lambda)
        {
            return _dbContext.Set<TEntity>().Count(lambda);
        }

        public TEntity Find(TPrimaryKey key)
        {
            return _dbContext.Set<TEntity>().Find(key);
        }
        public TEntity FirstOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> lambda)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(lambda);
        }
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, object>> includes)
        {
            IQueryable<TEntity> temp = _dbContext.Set<TEntity>();
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
