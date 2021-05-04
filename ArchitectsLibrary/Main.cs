using System.Reflection;
using ArchitectsLibrary.Patches;
using HarmonyLib;
using QModManager.API.ModLoading;
using QModManager.Utility;
using UnityEngine;
using System.Collections;
using CreatorKit.Patches;
using UWE;

namespace ArchitectsLibrary
{
    [QModCore]
    public static class Main
    {
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();
        public static Material ionCubeMaterial;
        public static Material precursorGlassMaterial;

        [QModPatch]
        public static void Load()
        {
            QModManager.Utility.Logger.Log(QModManager.Utility.Logger.Level.Info, "ArchitectsLibrary started Patching.");
            
            Initializer.PatchAllDictionaries();      

            QModManager.Utility.Logger.Log(QModManager.Utility.Logger.Level.Info, "ArchitectsLibrary successfully finished Patching!");

            UWE.CoroutineHost.StartCoroutine(LoadIonCubeMaterial());

            //CreatorKit.SNCreatorKit.Entry();

            Harmony harmony = new Harmony($"ArchitectsOfTheUnknown_{myAssembly.GetName().Name}");

            VehiclePatches.Patch(harmony); 
            //MainMenuMusicPatches.Patch(harmony);
        }

        private static IEnumerator LoadIonCubeMaterial()
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.PrecursorIonCrystal);
            yield return task;

            GameObject ionCube = task.GetResult();
            ionCubeMaterial = ionCube.GetComponentInChildren<MeshRenderer>().material;
        }

        private static IEnumerator LoadPrecursorGlassMaterial()
        {
            IPrefabRequest request = PrefabDatabase.GetPrefabAsync("2b43dcb7-93b6-4b21-bd76-c362800bedd1");
            yield return request;

            request.TryGetPrefab(out GameObject glassPanel);
            precursorGlassMaterial = glassPanel.GetComponentInChildren<MeshRenderer>().material;
        }
    }
}