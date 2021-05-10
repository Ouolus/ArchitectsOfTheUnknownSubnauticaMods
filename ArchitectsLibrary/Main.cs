using System;
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
using ArchitectsLibrary.API;
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
        internal static List<TechType> TechTypesToAdd = new();
        
        internal static AssetBundle assetBundle;
        internal static AssetBundle fabBundle;
        
        static Assembly myAssembly = Assembly.GetExecutingAssembly();

        internal static string AssetsFolder = Path.Combine(Path.GetDirectoryName(myAssembly.Location), "Assets"); 
        
        const string assetBundleName = "architectslibrary";
        const string fabBundleName = "fabricatorassets";

        static PrecursorFabricator PrecursorFabricator;
        static PrecursorAlloyIngot precursorAlloy;
        static Emerald emerald;
        static DrillableEmerald drillableEmerald;
        const string encyKey_emerald = "EmeraldEncy";

        /// <summary>
        /// Please DO NOT use this Method, its meant for only QModManager's Initializations of this Mod.
        /// </summary>
        [Obsolete("Please DO NOT use this Method, its meant for only QModManager's Initializations of this Mod.", true)]
        [QModPatch]
        public static void Load()
        {
            QModManager.Utility.Logger.Log(QModManager.Utility.Logger.Level.Info, "ArchitectsLibrary started Patching.");
            
            DictionaryInit.PatchAllDictionaries();

            MaterialUtils.LoadMaterials();

            fabBundle = AssetBundle.LoadFromFile(Path.Combine(AssetsFolder, fabBundleName));
            assetBundle = AssetBundle.LoadFromFile(Path.Combine(AssetsFolder, assetBundleName));

            PatchItems();

            Harmony harmony = new Harmony($"ArchitectsOfTheUnknown_{myAssembly.GetName().Name}");

            VehiclePatches.Patch(harmony);

            //CreatorKit.SNCreatorKit.Entry();
            //MainMenuMusicPatches.Patch(harmony);
            
            QModManager.Utility.Logger.Log(QModManager.Utility.Logger.Level.Info, "ArchitectsLibrary successfully finished Patching!");
        }

        /// <summary>
        /// Please DO NOT use this Method, its meant for only QModManager's Initializations of this Mod.
        /// </summary>
        [Obsolete("Please DO NOT use this Method, its meant for only QModManager's Initializations of this Mod.", true)]
        [QModPostPatch]
        public static void PostLoad()
        {
            PrecursorFabricator = new();
            PrecursorFabricator.Patch();

            foreach (var techType in TechTypesToAdd)
            {
                if (techType == TechType.None)
                    continue;
                
                PrecursorFabricator.Root.AddCraftingNode(techType);
            }

            KnownTechHandler.SetAnalysisTechEntry(precursorAlloy.TechType, new List<TechType>() { precursorAlloy.TechType, PrecursorFabricator.TechType });
        }

        static void PatchItems()
        {
            emerald = new Emerald();
            emerald.Patch();
            AUHandler.EmeraldTechType = emerald.TechType;
            ItemUtils.PatchEncy(encyKey_emerald, "PlanetaryGeology", "Emerald Crystal", "A relatively tough, green mineral and a variation of beryl. Can be found in small numbers in deeper areas. While there are few known practical uses for this gemstone, a significant amount of this mineral can be observed in alien technology.\n\nAssessment: May have applications in the fabrication of alien technology");
            ItemUtils.MakeObjectScannable(emerald.TechType, encyKey_emerald, 3f);

            drillableEmerald = new DrillableEmerald();
            drillableEmerald.Patch();

            precursorAlloy =  new PrecursorAlloyIngot();
            precursorAlloy.Patch();
            PrecursorFabricatorService.SubscribeToFabricator(precursorAlloy.TechType);
            AUHandler.PrecursorAlloyIngotTechType = precursorAlloy.TechType;

            PrecursorFabricatorService.SubscribeToFabricator(TechType.PrecursorIonCrystal);
        }
    }
}