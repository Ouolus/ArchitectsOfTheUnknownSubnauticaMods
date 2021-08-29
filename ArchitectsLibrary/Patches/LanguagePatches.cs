namespace ArchitectsLibrary.Patches
{
    using API;
    using System.Collections.Generic;
    using System.IO;
    using HarmonyLib;
    using Oculus.Newtonsoft.Json;
    using SMLHelper.V2.Handlers;
    
    internal static class LanguagePatches
    {
        private const string FallbackLanguage = "English";

        internal static void Patch(Harmony harmony)
        {
            var orig = AccessTools.Method(typeof(Language), nameof(Language.LoadLanguageFile));
            var prefix = new HarmonyMethod(AccessTools.Method(typeof(LanguagePatches), nameof(LoadLanguageFilePrefix)));
            harmony.Patch(orig, prefix);
        }

        private static void LoadLanguageFilePrefix(string language)
        {
            LoadLanguages(language);
        }

        internal static void LoadLanguages(string language = FallbackLanguage)
        {
            foreach (var languagePath in LanguageSystem.LanguagePaths)
            {
                LoadLanguageImpl(language, languagePath);
            }
        }

        internal static void LoadLanguageImpl(string language, string languageFolder)
        {
            string fallbackPath = Path.Combine(languageFolder, $"{FallbackLanguage}.json");
            var file = Path.Combine(languageFolder, language + ".json");
            if (!File.Exists(file)) // if the preferred language doesn't have a file, use english, and return.
            {
                file = fallbackPath;
                if (File.Exists(file))
                {
                    SetLanguages(file, false);
                    return;
                }
            }
            
            SetLanguages(file, false); // load the preferred language
            
            if (language != FallbackLanguage) SetLanguages(fallbackPath, true); // if the current language is not already the fallback, then load the fallback language. some mixed in english is much better than raw language keys.

            void SetLanguages(string fileToSet, bool loadIntoFallback)
            {
                var deserialize = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(fileToSet));
                if (deserialize is null)
                    return;

                var dictionary = loadIntoFallback
                    ? LanguageSystem.FallbackLanguageStrings
                    : LanguageSystem.CurrentLanguageStrings;
                
                dictionary.Clear();
                
                foreach (var kvp in deserialize)
                {
                    dictionary[kvp.Key] = kvp.Value;
                    if (!loadIntoFallback || LanguageSystem.CurrentLanguageStrings.ContainsKey(kvp.Key)) // If we are loading a fallback language, we should ONLY set a language line if a translation for the key doesn't already exist. Fallback should never override current language.
                    {
                        LanguageHandler.SetLanguageLine(kvp.Key, kvp.Value);
                    }
                }
            }
        }
    }
}