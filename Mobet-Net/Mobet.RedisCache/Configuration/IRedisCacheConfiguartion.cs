using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.RedisCache.Configuration
{
    public interface IRedisCacheConfiguartion
    { 
        string NameOrConnectionString { get; set; }
    }
}
