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
using ArchitectsLibrary.Buildables;
using ArchitectsLibrary.Config;

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
        static TechType alienTechnologyMasterTech;
        static PrecursorAlloyIngot precursorAlloy;
        static Emerald emerald;
        static DrillableEmerald drillableEmerald;
        static Sapphire sapphire;
        static DrillableSapphire drillableSapphire;
        static RedBeryl redBeryl;
        static DrillableRedBeryl drillableRedBeryl;
        static ReinforcedGlass reinforcedGlass;
        static AlienCompositeGlass alienCompositeGlass;
        static AotuPoster aotuPoster;
        static Morganite morganite;
        static DrillableMorganite drillableMorganite;
        static Electricube electricube;
        static RedIonCube redIonCube;
        static Cobalt cobalt;
        static DrillableCobalt drillableCobalt;
        static CobaltIngot cobaltIngot;

        static BuildableColumn buildableColumn;
        static BuildableArchway buildableArchway;
        static BuildablePlatform buildablePlatform;
        static BuildableLight1 buildableLight1;
        static BuildableLight2 buildableLight2;
        static BuildableLight3 buildableLight3;
        static BuildableDissectionTank buildableDissectionTank;
        static BuildableRelicTank buildableRelicTank;
        static BuildableLargeRelicTank buildableLargeRelicTank;
        static BuildableItemPedestal buildableItemPedestal;
        static BuildableAlienRobot buildableAlienRobot;
        static BuildableWarper buildableWarper;
        static BuildableInfoPanel buildableInfoPanel;
        static BuildableMicroscope buildableMicroscope;
        static BuildableSonicDeterrent buildableSonicDeterrent;
        static BuildableColumnSmall buildableColumnSmall;
        static BuildableLight4 buildableLight4;
        static BuildableLight5 buildableLight5;
        static BuildablePedestal buildablePedestal;
        static BuildablePedestalLarge buildablePedestalLarge;

        static internal AchievementData achievementData;

        const string encyKey_emerald = "EmeraldEncy";

        private const string ionCubePickupSound = "event:/loot/pickup_precursorioncrystal";

        /// <summary>
        /// Please DO NOT use this Method, its meant for only QModManager's Initializations of this Mod.
        /// </summary>
        [Obsolete("Please DO NOT use this Method, its meant for only QModManager's Initializations of this Mod.", true)]
        [QModPrePatch]
        public static void PreLoad()
        {
            fabBundle = AssetBundle.LoadFromFile(Path.Combine(AssetsFolder, fabBundleName));
            assetBundle = AssetBundle.LoadFromFile(Path.Combine(AssetsFolder, assetBundleName));
            
            alienTechnologyMasterTech = TechTypeHandler.AddTechType("AlienMasterTech", "Alien Technology", "Advanced technology used by an advanced race.", false);
            AUHandler.AlienTechnologyMasterTech = alienTechnologyMasterTech;
            
            PatchMinerals();

            achievementData = new AchievementData();
        }

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

            background = new Atlas.Sprite(assetBundle.LoadAsset<Sprite>("Background"));
            backgroundHovered = new Atlas.Sprite(assetBundle.LoadAsset<Sprite>("BackgroundHovered"));

            UWE.CoroutineHost.StartCoroutine(FixIonCubeCraftingCoroutine());

            PatchItems();
            
            PatchBuildables();

            achievementData.Load();

            PatchAchievements();

            Harmony harmony = new Harmony($"ArchitectsOfTheUnknown_{myAssembly.GetName().Name}");

            VehiclePatches.Patch(harmony);
            InspectOnFirstPickupPatches.Patch(harmony);
            //CreatorKit.SNCreatorKit.Entry();
            //MainMenuMusicPatches.Patch(harmony);
            CraftingMenuPatches.Patch(harmony);


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

            PrecursorFabricator.Root.AddTabNode("AlienMaterials", "Alien Materials", assetBundle.LoadAsset<Sprite>("Tab_AlienMaterials"));
            PrecursorFabricator.Root.AddTabNode("AlienEquipment", "Equipment", assetBundle.LoadAsset<Sprite>("Tab_Equipment"));
            PrecursorFabricator.Root.AddTabNode("AlienDevices", "Devices", assetBundle.LoadAsset<Sprite>("Tab_Devices"));
            PrecursorFabricator.Root.AddTabNode("AlienUpgrades", "Advanced Upgrade Modules", assetBundle.LoadAsset<Sprite>("Tab_Upgrades"));
            PrecursorFabricator.Root.AddTabNode("AlienDecorations", "Decoration Items", assetBundle.LoadAsset<Sprite>("Tab_Decorations"));
            PrecursorFabricator.Root.AddTabNode("AlienEggs", "Eggs", assetBundle.LoadAsset<Sprite>("Tab_Eggs"));

            foreach (var entry in PrecursorFabricatorEntriesToAdd)
            {
                if (entry.tab == PrecursorFabricatorTab.None)
                    continue;
                if (entry.techType == TechType.None) // Safety check
                    continue;
                
                PrecursorFabricator.Root.GetTabNode(PrecursorFabricatorService.TabToNameID(entry.tab)).AddCraftingNode(entry.techType);
            }

            KnownTechHandler.SetAnalysisTechEntry(alienTechnologyMasterTech, new List<TechType>() { PrecursorFabricator.TechType, TechType.PrecursorIonCrystal, alienCompositeGlass.TechType, reinforcedGlass.TechType, electricube.TechType, redIonCube.TechType });
            KnownTechHandler.SetAnalysisTechEntry(precursorAlloy.TechType, new List<TechType>() { precursorAlloy.TechType });
        }
        
        static IEnumerator FixIonCubeCraftingCoroutine()
        {
            var task = CraftData.GetPrefabForTechTypeAsync(TechType.PrecursorIonCrystal);
            yield return task;
            var prefab = task.GetResult();
            var vfxFabricating = prefab.GetComponentInChildren<MeshRenderer>(true).gameObject.AddComponent<VFXFabricating>();
            vfxFabricating.localMinY = -0.25f;
            vfxFabricating.localMaxY = 0.44f;
            vfxFabricating.posOffset = new Vector3(0f, -0.04f, 0.1f);
            vfxFabricating.eulerOffset = new Vector3(270f, 0f, 0f);
            vfxFabricating.scaleFactor = 1.5f;
        }

        static void PatchAchievements()
        {
            AchievementServices.RegisterAchievement("BuildPrecursorFabricator", "Architect", assetBundle.LoadAsset<Sprite>("AchievementIcon_Architect"), "This achievement is locked.", "Constructed a Precursor Fabricator.", true, 1);
        }

        static void PatchMinerals()
        {
            emerald = new Emerald();
            emerald.Patch();
            AUHandler.EmeraldTechType = emerald.TechType;
            ItemUtils.PatchEncy(encyKey_emerald, "PlanetaryGeology", "Emerald Crystal", "A relatively tough, green mineral and a variation of beryl. Can be found in small amounts in deep biomes, and in large deposits amongst areas with extensive sand dunes. While there are few known practical uses for this gemstone, a significant amount of this mineral has been observed in alien technology.\n\nAssessment: May have applications in the fabrication of alien technology");
            ItemUtils.MakeObjectScannable(emerald.TechType, encyKey_emerald, 3f);
            CraftData.pickupSoundList.Add(emerald.TechType, ionCubePickupSound);

            drillableEmerald = new DrillableEmerald();
            drillableEmerald.Patch();
            AUHandler.DrillableEmeraldTechType = drillableEmerald.TechType;
            ItemUtils.MakeObjectScannable(drillableEmerald.TechType, encyKey_emerald, 5f);

            cobalt = new Cobalt();
            cobalt.Patch();
            AUHandler.CobaltTechType = cobalt.TechType;
            CraftData.pickupSoundList.Add(cobalt.TechType, ionCubePickupSound);

            drillableCobalt = new DrillableCobalt();
            drillableCobalt.Patch();
            AUHandler.DrillableCobaltTechType = drillableCobalt.TechType;

            cobaltIngot = new CobaltIngot();
            cobaltIngot.Patch();
            AUHandler.CobaltIngotTechType = cobaltIngot.TechType;
            CraftData.pickupSoundList.Add(cobaltIngot.TechType, ionCubePickupSound);

            precursorAlloy =  new PrecursorAlloyIngot();
            precursorAlloy.Patch();
            PrecursorFabricatorService.SubscribeToFabricator(precursorAlloy.TechType, PrecursorFabricatorTab.Materials);
            AUHandler.PrecursorAlloyIngotTechType = precursorAlloy.TechType;
            CraftData.pickupSoundList.Add(precursorAlloy.TechType, ionCubePickupSound);

            sapphire = new Sapphire();
            sapphire.Patch();
            AUHandler.SapphireTechType = sapphire.TechType;
            CraftData.pickupSoundList.Add(sapphire.TechType, ionCubePickupSound);

            drillableSapphire = new DrillableSapphire();
            drillableSapphire.Patch();
            AUHandler.DrillableSapphireTechType = drillableSapphire.TechType;

            redBeryl = new RedBeryl();
            redBeryl.Patch();
            AUHandler.RedBerylTechType = redBeryl.TechType;
            CraftData.pickupSoundList.Add(redBeryl.TechType, ionCubePickupSound);

            drillableRedBeryl = new DrillableRedBeryl();
            drillableRedBeryl.Patch();
            AUHandler.DrillableRedBerylTechType = drillableRedBeryl.TechType;

            morganite = new Morganite();
            morganite.Patch();
            AUHandler.MorganiteTechType = morganite.TechType;
            CraftData.pickupSoundList.Add(morganite.TechType, "event:/loot/pickup_glass");

            drillableMorganite = new DrillableMorganite();
            drillableMorganite.Patch();
            AUHandler.DrillableMorganiteTechType = drillableMorganite.TechType;

            electricube = new Electricube();
            electricube.Patch();
            AUHandler.ElectricubeTechType = electricube.TechType;
            CraftData.pickupSoundList.Add(electricube.TechType, ionCubePickupSound);
            PrecursorFabricatorService.SubscribeToFabricator(electricube.TechType, PrecursorFabricatorTab.Materials);

            redIonCube = new RedIonCube();
            redIonCube.Patch();
            AUHandler.RedIonCubeTechType = redIonCube.TechType;
            CraftData.pickupSoundList.Add(redIonCube.TechType, ionCubePickupSound);
            PrecursorFabricatorService.SubscribeToFabricator(redIonCube.TechType, PrecursorFabricatorTab.Materials);

            reinforcedGlass = new ReinforcedGlass();
            reinforcedGlass.Patch();
            AUHandler.ReinforcedGlassTechType = reinforcedGlass.TechType;
            CraftData.pickupSoundList.Add(reinforcedGlass.TechType, "event:/loot/pickup_glass");

            alienCompositeGlass = new AlienCompositeGlass();
            alienCompositeGlass.Patch();
            PrecursorFabricatorService.SubscribeToFabricator(alienCompositeGlass.TechType, PrecursorFabricatorTab.Materials);
            AUHandler.AlienCompositeGlassTechType = alienCompositeGlass.TechType;
            CraftData.pickupSoundList.Add(alienCompositeGlass.TechType, "event:/loot/pickup_glass");
        }

        static void PatchItems()
        {
            aotuPoster = new AotuPoster();
            aotuPoster.Patch();

            PrecursorFabricatorService.SubscribeToFabricator(TechType.PrecursorIonCrystal, PrecursorFabricatorTab.Materials);
            CraftDataHandler.SetTechData(TechType.PrecursorIonCrystal, new TechData {craftAmount = 1, Ingredients = new List<Ingredient>() { new Ingredient(emerald.TechType, 2)} });
            CraftDataHandler.SetCraftingTime(TechType.PrecursorIonCrystal, 30f);
            CraftData.groups[TechGroup.Resources][TechCategory.AdvancedMaterials].Add(TechType.PrecursorIonCrystal);

            PrecursorFabricatorService.SubscribeToFabricator(TechType.PrecursorIonBattery, PrecursorFabricatorTab.Devices);
            PrecursorFabricatorService.SubscribeToFabricator(TechType.PrecursorIonPowerCell, PrecursorFabricatorTab.Devices);
        }

        static void PatchBuildables()
        {
            //exterior only
            buildableColumn = new BuildableColumn();
            buildableColumn.Patch();

            buildableArchway = new BuildableArchway();
            buildableArchway.Patch();

            buildableSonicDeterrent = new BuildableSonicDeterrent();
            buildableSonicDeterrent.Patch();

            //exterior and interior
            buildablePlatform = new BuildablePlatform();
            buildablePlatform.Patch();

            buildableRelicTank = new BuildableRelicTank();
            buildableRelicTank.Patch();

            buildableLargeRelicTank = new BuildableLargeRelicTank();
            buildableLargeRelicTank.Patch();

            buildableItemPedestal = new BuildableItemPedestal();
            buildableItemPedestal.Patch();

            buildableDissectionTank = new BuildableDissectionTank();
            buildableDissectionTank.Patch();

            /*buildableAlienRobot = new BuildableAlienRobot();
            buildableAlienRobot.Patch();

            buildableWarper = new BuildableWarper();
            buildableWarper.Patch();*/

            buildableInfoPanel = new BuildableInfoPanel();
            buildableInfoPanel.Patch();

            buildableMicroscope = new BuildableMicroscope();
            buildableMicroscope.Patch();

            buildableColumnSmall = new BuildableColumnSmall();
            buildableColumnSmall.Patch();

            buildablePedestal = new BuildablePedestal();
            buildablePedestal.Patch();

            buildablePedestalLarge = new BuildablePedestalLarge();
            buildablePedestalLarge.Patch();

            buildableLight1 = new BuildableLight1();
            buildableLight1.Patch();

            buildableLight2 = new BuildableLight2();
            buildableLight2.Patch();

            buildableLight3 = new BuildableLight3();
            buildableLight3.Patch();

            buildableLight4 = new BuildableLight4();
            buildableLight4.Patch();

            buildableLight5 = new BuildableLight5();
            buildableLight5.Patch();

        }
    }
}
