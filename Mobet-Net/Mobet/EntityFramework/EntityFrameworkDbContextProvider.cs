using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Domain.UnitOfWork;
using Mobet.EntityFramework.Extensions;

namespace Mobet.EntityFramework
{
    /// <summary>
    /// Implements <see cref="IDbContextProvider{TDbContext}"/> that gets DbContext from
    /// active unit of work.
    /// </summary>
    /// <typeparam name="TDbContext">Type of the DbContext</typeparam>
    public class EntityFrameworkDbContextProvider<TDbContext> : IEntityFrameworkDbContextProvider<TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// Gets the DbContext.
        /// </summary>
        public TDbContext DbContext { get { return _unitOfWorkProvider.Current.GetDbContext<TDbContext>(); } }

        private readonly ICurrentUnitOfWorkProvider _unitOfWorkProvider;

        /// <summary>
        /// Creates a new <see cref="EntityFrameworkDbContextProvider{TDbContext}"/>.
        /// </summary>
        /// <param name="currentUnitOfWorkProvider"></param>
        public EntityFrameworkDbContextProvider(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _unitOfWorkProvider = currentUnitOfWorkProvider;
        }
    }
}
