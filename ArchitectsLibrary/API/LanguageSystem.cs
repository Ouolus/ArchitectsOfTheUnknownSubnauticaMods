using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ArchitectsLibrary.API
{
    /// <summary>
    /// A class for Multi-Language localization.
    /// </summary>
    public static class LanguageSystem
    {
        internal static List<string> languagePaths = new();

        /// <summary>
        /// Registers a folder path as a Multi-Language folder
        /// </summary>
        /// <param name="languageFolderName">the folder name</param>
        public static void RegisterLocalization(string languageFolderName = "Languages")
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), languageFolderName);
            languagePaths.Add(path);
        }
    }
}