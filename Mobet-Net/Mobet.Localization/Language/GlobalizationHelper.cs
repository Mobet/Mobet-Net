using System.Globalization;

namespace Mobet.Localization.Language
{
    public static class GlobalizationHelper
    {
        public static bool IsValidCultureCode(string cultureCode)
        {
            try
            {
                CultureInfo.GetCultureInfo(cultureCode);
                return true;
            }
            catch (CultureNotFoundException)
            {
                return false;
            }
        }
    }
}
