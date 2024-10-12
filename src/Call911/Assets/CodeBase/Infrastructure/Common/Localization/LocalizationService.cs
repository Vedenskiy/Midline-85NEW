using System;

namespace CodeBase.Infrastructure.Common.Localization
{
    public class LocalizationService
    {
        public event Action LanguageChanged;
        
        public string GetTranslatedString(string key)
        {
            return key;
        }
    }
}