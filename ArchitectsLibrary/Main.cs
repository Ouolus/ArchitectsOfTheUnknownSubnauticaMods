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
using ArchitectsLibrary.Handlers;
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
        
        static PrecursorAlloyIngot precursorAlloy;
        static Emerald emerald;
        const string encyKey_emerald = "EmeraldEncy";

        /// <summary>
        /// Please DO NOT use this Method, its meant for only QModManager's Initializations of this Mod.
        /// </summary>
        [QModPatch]
        public static void Load()
        {
            QModManager.Utility.Logger.Log(QModManager.Utility.Logger.Level.Info, "ArchitectsLibrary started Patching.");
            
            DictionaryInit.PatchAllDictionaries();

            MaterialUtils.LoadMaterials();

            assetBundle = AssetBundle.LoadFromFile(Path.Combine(Path.GetDirectoryName(myAssembly.Location), "Assets", assetBundleName));

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
            AUHandler.PrecursorAlloyIngotTechType = precursorAlloy.TechType;

            emerald = new Emerald();
            emerald.Patch();
            AUHandler.EmeraldTechType = emerald.TechType;
            ItemUtils.PatchEncy(encyKey_emerald, "PlanetaryGeology", "Emerald Crystal", "A rare, green mineral and a variation of beryl. Can be found in small numbers in deeper biomes. While there are few known practical uses for this gemstone, a significant amount of this mineral can be observed in alien technology.\n\nAssessment: May have applications in the fabrication of alien technology.");
            ItemUtils.MakeObjectScannable(emerald.TechType, encyKey_emerald, 3f);
        }
    }
}