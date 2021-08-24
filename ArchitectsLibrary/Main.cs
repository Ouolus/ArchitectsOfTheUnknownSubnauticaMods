using SMLHelper.V2.Assets;

namespace ArchitectsLibrary
{
    using QModManager.API.ModLoading;
    using HarmonyLib;
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Reflection;
    using API;
    using Buildables;
    using Handlers;
    using Items;
    using Items.AdvancedMaterials;
    using Items.Cubes;
    using Items.Drillables;
    using Items.Minerals;
    using Items.VanillaPrefabPatching;
    using Patches;
    using Utility;
    using SMLHelper.V2.Handlers;
    using SMLHelper.V2.Crafting;
    using UnityEngine;
    using Configuration;
    
    /// <summary>
    /// Please DO NOT use this class, its meant for only QModManager's Initializations of this Mod.
    /// </summary>
    [QModCore]
    public static class Main
    {
        internal static List<TechType> DecorationTechs = new();
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

        internal static AchievementData achievementData;
        internal static Config Config { get; private set; }
        
        internal static TechGroup DecorationGroup { get; private set; }
        internal static TechCategory DecorationCategory { get; private set; }
        internal static CraftData.BackgroundType AlienBackground { get; private set; }

        internal const string encyKey_emerald = "EmeraldEncy";

        internal const string ionCubePickupSound = "event:/loot/pickup_precursorioncrystal";

        /// <summary>
        /// Please DO NOT use this Method, its meant for only QModManager's Initializations of this Mod.
        /// </summary>
        [Obsolete("Please DO NOT use this Method, its meant for only QModManager's Initializations of this Mod.", true)]
        [QModPrePatch]
        public static void PreLoad()
        {
            LanguageSystem.RegisterLocalization();
            
            fabBundle = AssetBundle.LoadFromFile(Path.Combine(AssetsFolder, fabBundleName));
            assetBundle = AssetBundle.LoadFromFile(Path.Combine(AssetsFolder, assetBundleName));
            
            alienTechnologyMasterTech = TechTypeHandler.AddTechType("AlienMasterTech", "Alien Technology", "Advanced technology used by an advanced race.", false);
            AUHandler.AlienTechnologyMasterTech = alienTechnologyMasterTech;
            
            PatchMinerals();

            PatchItems();

            FixDisplayCaseItems();

            PatchBuildables();

            achievementData = new AchievementData();
            Config = OptionsPanelHandler.RegisterModOptions<Config>();
        }

