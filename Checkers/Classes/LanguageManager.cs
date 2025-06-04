using System;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Globalization;

namespace Checkers.Classes
{
    public class LanguageManager
    {
        private static ResourceManager resourceManager = new ResourceManager("Checkers.Resources.Strings", Assembly.GetExecutingAssembly());
        public static event Action OnLanguageChanged;
        public static void SetLanguage(string cultureCode)
        {
            var culture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentUICulture = culture;
            OnLanguageChanged?.Invoke();
        }
        public static string GetString(string key)
        {
            return resourceManager.GetString(key, Thread.CurrentThread.CurrentUICulture);
        }
    }
}
