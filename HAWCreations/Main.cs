using System.IO;
using System.Reflection;
using HarmonyLib;
using QModManager.API.ModLoading;

namespace HAWCreations
{
    [QModCore]
    public static class Main
    {
        internal static Assembly myAssembly = Assembly.GetExecutingAssembly();
        internal static string modPath = myAssembly.Location;
        internal static string AssetsFolder = Path.Combine(modPath, "Assets");
        [QModPatch]
        public static void Load()
        {
            Harmony.CreateAndPatchAll(myAssembly, $"HAW_{myAssembly.GetName().Name}");
        }
    }
}