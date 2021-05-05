using System.Reflection;
using ArchitectsLibrary.Patches;
using HarmonyLib;
using QModManager.API.ModLoading;
using QModManager.Utility;
using UnityEngine;
using System.Collections;
using CreatorKit.Patches;
using UWE;
using System.IO;
using ArchitectsLibrary.Items;
using SMLHelper.V2.Handlers;

namespace ArchitectsLibrary
{
    [QModCore]
    public static class Main
    {
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();
        public static Material ionCubeMaterial;
        public static Material precursorGlassMaterial;
        public static AssetBundle assetBundle;
        public const string assetBundleName = "architectslibrary";
        public static CraftTree.Type precursorFabricatorTree;

        public static PrecursorAlloyIngot precursorAlloy = new PrecursorAlloyIngot();

        [QModPatch]
        public static void Load()
        {
            QModManager.Utility.Logger.Log(QModManager.Utility.Logger.Level.Info, "ArchitectsLibrary started Patching.");
            
            Initializer.PatchAllDictionaries();      

            QModManager.Utility.Logger.Log(QModManager.Utility.Logger.Level.Info, "ArchitectsLibrary successfully finished Patching!");

            CoroutineHost.StartCoroutine(LoadIonCubeMaterial());
            CoroutineHost.StartCoroutine(LoadPrecursorGlassMaterial());

            assetBundle = AssetBundle.LoadFromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets", assetBundleName));

            CraftTreeHandler.CreateCustomCraftTreeAndType("PrecursorFabricator", out precursorFabricatorTree);
            PatchItems();

            Harmony harmony = new Harmony($"ArchitectsOfTheUnknown_{myAssembly.GetName().Name}");

            VehiclePatches.Patch(harmony);

            //CreatorKit.SNCreatorKit.Entry();
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

        private static void PatchItems()
        {
            precursorAlloy.Patch();
        }
    }
}