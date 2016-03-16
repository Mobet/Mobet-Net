using System.Threading.Tasks;
using Mobet.Domain.UnitOfWork.Configuration;

namespace Mobet.Domain.UnitOfWork
{
    /// <summary>
    /// Null implementation of unit of work.
    /// It's used if no component registered for <see cref="IUnitOfWork"/>.
    /// This ensures working ABP without a database.
    /// </summary>
    public sealed class NullUnitOfWork : UnitOfWorkBase
    {
        public override void SaveChanges()
        {

        }

        public async override Task SaveChangesAsync()
        {

        }

        protected override void BeginUow()
        {

        }

        protected override void CompleteUow()
        {

        }

        protected async override Task CompleteUowAsync()
        {

        }

        protected override void DisposeUow()
        {

        }

        public NullUnitOfWork(IUnitOfWorkDefaultOptionsConfiguration defaultOptions)
            : base(defaultOptions)
        {
        }
    }
}
