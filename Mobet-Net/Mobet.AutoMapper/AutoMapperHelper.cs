using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using AutoMapper;
using Mobet.AutoMapper;

namespace Mobet.AutoMapper
{
    internal static class AutoMapperHelper
    {
        public static void CreateMap(Type type)
        {
            CreateMap<AutoMapFromAttribute>(type);
            CreateMap<AutoMapToAttribute>(type);
            CreateMap<AutoMapAttribute>(type);
        }

        public static void CreateMap<TAttribute>(Type type) where TAttribute : AutoMapAttribute
        {
            if (!type.IsDefined(typeof(TAttribute)))
            {
                return;
            }

            foreach (var autoMapToAttribute in type.GetCustomAttributes<TAttribute>())
            {
                if (autoMapToAttribute.TargetTypes.Length == 0)
                {
                    continue;
                }

                foreach (var targetType in autoMapToAttribute.TargetTypes)
                {
                    if (autoMapToAttribute.Direction.HasFlag(AutoMapDirection.To))
                    {
                        Mapper.CreateMap(type, targetType);
                    }

                    if (autoMapToAttribute.Direction.HasFlag(AutoMapDirection.From))
                    {
                        Mapper.CreateMap(targetType, type);
                    }
                }
            }
        }
    }
}
