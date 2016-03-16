using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Autofac;
using Autofac.Integration.Mvc;

namespace Mobet.Dependency.ConventionalRegistrars
{
    /// <summary>
    /// This class is used to register basic dependency implementations such as <see cref="IDependency"/> and <see cref="ISingletonDependency"/>.
    /// </summary>
    public class BasicConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            var builder = new ContainerBuilder();

            //IDependency
            builder
                .RegisterAssemblyTypes(context.Assembly)
                .Where(t => typeof(IDependency).IsAssignableFrom(t) && t != typeof(IDependency) && !t.IsAbstract)
                .AsImplementedInterfaces()
                .InstancePerDependency();

            //ISingletonDependency
            builder
                .RegisterAssemblyTypes(context.Assembly)
                .Where(t => typeof(ISingletonDependency).IsAssignableFrom(t) && t != typeof(ISingletonDependency) && !t.IsAbstract)
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.Update(context.IocManager.IocContainer);
        }
    }
}
