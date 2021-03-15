using System.Reflection;
using HarmonyLib;
using QModManager.API.ModLoading;
using QModManager.Utility;

namespace HAWCreations
{
    [QModCore]
    public static class Main
    {
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();

        [QModPatch]
        public static void Load()
        {
            Logger.Log(Logger.Level.Info, "ArchitectsLibrary started Patching.");
            
            Harmony.CreateAndPatchAll(myAssembly, $"ArchitectsOfTheUnknown_{myAssembly.GetName().Name}");
            
            Logger.Log(Logger.Level.Info, "ArchitectsLibrary successfully finished Patching!");
        }
    }
}