using System.Collections.Generic;
using System.IO;
using ArchitectsLibrary.API;
using HarmonyLib;
using Oculus.Newtonsoft.Json;
using SMLHelper.V2.Handlers;

namespace ArchitectsLibrary.Patches
{
    internal static class LanguagePatches
    {
        internal static void Patch(Harmony harmony)
        {
            var orig = AccessTools.Method(typeof(Language), nameof(Language.LoadLanguageFile));
            var prefix = new HarmonyMethod(AccessTools.Method(typeof(LanguagePatches), nameof(LoadLanguageFilePrefix)));
            harmony.Patch(orig, prefix);
        }

        private static void LoadLanguageFilePrefix(string language)
        {
            foreach (var languagePath in LanguageSystem.languagePaths)
            {
                LoadLanguages(language, languagePath);
            }
        }

        private static void LoadLanguages(string language, string languageFolder)
        {
            var file = Path.Combine(languageFolder, language + ".json");
            if (!File.Exists(file))
            {
                file = Path.Combine(languageFolder, "English.json");
                if (File.Exists(file))
                {
                    SetLanguages(file);
                    return;
                }
            }
            
            SetLanguages(file);

            void SetLanguages(string file)
            {
                var deserialize = JsonConvert.DeserializeObject<Dictionary<string, string>>(file);
                if (deserialize is null)
                    return;
                
                LanguageSystem.currentLanguageStrings.Clear();
                
                foreach (var kvp in deserialize)
                {
                    LanguageSystem.currentLanguageStrings[kvp.Key] = kvp.Value;
                    if (!kvp.Key.StartsWith("Tooltip_"))
                        LanguageHandler.SetLanguageLine(kvp.Key, kvp.Value);
                }
            }
        }
    }
}