using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


using Mobet.Configuration.Startup;
using Mobet.AutoMapper;

namespace Mobet.AutoMapper.Startup
{
    public static class StartupConfigurationExtensions
    {
        private static bool _isCreatedMappingsBefore;
        private static readonly object _synclock = new object();
        public static StartupConfiguration UseAutoMapper(this StartupConfiguration bootstrap)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToArray();
            lock (_synclock)
            {
                if (_isCreatedMappingsBefore)
                {
                    return bootstrap;
                }
                MapAutoAttributes(assemblies);
                MapOtherMappings(assemblies);

                _isCreatedMappingsBefore = true;
            }
            return bootstrap;
        }
        private static void MapAutoAttributes(Assembly[] assemblies)
        {
            var types = GetAllTypes(type => type.IsDefined(typeof(AutoMapAttribute)) || type.IsDefined(typeof(AutoMapFromAttribute)) || type.IsDefined(typeof(AutoMapToAttribute)), assemblies);
            foreach (var type in types)
            {
                AutoMapperHelper.CreateMap(type);
            }
        }
        private static void MapOtherMappings(Assembly[] assemblies)
        {
            var types = GetAllTypes(type => typeof(IAutoMaping).IsAssignableFrom(type) && type != typeof(IAutoMaping) && !type.IsAbstract, assemblies).ToList();
            types.ForEach(x =>
            {
                x.GetMethod("CreateMapping").Invoke(Activator.CreateInstance(x), null);
            });
        }
        private static Type[] GetAllTypes(Func<Type, bool> predicate, Assembly[] assemblies)
        {
            var allTypes = new List<Type>();

            foreach (var assembly in assemblies)
            {
                allTypes.AddRange(assembly.GetTypes().Where(type => type != null));
            }

            return allTypes.Where(predicate).ToArray();
        }
    }
}
