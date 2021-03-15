using HarmonyLib;
using QModManager.API.ModLoading;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
using System.IO;
using System.Reflection;

namespace Draconis
{
    [QModCore]
    public static class Main
    {
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();
        private static string ModPath = Path.GetDirectoryName(myAssembly.Location);
        internal static string AssetsFolder = Path.Combine(ModPath, "Assets");
        public const string version = "1.0.0.0";
        [QModPatch]
        public static void Load()
        {
            Harmony.CreateAndPatchAll(myAssembly, $"ArchitectsOfTheUnknown_{myAssembly.GetName().Name}");
        }
    }
}
