using System.Reflection;
using ArchitectsLibrary.Patches;
using HarmonyLib;
using QModManager.API.ModLoading;
using QModManager.Utility;

namespace ArchitectsLibrary
{
    [QModCore]
    public static class Main
    {
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();

        [QModPatch]
        public static void Load()
        {
            Logger.Log(Logger.Level.Info, "ArchitectsLibrary started Patching.");
            
            Initializer.PatchAllDictionaries();
            
            Harmony harmony = new Harmony($"ArchitectsOfTheUnknown_{myAssembly.GetName().Name}");
            
            VehiclePatches.Patch(harmony);
            
            Logger.Log(Logger.Level.Info, "ArchitectsLibrary successfully finished Patching!");
        }
        
    }
}