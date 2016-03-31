using Mobet.Caching;
using Mobet.Dependency;
using Mobet.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Localization.Dictionaries.Db
{
    public class LanguageTextChangedEventHandler : IDependency,IEventHandler<LanguageTextChangedEvent>
    {
        private readonly ICacheManager _cacheManager;

        public LanguageTextChangedEventHandler(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public void HandleEvent(LanguageTextChangedEvent eventData)
        {
            _cacheManager.Remove(LocalizationConstansts.CacheKeys.LocalizationText + eventData.LanguageTextSourceName);
        }

    }
}
