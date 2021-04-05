using System.Reflection;
using ArchitectsLibrary.Patches;
using HarmonyLib;
using QModManager.API.ModLoading;
using QModManager.Utility;
using UnityEngine;
using System.Collections;

namespace ArchitectsLibrary
{
    [QModCore]
    public static class Main
    {
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();
        public static Material ionCubeMaterial;

        [QModPatch]
        public static void Load()
        {
            QModManager.Utility.Logger.Log(QModManager.Utility.Logger.Level.Info, "ArchitectsLibrary started Patching.");
            
            Initializer.PatchAllDictionaries();
            
            Harmony harmony = new Harmony($"ArchitectsOfTheUnknown_{myAssembly.GetName().Name}");
            
            VehiclePatches.Patch(harmony);

            QModManager.Utility.Logger.Log(QModManager.Utility.Logger.Level.Info, "ArchitectsLibrary successfully finished Patching!");

            UWE.CoroutineHost.StartCoroutine(LoadIonCubeMaterial());
        }
        
        private static IEnumerator LoadIonCubeMaterial()
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.PrecursorIonCrystal);
            yield return task;

            GameObject ionCube = task.GetResult();
            ionCubeMaterial = ionCube.GetComponentInChildren<MeshRenderer>().material;
        }
    }
}