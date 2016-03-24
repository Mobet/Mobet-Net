using System.Linq;

using Autofac;
using Mobet.Dependency;
using Mobet.EntityFramework.Configuration;

namespace Mobet.EntityFramework.ConventionalRegistras
{
    /// <summary>
    /// Registers classes derived from AppDbContext with configurations.
    /// </summary>
    public class EntityFrameworkConventionalRegistrar : IConventionalDependencyRegistrar
    {
        private IEntityFrameworkConfiguration _config;
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            _config = context.IocManager.Resolve<IEntityFrameworkConfiguration>();
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(context.Assembly)
                   .Where(t => typeof(EntityFrameworkDbContext).IsAssignableFrom(t) && t != typeof(EntityFrameworkDbContext) && !t.IsAbstract)
                   .WithParameter("nameOrConnectionString", _config.DefaultNameOrConnectionString)
                   .AsSelf()
                   .InstancePerDependency();

            builder.RegisterGeneric(typeof(EntityFrameworkDbContextProvider<>))
                    .As(typeof(IEntityFrameworkDbContextProvider<>))
                    .InstancePerDependency();

            builder.Update(context.IocManager.IocContainer);
        }
    }
}
