using System.Reflection;
using ArchitectsLibrary.Patches;
using HarmonyLib;
using QModManager.API.ModLoading;
using UnityEngine;
using CreatorKit.Patches;
using System.IO;
using ArchitectsLibrary.Items;
using SMLHelper.V2.Handlers;
using System.Collections.Generic;
using ArchitectsLibrary.Utility;

namespace ArchitectsLibrary
{
    /// <summary>
    /// Please DO NOT use this class, its meant for only QModManager's Initializations of this Mod.
    /// </summary>
    [QModCore]
    public static class Main
    {
        internal static AssetBundle assetBundle;
        
        static Assembly myAssembly = Assembly.GetExecutingAssembly();
        
        const string assetBundleName = "architectslibrary";
        static CraftTree.Type precursorFabricatorTree;
        
        static PrecursorAlloyIngot precursorAlloy;

        /// <summary>
        /// Please DO NOT use this Method, its meant for only QModManager's Initializations of this Mod.
        /// </summary>
        [QModPatch]
        public static void Load()
        {
            QModManager.Utility.Logger.Log(QModManager.Utility.Logger.Level.Info, "ArchitectsLibrary started Patching.");
            
            DictionaryInit.PatchAllDictionaries();

            MaterialUtils.LoadMaterials();

            assetBundle = AssetBundle.LoadFromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets", assetBundleName));

            PatchItems();

            Harmony harmony = new Harmony($"ArchitectsOfTheUnknown_{myAssembly.GetName().Name}");

            VehiclePatches.Patch(harmony);

            //CreatorKit.SNCreatorKit.Entry();
            //MainMenuMusicPatches.Patch(harmony);
            
            QModManager.Utility.Logger.Log(QModManager.Utility.Logger.Level.Info, "ArchitectsLibrary successfully finished Patching!");
        }

        

        static void PatchItems()
        {
            precursorAlloy =  new PrecursorAlloyIngot();
            precursorAlloy.Patch();
            KnownTechHandler.SetAnalysisTechEntry(precursorAlloy.TechType, new List<TechType>() { precursorAlloy.TechType });
        }
    }
}