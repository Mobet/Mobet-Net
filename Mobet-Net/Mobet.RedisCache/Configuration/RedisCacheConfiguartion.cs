using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.RedisCache.Configuration
{
    public class RedisCacheConfiguartion : IRedisCacheConfiguartion
    {
        public string NameOrConnectionString { get; set; }

    }
}
