using QModManager.API.ModLoading;
using HarmonyLib;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ArchitectsLibrary.API;
using ArchitectsLibrary.Items;
using ArchitectsLibrary.Patches;
using ArchitectsLibrary.Handlers;
using ArchitectsLibrary.Utility;
using CreatorKit.Patches;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Crafting;
using UnityEngine;

namespace ArchitectsLibrary
{
    /// <summary>
    /// Please DO NOT use this class, its meant for only QModManager's Initializations of this Mod.
    /// </summary>
    [QModCore]
    public static class Main
    {
        internal static List<PrecursorFabricatorEntry> PrecursorFabricatorEntriesToAdd = new();
        
        internal static AssetBundle assetBundle;
        internal static AssetBundle fabBundle;
        
        internal static Atlas.Sprite background;
        internal static Atlas.Sprite backgroundHovered;
        
        static Assembly myAssembly = Assembly.GetExecutingAssembly();

        internal static string AssetsFolder = Path.Combine(Path.GetDirectoryName(myAssembly.Location), "Assets"); 
        
        const string assetBundleName = "architectslibrary";
        const string fabBundleName = "fabricatorassets";

        internal static PrecursorFabricator PrecursorFabricator;
        static PrecursorAlloyIngot precursorAlloy;
        static Emerald emerald;
        static DrillableEmerald drillableEmerald;
        static Sapphire sapphire;
        static DrillableSapphire drillableSapphire;
        static ReinforcedGlass reinforcedGlass;
        static AlienCompositeGlass alienCompositeGlass;
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
            
            background = new Atlas.Sprite(assetBundle.LoadAsset<Sprite>("Background"));
            backgroundHovered = new Atlas.Sprite(assetBundle.LoadAsset<Sprite>("BackgroundHovered"));

            UWE.CoroutineHost.StartCoroutine(FixIonCubeCraftingCoroutine());

            PatchItems();

            Harmony harmony = new Harmony($"ArchitectsOfTheUnknown_{myAssembly.GetName().Name}");

            VehiclePatches.Patch(harmony);

            //CreatorKit.SNCreatorKit.Entry();
            //MainMenuMusicPatches.Patch(harmony);
            
            CraftingMenuPatches.Patch(harmony);
            
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

            Atlas.Sprite tabSprite = SpriteManager.defaultSprite;
            PrecursorFabricator.Root.AddTabNode("AlienMaterials", "Alien Materials", tabSprite);
            PrecursorFabricator.Root.AddTabNode("AlienEquipment", "Equipment", tabSprite);
            PrecursorFabricator.Root.AddTabNode("AlienDevices", "Devices", tabSprite);
            PrecursorFabricator.Root.AddTabNode("AlienUpgrades", "Advanced Upgrade Modules", tabSprite);
            PrecursorFabricator.Root.AddTabNode("AlienDecorations", "Decoration Items", tabSprite);
            PrecursorFabricator.Root.AddTabNode("AlienEggs", "Eggs", tabSprite);

            foreach (var entry in PrecursorFabricatorEntriesToAdd)
            {
                if (entry.tab == PrecursorFabricatorTab.None)
                    continue;
                if (entry.techType == TechType.None) // Safety check
                    continue;
                
                PrecursorFabricator.Root.GetTabNode(PrecursorFabricatorService.TabToNameID(entry.tab)).AddCraftingNode(entry.techType);
            }

            KnownTechHandler.SetAnalysisTechEntry(precursorAlloy.TechType, new List<TechType>() { precursorAlloy.TechType, PrecursorFabricator.TechType, TechType.PrecursorIonCrystal, alienCompositeGlass.TechType });
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
            PrecursorFabricatorService.SubscribeToFabricator(precursorAlloy.TechType, PrecursorFabricatorTab.Materials);
            AUHandler.PrecursorAlloyIngotTechType = precursorAlloy.TechType;
            CraftData.pickupSoundList.Add(precursorAlloy.TechType, "event:/loot/pickup_precursorioncrystal");

            sapphire = new Sapphire();
            sapphire.Patch();
            AUHandler.SapphireTechType = sapphire.TechType;
            CraftData.pickupSoundList.Add(sapphire.TechType, "event:/loot/pickup_precursorioncrystal");

            drillableSapphire = new DrillableSapphire();
            drillableSapphire.Patch();
            AUHandler.DrillableSapphireTechType = drillableSapphire.TechType;

            reinforcedGlass = new ReinforcedGlass();
            reinforcedGlass.Patch();
            AUHandler.ReinforcedGlassTechType = reinforcedGlass.TechType;
            CraftData.pickupSoundList.Add(reinforcedGlass.TechType, "event:/loot/pickup_glass");

            alienCompositeGlass = new AlienCompositeGlass();
            alienCompositeGlass.Patch();
            PrecursorFabricatorService.SubscribeToFabricator(alienCompositeGlass.TechType, PrecursorFabricatorTab.Materials);
            AUHandler.AlienCompositeGlassTechType = alienCompositeGlass.TechType;
            CraftData.pickupSoundList.Add(alienCompositeGlass.TechType, "event:/loot/pickup_glass");

            PrecursorFabricatorService.SubscribeToFabricator(TechType.PrecursorIonCrystal, PrecursorFabricatorTab.Materials);
            CraftDataHandler.SetTechData(TechType.PrecursorIonCrystal, new TechData {craftAmount = 1, Ingredients = new List<Ingredient>() { new Ingredient(emerald.TechType, 2)} });
            CraftDataHandler.SetCraftingTime(TechType.PrecursorIonCrystal, 30f);
            CraftData.groups[TechGroup.Resources][TechCategory.AdvancedMaterials].Add(TechType.PrecursorIonCrystal);
        }
    }
}