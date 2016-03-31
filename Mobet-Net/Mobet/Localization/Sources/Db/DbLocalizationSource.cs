using Castle.Core.Logging;
using Mobet.Localization.Dictionaries;
using Mobet.Localization.Dictionaries.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Localization.Sources.Db
{
    public class DbLocalizationSource : DictionaryBasedLocalizationSource
    {
        public DbLocalizationSource(string name, DbLocalizationDictionaryProvider dictionaryProvider) 
            : base(name, dictionaryProvider)
        {
        }
    }
}
