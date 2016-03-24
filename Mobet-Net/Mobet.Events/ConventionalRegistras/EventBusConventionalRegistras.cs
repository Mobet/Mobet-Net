using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Dependency;
using Mobet.Events.Configuration;
using Autofac;
using Mobet.Events.Handlers;
using System.Reflection;

namespace Mobet.Events.ConventionalRegistras
{
    public class EventBusConventionalRegistras : IConventionalDependencyRegistrar
    {

        private IEventBusConfiguration _eventBusConfiguration;
        private IEventBus _eventBus;
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            _eventBusConfiguration = IocManager.Instance.Resolve<IEventBusConfiguration>();

            if (_eventBusConfiguration.UseDefaultEventBus)
            {
                _eventBus = IocManager.Instance.IsRegistered<IEventBus>() ? IocManager.Instance.Resolve<IEventBus>() : NullEventBus.Instance;
                var builder = new ContainerBuilder();
                builder.RegisterAssemblyTypes(context.Assembly)
                        .Where(t => typeof(IEventHandler).IsAssignableFrom(t) && t != typeof(IEventHandler) && !t.IsAbstract)
                        .InstancePerDependency();

                builder.Update(IocManager.Instance.IocContainer);

                foreach (var assembly in context.Assembly)
                {
                    var types = assembly.GetTypes().Where(t => typeof(IEventHandler).IsAssignableFrom(t) && t != typeof(IEventHandler));
                    foreach (var type in types)
                    {
                        if (!typeof(IEventHandler).IsAssignableFrom(type) || type.IsNotPublic)
                        {
                            return;
                        }
                        var interfaces = type.GetInterfaces();
                        foreach (var inter in interfaces)
                        {
                            var genericArgs = inter.GetGenericArguments();
                            if (genericArgs.Length == 1)
                            {
                                _eventBus.Register(genericArgs[0], (IEventHandler)IocManager.Instance.Resolve(type));
                            }
                        }
                       

                    }
                }
            }
        }
    }
}
