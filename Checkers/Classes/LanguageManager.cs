using System.Resources;
using System.Reflection;
using System.Globalization;

namespace Checkers.Classes
{
    /// <summary>
    /// класс для управления локализацией
    /// </summary>
    public class LanguageManager
    {
        private static ResourceManager resourceManager = new ResourceManager("Checkers.Resources.Strings", Assembly.GetExecutingAssembly());
        /// <summary>
        /// событие, которое вызывается при смене языка 
        /// </summary>
        public static event Action? OnLanguageChanged;
        /// <summary>
        /// устанавливает язык на основе указанного кода культуры
        /// </summary>
        public static void SetLanguage(string cultureCode)
        {
            var culture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentUICulture = culture;
            OnLanguageChanged?.Invoke();
        }
        /// <summary>
        /// получает локализованную строку по указанному ключу
        /// </summary>
        public static string GetString(string key)
        {
            return resourceManager.GetString(key, Thread.CurrentThread.CurrentUICulture);
        }
    }
}