        /// <summary>
        /// Please DO NOT use this Method, its meant for only QModManager's Initializations of this Mod.
        /// </summary>
        [Obsolete("Please DO NOT use this Method, its meant for only QModManager's Initializations of this Mod.", true)]
        [QModPatch]
        public static void Load()
        {
            QModManager.Utility.Logger.Log(QModManager.Utility.Logger.Level.Info, "ArchitectsLibrary started Patching.");

            DecorationGroup = TechGroupHandler.Main.AddTechGroup("AlienDecoration", "Alien Decorations");
            DecorationCategory = TechCategoryHandler.Main.AddTechCategory("AlienDecoration", "Alien Decorations");

            TechCategoryHandler.Main.TryRegisterTechCategoryToTechGroup(DecorationGroup, DecorationCategory);
            
            SpriteHandler.RegisterSprite(SpriteManager.Group.Tab, "groupAlienDecoration", assetBundle.LoadAsset<Sprite>("AotU"));

            DictionaryInit.PatchAllDictionaries();

            MaterialUtils.LoadMaterials();

            background = new Atlas.Sprite(assetBundle.LoadAsset<Sprite>("Background"));
            backgroundHovered = new Atlas.Sprite(assetBundle.LoadAsset<Sprite>("BackgroundHovered"));

            AlienBackground = BackgroundTypeHandler.AddBackgroundType("AlienBackground", background);
            
            achievementData.Load();

            PatchAchievements();

            List<VanillaPrefab> prefabPatchings = new() { new PrecursorIonCrystal(), new PrecursorIonBattery(), new PrecursorIonPowerCell() };
            
            prefabPatchings.ForEach(PrefabHandler.RegisterPrefab);

            Harmony harmony = new Harmony($"ArchitectsOfTheUnknown_{myAssembly.GetName().Name}");

            VehiclePatches.Patch(harmony);
            InspectOnFirstPickupPatches.Patch(harmony);
            //CreatorKit.SNCreatorKit.Entry();
            //MainMenuMusicPatches.Patch(harmony);
            CraftingMenuPatches.Patch(harmony);
            WaterParkPatches.Patch(harmony);
            BuilderPatches.Patch(harmony);
            uGUI_InventoryTabPatches.Patch(harmony);
            uGUI_BuilderMenuPatches.Patch(harmony);
            uGUI_ItemSelectorPatches.Patch(harmony);
            LanguagePatches.Patch(harmony);

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

            PrecursorFabricator.Root.AddTabNode("AlienMaterials", Language.main.Get("PrecursorFabricatorMenu_AlienMaterials"), assetBundle.LoadAsset<Sprite>("Tab_AlienMaterials"));
            PrecursorFabricator.Root.AddTabNode("AlienEquipment", Language.main.Get("PrecursorFabricatorMenu_AlienEquipment"), assetBundle.LoadAsset<Sprite>("Tab_Equipment"));
            PrecursorFabricator.Root.AddTabNode("AlienDevices", Language.main.Get("PrecursorFabricatorMenu_AlienDevices"), assetBundle.LoadAsset<Sprite>("Tab_Devices"));
            PrecursorFabricator.Root.AddTabNode("AlienUpgrades", Language.main.Get("PrecursorFabricatorMenu_AlienUpgrades"), assetBundle.LoadAsset<Sprite>("Tab_Upgrades"));
            PrecursorFabricator.Root.AddTabNode("AlienDecorations", Language.main.Get("PrecursorFabricatorMenu_AlienDecorations"), assetBundle.LoadAsset<Sprite>("Tab_Decorations"));
            PrecursorFabricator.Root.AddTabNode("AlienEggs", Language.main.Get("PrecursorFabricatorMenu_AlienEggs"), assetBundle.LoadAsset<Sprite>("Tab_Eggs"));

            foreach (var entry in PrecursorFabricatorEntriesToAdd)
            {
                if (entry.tab == PrecursorFabricatorTab.None)
                    continue;
                if (entry.techType == TechType.None) // Safety check
                    continue;
                
                PrecursorFabricator.Root.GetTabNode(PrecursorFabricatorService.TabToNameID(entry.tab)).AddCraftingNode(entry.techType);
            }

            KnownTechHandler.SetAnalysisTechEntry(alienTechnologyMasterTech, new List<TechType>() { PrecursorFabricator.TechType, TechType.PrecursorIonCrystal, AUHandler.AlienCompositeGlassTechType, AUHandler.ReinforcedGlassTechType, AUHandler.ElectricubeTechType, AUHandler.RedIonCubeTechType });
        }

        internal static void IonCubeCraftModelFix(GameObject prefab)
        {
            var vfxFabricating = prefab.GetComponentInChildren<MeshRenderer>(true).gameObject.EnsureComponent<VFXFabricating>();
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
            List<ReskinSpawnable> minerals = new() { new Cobalt(), new Emerald(), new Morganite(), new RedBeryl(), new Sapphire() };
            minerals.ForEach(mineral => mineral.Patch());
            
            List<ReskinSpawnable> drillables = new() { new DrillableCobalt(), new DrillableEmerald(), new DrillableMorganite(), new DrillableRedBeryl(), new DrillableSapphire() };
            drillables.ForEach(drillable => drillable.Patch());
        }

