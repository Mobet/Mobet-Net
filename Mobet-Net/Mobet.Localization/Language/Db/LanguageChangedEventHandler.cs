using Mobet.Caching;
using Mobet.Dependency;
using Mobet.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Localization.Language.Db
{
    public class LanguageChangedEventHandler : IDependency, IEventHandler<LanguageChangedEvent>
    {
        private readonly ICacheManager _cacheManager;

        public LanguageChangedEventHandler(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public void HandleEvent(LanguageChangedEvent eventData)
        {
            _cacheManager.Remove(LocalizationConstansts.CacheKeys.Language + eventData.LanguageSourceName);
        }

    }
}
