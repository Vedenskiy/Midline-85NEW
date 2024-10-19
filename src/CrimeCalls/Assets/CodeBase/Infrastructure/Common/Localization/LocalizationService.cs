using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.Common.Localization
{
    public class LocalizationService
    {
        private IDictionary<string, string> _table;

        public event Action LanguageChanged;

        public void Load(IDictionary<string, string> table) => 
            _table = table;

        public string GetTranslatedString(string key)
        {
            if (_table.TryGetValue(key, out var value))
                return value;
            
            return key;
        }
    }
}