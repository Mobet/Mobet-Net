using Mobet.Localization.Language;
using System.Collections.Generic;

namespace Mobet.Localization
{
    public interface ILanguageProvider
    {
        IReadOnlyList<LanguageInfo> GetLanguages();
    }
}