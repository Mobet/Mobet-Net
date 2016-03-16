using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;
using Autofac.Extras.DynamicProxy2;
using Mobet.Dependency;
using Mobet.Domain.UnitOfWork;
using Mobet.Domain.Services;

namespace Mobet.Domain.UnitOfWork.ConventionalRegistras
{
    /// <summary>
    /// Registers classes derived from AppDbContext with configurations.
    /// </summary>
    public class UnitOfWorkConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.IocManager.Register<ICurrentUnitOfWorkProvider, CallContextCurrentUnitOfWorkProvider>();
            context.IocManager.RegisterIfNot<IUnitOfWork, NullUnitOfWork>(DependencyLifeStyle.Transient);

            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(context.Assembly)
                   .Where(t => typeof(IApplicationService).IsAssignableFrom(t) && t != typeof(IApplicationService) && !t.IsAbstract)
                   .AsImplementedInterfaces()
                   .EnableInterfaceInterceptors()
                   .InterceptedBy(typeof(UnitOfWorkInterceptor))
                   .InstancePerDependency();

            builder.RegisterType<UnitOfWorkInterceptor>();

            builder.Update(context.IocManager.IocContainer);
        }

    }
}
