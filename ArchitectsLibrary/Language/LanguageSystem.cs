using System.Collections.Generic;
using System.IO;
using System.Reflection;
#if SN1
using Oculus.Newtonsoft.Json;
#else
using Newtonsoft.Json;
#endif
using QModManager.Utility;
using SMLHelper.V2.Handlers;

namespace ArchitectsLibrary.Language
{
    /// <summary>
    /// A simple class that allows you to make a new <see cref="LanguageSystem"/> and read up a JSON file and use its content dictionary in your code.
    /// </summary>
    public class LanguageSystem
    {
        Dictionary<string, string> _strings = new();
        
        static readonly string _modPath = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);

        bool _initialized;
        string _langPath = Path.Combine(_modPath, "Languages");

        /// <summary>
        /// Initialize a new <see cref="LanguageSystem"/>.
        /// </summary>
        /// <param name="jsonPath">the JSON file name, should be placed in {your mod's location}/Languages/</param>
        public LanguageSystem(string jsonPath)
        {
            if (jsonPath is null)
            {
                Logger.Log(Logger.Level.Error, "jsonPath is null");
                return;
            }
            
            string path;
            if (jsonPath.EndsWith(".json"))
                path = Path.Combine(_langPath, jsonPath);
            else
            {
                var fileName = jsonPath + ".json";
                path = Path.Combine(_langPath, fileName);
            }

            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    var serializer = new JsonSerializer();
                    _strings = (Dictionary<string, string>)serializer.Deserialize(reader, typeof(Dictionary<string, string>));
                }
            }
            else
            {
                Logger.Log(Logger.Level.Error, $"Couldn't find file {path}");
            }
        }

        /// <summary>
        /// Adds all of the Keys/Values of this file to the Game's <see cref="Language"/> system.
        /// </summary>
        public void InitializeAllToLanguage()
        {
            if (_initialized)
                return;
            
            _strings.ForEach(x => LanguageHandler.SetLanguageLine(x.Key, x.Value));
            _initialized = true;
        }
        
        /// <summary>
        /// use this method to get a value from the loaded Dictionary.
        /// </summary>
        /// <param name="key">the key to look for its value.</param>
        /// <returns>value found from the passed key.</returns>
        public string Get(string key)
        {
            if (_strings.TryGetValue(key, out string value))
                return value;

            return null;
        }
    }
}