        static void PatchItems()
        {
            List<Spawnable> advancedMaterials = new() { new ReinforcedGlass(), new AlienCompositeGlass(), new CobaltIngot(), new PrecursorAlloyIngot(), new AotuPoster() };
            advancedMaterials.ForEach(one => one.Patch());
            
            List<PrecursorIonCube> precursorCubes = new() { new Electricube(), new OmegaCube(), new RedIonCube() };
            precursorCubes.ForEach(cube => cube.Patch());

            PrecursorFabricatorService.SubscribeToFabricator(TechType.PrecursorIonCrystal, PrecursorFabricatorTab.Materials);
            CraftDataHandler.SetTechData(TechType.PrecursorIonCrystal, new TechData {craftAmount = 1, Ingredients = new List<Ingredient>() { new Ingredient(AUHandler.EmeraldTechType, 2)} });
            CraftDataHandler.SetCraftingTime(TechType.PrecursorIonCrystal, 30f);
            CraftData.groups[TechGroup.Resources][TechCategory.AdvancedMaterials].Add(TechType.PrecursorIonCrystal);

            PrecursorFabricatorService.SubscribeToFabricator(TechType.PrecursorIonBattery, PrecursorFabricatorTab.Devices);
            PrecursorFabricatorService.SubscribeToFabricator(TechType.PrecursorIonPowerCell, PrecursorFabricatorTab.Devices);
        }

        static void FixDisplayCaseItems()
        {
            TechType[] resourcesToFix = new TechType[] { TechType.UraniniteCrystal, TechType.Diamond, TechType.Copper, TechType.AluminumOxide, TechType.Kyanite, AUHandler.EmeraldTechType, AUHandler.RedBerylTechType, AUHandler.SapphireTechType, TechType.Silver, TechType.PrecursorIonCrystal, AUHandler.RedIonCubeTechType, AUHandler.ElectricubeTechType, TechType.Gold};
            FixArrayOfDisplayCaseItems(resourcesToFix, new Vector3(0f, -0.25f, 0f));
            DisplayCaseServices.SetScaleInSpecimenCase(AUHandler.EmeraldTechType, 0.4f);
            DisplayCaseServices.SetScaleInSpecimenCase(TechType.Kyanite, 0.4f);
            DisplayCaseServices.SetOffset(TechType.StasisRifle, Vector3.up * -0.25f);
            DisplayCaseServices.SetRotationInRelicTank(TechType.StasisRifle, new Vector3(-90f, 0f, 0f));
            DisplayCaseServices.SetRotationInRelicTank(TechType.LaserCutter, new Vector3(-90f, 0f, 0f));
            DisplayCaseServices.SetRotationInRelicTank(TechType.PrecursorKey_Purple, new Vector3(90f, 0f, 0f));
            DisplayCaseServices.SetRotationInRelicTank(TechType.PrecursorKey_Orange, new Vector3(90f, 0f, 0f));
            DisplayCaseServices.SetRotationInRelicTank(TechType.PrecursorKey_Blue, new Vector3(90f, 0f, 0f));
            DisplayCaseServices.SetRotationInRelicTank(TechType.PrecursorKey_White, new Vector3(90f, 0f, 0f));
            DisplayCaseServices.SetRotationInRelicTank(TechType.PrecursorKey_Red, new Vector3(90f, 0f, 0f));
            DisplayCaseServices.SetScaleInRelicTank(TechType.WiringKit, 0.8f);
            DisplayCaseServices.SetScaleInRelicTank(TechType.AdvancedWiringKit, 0.8f);
        }

        static void FixArrayOfDisplayCaseItems(TechType[] techTypes, Vector3 newOffset)
        {
            for(int i = 0; i < techTypes.Length; i++)
            {
                DisplayCaseServices.SetOffset(techTypes[i], newOffset);
            }
        }

        static void PatchBuildables()
        {
            List<GenericPrecursorDecoration> buildables = new()
            {
                // exterior only
                new BuildableColumn(), new BuildableArchway(), new BuildableSonicDeterrent(),
            
                // exterior and interior
                new BuildablePlatform(), new BuildableRelicTank(), new BuildableLargeRelicTank(), new BuildableItemPedestal(), new BuildableSpecimenCases(), new BuildableDissectionTank(),
                /*new BuildableAlienRobot(), new BuildableWarper(),*/ new BuildableInfoPanel(), new BuildableMicroscope(), new BuildableTable(), new BuildableColumnSmall(), 
                new BuildablePedestal(), new BuildablePedestalLarge(), new BuildableLight1(), new BuildableLight2(), new BuildableLight3(), new BuildableLight4(), new BuildableLight5()
            };
            buildables.ForEach(buildable => buildable.Patch());
        }
    }
}
