using System.Collections.Generic;
using Mobet.Dependency;
namespace Mobet.Localization.Language
{
    public interface ILanguageManager : IDependency
    {
        LanguageInfo CurrentLanguage { get; }

        IReadOnlyList<LanguageInfo> GetLanguages();
    }
}