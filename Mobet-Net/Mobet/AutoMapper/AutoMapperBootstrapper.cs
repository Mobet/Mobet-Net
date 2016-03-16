using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Web.Compilation;
using System.Text.RegularExpressions;

using Mobet.AutoMapper;

namespace Mobet.AutoMapper
{
    public class AutoMapperBootstrapper
    {
        private static bool _isCreatedMappingsBefore;
        private static readonly object _syncObj = new object();
        public static void Initialize(Assembly[] assemblies)
        {
            lock (_syncObj)
            {
                //We should prevent duplicate mapping in an application, since AutoMapper is static.
                if (_isCreatedMappingsBefore)
                {
                    return;
                }
                MapAutoAttributes(assemblies);
                MapOtherMappings(assemblies);

                _isCreatedMappingsBefore = true;
            }
        }
        private static void MapAutoAttributes(Assembly[] assemblies)
        {
            var types = GetAllTypes(type => type.IsDefined(typeof(AutoMapAttribute)) || type.IsDefined(typeof(AutoMapFromAttribute)) || type.IsDefined(typeof(AutoMapToAttribute)),assemblies);
            foreach (var type in types)
            {
                AutoMapperHelper.CreateMap(type);
            }
        }
        private static void MapOtherMappings(Assembly[] assemblies)
        {
            var types = GetAllTypes(type => typeof(IAutoMaping).IsAssignableFrom(type) && type != typeof(IAutoMaping) && !type.IsAbstract,assemblies).ToList();
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
