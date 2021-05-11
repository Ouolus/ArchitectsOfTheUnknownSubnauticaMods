using System;
using System.Collections;
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
using SMLHelper.V2.Crafting;

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
        static Sapphire sapphire;
        static ReinforcedGlass reinforcedGlass;
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

            UWE.CoroutineHost.StartCoroutine(FixIonCubeCraftingCoroutine());

            PatchItems();

            Harmony harmony = new Harmony($"ArchitectsOfTheUnknown_{myAssembly.GetName().Name}");

            VehiclePatches.Patch(harmony);

            //CreatorKit.SNCreatorKit.Entry();
            //MainMenuMusicPatches.Patch(harmony);
            
            QModManager.Utility.Logger.Log(QModManager.Utility.Logger.Level.Info, "ArchitectsLibrary successfully finished Patching!");
        }

        static IEnumerator FixIonCubeCraftingCoroutine()
        {
            var task = CraftData.GetPrefabForTechTypeAsync(TechType.PrecursorIonCrystal);
            yield return task;
            var prefab = task.GetResult();
            var vfxFabricating = prefab.GetComponentInChildren<MeshRenderer>(true).gameObject.AddComponent<VFXFabricating>();
            vfxFabricating.localMinY = -0.12f;
            vfxFabricating.localMaxY = 0.34f;
            vfxFabricating.posOffset = new Vector3(0f, -0.04f, 0.1f);
            vfxFabricating.eulerOffset = new Vector3(270f, 0f, 0f);
            vfxFabricating.scaleFactor = 1.5f;
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

            KnownTechHandler.SetAnalysisTechEntry(precursorAlloy.TechType, new List<TechType>() { precursorAlloy.TechType, PrecursorFabricator.TechType, TechType.PrecursorIonCrystal, reinforcedGlass.TechType });
        }

        static void PatchItems()
        {
            emerald = new Emerald();
            emerald.Patch();
            AUHandler.EmeraldTechType = emerald.TechType;
            ItemUtils.PatchEncy(encyKey_emerald, "PlanetaryGeology", "Emerald Crystal", "A relatively tough, green mineral and a variation of beryl. Can be found in small numbers in deeper areas. While there are few known practical uses for this gemstone, a significant amount of this mineral can be observed in alien technology.\n\nAssessment: May have applications in the fabrication of alien technology");
            ItemUtils.MakeObjectScannable(emerald.TechType, encyKey_emerald, 3f);
            CraftData.pickupSoundList.Add(emerald.TechType, "event:/loot/pickup_precursorioncrystal");

            drillableEmerald = new DrillableEmerald();
            drillableEmerald.Patch();
            AUHandler.DrillableEmeraldTechType = drillableEmerald.TechType;

            precursorAlloy =  new PrecursorAlloyIngot();
            precursorAlloy.Patch();
            PrecursorFabricatorService.SubscribeToFabricator(precursorAlloy.TechType);
            AUHandler.PrecursorAlloyIngotTechType = precursorAlloy.TechType;
            CraftData.pickupSoundList.Add(precursorAlloy.TechType, "event:/loot/pickup_precursorioncrystal");

            sapphire = new Sapphire();
            sapphire.Patch();
            AUHandler.SapphireTechType = sapphire.TechType;
            CraftData.pickupSoundList.Add(sapphire.TechType, "event:/loot/pickup_precursorioncrystal");

            reinforcedGlass = new ReinforcedGlass();
            reinforcedGlass.Patch();
            PrecursorFabricatorService.SubscribeToFabricator(reinforcedGlass.TechType);
            AUHandler.ReinforcedGlassTechType = reinforcedGlass.TechType;
            CraftData.pickupSoundList.Add(reinforcedGlass.TechType, "event:/loot/pickup_glass");

            PrecursorFabricatorService.SubscribeToFabricator(TechType.PrecursorIonCrystal);
            CraftDataHandler.SetTechData(TechType.PrecursorIonCrystal, new TechData {craftAmount = 1, Ingredients = new List<Ingredient>() { new Ingredient(emerald.TechType, 2)} });
            CraftDataHandler.SetCraftingTime(TechType.PrecursorIonCrystal, 30f);
            CraftData.groups[TechGroup.Resources][TechCategory.AdvancedMaterials].Add(TechType.PrecursorIonCrystal);
        }
    }
}