using Autofac.Core;
using Mobet.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Localization.Modules
{
    public class LocalizationManagerModule : Autofac.Module
    {
        public LocalizationManagerModule()
        {
        }
        private static void InjectProperties(object instance)
        {
            var instanceType = instance.GetType();

            // Get all the injectable properties to set.
            // If you wanted to ensure the properties were only UNSET properties,
            // here's where you'd do it.
            var properties = instanceType
              .GetProperties(BindingFlags.Public | BindingFlags.Instance)
              .Where(p => p.PropertyType == typeof(ILocalizationManager) && p.CanWrite && p.GetIndexParameters().Length == 0);

            // Set the properties located.
            foreach (var propToSet in properties)
            {
                propToSet.SetValue(instance, IocManager.Instance.Resolve<ILocalizationManager>(), null);
            }
        }

        private static void OnComponentPreparing(object sender, PreparingEventArgs e)
        {
            e.Parameters = e.Parameters.Union(
                                      new[]
                              {
                                new ResolvedParameter(
                                    (p, i) => p.ParameterType == typeof(ILocalizationManager),
                                    (p, i) => IocManager.Instance.Resolve<ILocalizationManager>()
                                ),
                              });
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            // Handle constructor parameters.
            registration.Preparing += OnComponentPreparing;

            // Handle properties.
            registration.Activated += (sender, e) => InjectProperties(e.Instance);
        }
    }
}
