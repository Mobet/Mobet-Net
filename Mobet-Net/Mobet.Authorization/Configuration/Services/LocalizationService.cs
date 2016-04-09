using IdentityServer3.Core;
using IdentityServer3.Core.Services;
using Mobet.Dependency;
using Mobet.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mobet.Authorization.Configuration.Services
{

    public class LocalizationService : ILocalizationService
    {
        public ILocalizationManager LocalizationManager { get; set; }

        public LocalizationService()
        {
            LocalizationManager = IocManager.Instance.Resolve<ILocalizationManager>();
        }

        public virtual string GetString(string category, string id)
        {
            switch (category)
            {
                case Constants.LocalizationCategories.Messages:
                    return LocalizationManager.GetSource(Mobet.Services.Constants.Localization.SourceName.Messages).GetString(id);
                case Constants.LocalizationCategories.Events:
                    return LocalizationManager.GetSource(Mobet.Services.Constants.Localization.SourceName.Events).GetString(id);
                case Constants.LocalizationCategories.Scopes:
                    return LocalizationManager.GetSource(Mobet.Services.Constants.Localization.SourceName.Scopes).GetString(id);
            }

            return null;
        }
    }
}