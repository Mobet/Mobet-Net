using Mobet.Caching;
using Mobet.Configuration.Startup;
using Mobet.Dependency;
using Mobet.RedisCache.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.RedisCache.Startup
{
    public static class StartupConfigurationExtensions
    {
        public static StartupConfiguration UseRedisCache(this StartupConfiguration bootstrap, Action<IRedisCacheConfiguartion> invoke)
        {
            IRedisCacheConfiguartion _redisCacheConfiguration = IocManager.Instance.Resolve<IRedisCacheConfiguartion>();
            invoke(_redisCacheConfiguration);

            if (string.IsNullOrWhiteSpace(_redisCacheConfiguration.NameOrConnectionString))
            {
                throw new ArgumentNullException("Redis connectstring cannot be empty. ");
            }

            IocManager.Instance.RegisterWithParameter<ICacheManager, RedisCacheManager>("configuration",
                    _redisCacheConfiguration.NameOrConnectionString,
                    DependencyLifeStyle.Singleton
                );

            return bootstrap;
        }

    }
}
