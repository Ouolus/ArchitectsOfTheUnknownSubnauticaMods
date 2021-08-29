using UnityEngine;

namespace ArchitectsLibrary.API
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using Patches;

    /// <summary>
    /// A class for Multi-Language localization.
    /// </summary>
    public static class LanguageSystem
    {
        internal static readonly List<string> LanguagePaths = new();
        internal static readonly Dictionary<string, string> CurrentLanguageStrings = new();
        internal static readonly Dictionary<string, string> FallbackLanguageStrings = new();
        private static readonly string DefaultLanguage;

        static LanguageSystem()
        {
            string savedLanguagePath = PlayerPrefs.GetString("Language", null);
            if (!string.IsNullOrEmpty(savedLanguagePath))
            {
                DefaultLanguage = Path.GetFileNameWithoutExtension(savedLanguagePath);
            }

            if (CurrentLanguageStrings.Count > 0 || FallbackLanguageStrings.Count > 0)
                return;

            LanguagePatches.LoadLanguages();
        }

        /// <summary>
        /// A read-only property of "Undefined" string literal.
        /// </summary>
        public static string Default => "Undefined";

        /// <summary>
        /// Registers a folder path as a Multi-Language folder
        /// </summary>
        /// <param name="languageFolderName">the folder name</param>
        public static void RegisterLocalization(string languageFolderName = "Localization")
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), languageFolderName);
            LanguagePaths.Add(path);
            if (!string.IsNullOrEmpty(DefaultLanguage))
            {
                LanguagePatches.LoadLanguageImpl(DefaultLanguage, path);
            }
        }

        /// <summary>
        /// Gets the translation of the key
        /// </summary>
        /// <param name="key">the key</param>
        /// <returns>the translation string</returns>
        public static string Get(string key)
        {
            if (CurrentLanguageStrings.TryGetValue(key, out var translation))
            {
                return translation;
            }
            return FallbackLanguageStrings.GetOrDefault(key, Default);
        }
        
        /// <summary>
        /// Gets a translation of a tooltip of a TechType. Same as <see cref="Get"/> but grabs a translation by the key "Tooltip_{<paramref name="key"/>}"
        /// </summary>
        /// <param name="key">the key</param>
        /// <returns>the translation string</returns>
        public static string GetTooltip(string key)
        {
            if (CurrentLanguageStrings.TryGetValue($"Tooltip_{key}", out var translation))
            {
                return translation;
            }
            return FallbackLanguageStrings.GetOrDefault($"Tooltip_{key}", Default);
        }
    }
}