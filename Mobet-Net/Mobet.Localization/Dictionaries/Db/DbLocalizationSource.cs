using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Localization.Dictionaries.Db
{
    public class DbLocalizationSource : DictionaryBasedLocalizationSource
    {
        public DbLocalizationSource(string name, DbLocalizationDictionaryProvider dictionaryProvider) 
            : base(name, dictionaryProvider)
        {
            Logger = NullLogger.Instance;
        }
    }
}
