using ArchitectsLibrary.API;
using ArchitectsLibrary.Handlers;
using ECCLibrary;
using HarmonyLib;
using QModManager.API.ModLoading;
using RotA.Mono.AlienBaseSpawners;
using RotA.Patches;
using RotA.Prefabs;
using RotA.Prefabs.AlienBase;
using RotA.Prefabs.AlienBase.Teleporter;
using RotA.Prefabs.Buildable;
using RotA.Prefabs.Creatures;
using RotA.Prefabs.Equipment;
using RotA.Prefabs.Initializers;
using RotA.Prefabs.Modules;
using RotA.Prefabs.Placeable;
using RotA.Prefabs.Signals;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Handlers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UWE;

namespace RotA
{
    [QModCore]
    public class Mod
    {
        public static AssetBundle assetBundle;

        public static SeamothElectricalDefenseMK2 electricalDefenseMk2;
        public static GameObject electricalDefensePrefab;
        public static ExosuitZapModule exosuitZapModule;
        public static ExosuitDashModule exosuitDashModule;
        public static SuperDecoy superDecoy;

        public static GargantuanJuvenile gargJuvenilePrefab;
        public static GargantuanVoid gargVoidPrefab;
        public static GargantuanBaby gargBabyPrefab;
        public static SkeletonGarg spookySkeletonGargPrefab;
        public static GargantuanEgg gargEgg;
        public static AquariumGuppy aquariumGuppy;

        public static GenericSignalPrefab signal_cragFieldBase;
        public static GenericSignalPrefab signal_sparseReefBase;
        public static GenericSignalPrefab signal_kooshZoneBase;
        public static GenericSignalPrefab signal_ruinedGuardian;
        public static GenericSignalPrefab signal_cache_bloodKelp;
        public static GenericSignalPrefab signal_cache_sparseReef;
        public static GenericSignalPrefab signal_cache_dunes;
        public static GenericSignalPrefab signal_cache_lostRiver;

        public static TabletTerminalPrefab purpleTabletTerminal;
        public static TabletTerminalPrefab redTabletTerminal;
        public static TabletTerminalPrefab whiteTabletTerminal;
        public static TabletTerminalPrefab orangeTabletTerminal;
        public static TabletTerminalPrefab blueTabletTerminal;
        public static InfectionTesterTerminal infectionTesterTerminal;

        public static PrecursorDoorPrefab door_supplyCache;
        public static PrecursorDoorPrefab door_researchBase;
        public static PrecursorDoorPrefab door_kooshBase;
        public static PrecursorDoorPrefab whiteTabletDoor;

        public static VoidInteriorForcefield voidInteriorForcefield;
        public static PrecursorDoorPrefab voidDoor_red;
        public static PrecursorDoorPrefab voidDoor_white;
        public static PrecursorDoorPrefab voidDoor_blue;
        public static PrecursorDoorPrefab voidDoor_purple;
        public static PrecursorDoorPrefab voidDoor_orange;
        public static PrecursorDoorPrefab voidDoor_interior_left;
        public static PrecursorDoorPrefab voidDoor_interior_right;
        public static PrecursorDoorPrefab voidDoor_interior_infectionTest;

        public static DataTerminalPrefab tertiaryOutpostTerminalGrassy;
        public static DataTerminalPrefab tertiaryOutpostTerminalSparseReef;
        public static DataTerminalPrefab tertiaryOutpostTerminalLostRiver;
        public static DataTerminalPrefab guardianTerminal;
        public static DataTerminalPrefab researchBaseTerminal;
        public static DataTerminalPrefab supplyCacheTerminal;
        public static DataTerminalPrefab kooshBaseTerminal;
        public static DataTerminalPrefab archElectricityTerminal;
        public static DataTerminalPrefab voidBaseTerminal;
        public static DataTerminalPrefab cachePingsTerminal;
        public static DataTerminalPrefab spamTerminal;
        public static DataTerminalPrefab eggRoomTerminal;
        public static DataTerminalPrefab warpCannonTerminal;
        public static DataTerminalPrefab precursorMasterTechTerminal;
        public static DataTerminalPrefab redTabletHolder;
        public static DataTerminalPrefab devSecretTerminal;

        public static OmegaCubePedestal buildableOmegaCubePedestal;

        public static GenericWorldPrefab secondaryBaseModel;
        public static GenericWorldPrefab voidBaseModel;
        public static GenericWorldPrefab guardianTailfinModel;
        public static AquariumSkeleton aquariumSkeleton;
        public static BlackHolePrefab blackHole;
        public static OmegaCube omegaCube;
        public static OmegaCubeFabricator omegaCubeFabricator;

        public static AtmosphereVolumePrefab precursorAtmosphereVolume;

        public static AlienRelicPrefab ingotRelic;
        public static AlienRelicPrefab rifleRelic;
        public static AlienRelicPrefab bladeRelic;
        public static AlienRelicPrefab builderRelic;

        public static GargPoster gargPoster;

        public static WarpCannonPrefab warpCannon;

        public static RuinedGuardianPrefab prop_ruinedGuardian;

        public static TechType architectElectricityMasterTech;
        public static TechType warpMasterTech;

        /// <summary>
        /// this value is only used by this mod, please dont use it or it'll cause conflicts.
        /// </summary>
        internal static DamageType architectElect = (DamageType)259745135;

        /// <summary>
        /// this value is only used by this mod, please dont use it or it'll cause conflicts.
        /// </summary>
        internal static EcoTargetType superDecoyTargetType = (EcoTargetType)49013491;

        private const string assetBundleName = "projectancientsassets";

        public const string modEncyPath_root = "GargMod";
        public const string modEncyPath_terminalInfo = "GargMod/GargModInformation";
        public const string modEncyPath_analysis = "GargMod/GargModPrecursorAnalysis";
        public const string modEncyPath_tech = "GargMod/GargModPrecursorTech";
        public const string modEncyPath_relics = "GargMod/GargModPrecursorRelics";
        public const string modEncyPath_gargantuan = "GargMod/GargModGargantuanLeviathan";

        private const string ency_tertiaryOutpostTerminalGrassy = "TertiaryOutpostTerminal1Ency";
        private const string ency_tertiaryOutpostTerminalSparseReef = "TertiaryOutpostTerminal2Ency";
        private const string ency_tertiaryOutpostTerminalLostRiver = "TertiaryOutpostTerminal3Ency";
        private const string ency_supplyCacheTerminal = "SupplyCacheTerminalEncy";
        private const string ency_researchBaseTerminal = "ResearchBaseTerminalEncy";
        private const string ency_kooshBaseTerminal = "KooshBaseTerminalEncy";
        private const string ency_archElectricityTerminal = "ArchitectElectricityEncy";
        private const string ency_voidBaseTerminal = "VoidBaseTerminalEncy";
        private const string ency_ruinedGuardian = "RuinedGuardianEncy";
        private const string ency_distressSignal = "GuardianTerminalEncy";
        private const string ency_tailfin = "GuardianTailfinEncy";
        private const string ency_secondaryBaseModel = "SecondaryBaseModelEncy";
        private const string ency_voidBaseModel = "VoidBaseModelEncy";
        private const string ency_precingot = "PrecursorIngotEncy";
        private const string ency_cachePings = "CachePingsEncy";
        private const string ency_precrifle = "PrecursorRifleEncy";
        private const string ency_precblade = "PrecursorBladeEncy";
        private const string ency_precbuilder = "PrecursorBuilderEncy";
        private const string ency_alienSpam = "PrecursorSpamEncy";
        private const string ency_eggRoom = "PrecursorEggRoomEncy";
        private const string ency_aquariumSkeleton = "BabyGargSkeletonEncy";
        private const string ency_blackHole = "ResearchBaseSingularityEncy";
        private const string ency_warpCannonTerminal = "WarpCannonTerminalEncy";
        private const string ency_omegaCubeFabricator = "OmegaCubeFabricatorEncy";

        private const string alienSignalName = "Alien Signal";

        public static Config config = OptionsPanelHandler.Main.RegisterModOptions<Config>();

        private static Assembly myAssembly = Assembly.GetExecutingAssembly();

        public static string warpCannonSwitchFireModeCurrentlyWarpKey = "WarpCannonSwitchFireModeWarp";
        public static string warpCannonSwitchFireModeCurrentlyCreatureKey = "WarpCannonSwitchFireModeCreature";
        public static string warpCannonSwitchFireModeCurrentlyManipulateFirePrimaryKey = "WarpCannonSwitchFireModeManipulatePrimary";
        public static string warpCannonSwitchFireModeCurrentlyManipulateFireSecondaryKey = "WarpCannonSwitchFireModeManipulateSecond";
        public static string warpCannonNotEnoughPowerError = "WarpCannonNotEnoughPowerError";

        public static string omegaTerminalHoverText = "OmegaTerminalHoverText";
        public static string omegaTerminalInteract = "OmegaTerminalInteract";
        public static string omegaTerminalRegenerateCube = "OmegaCubeRegenerateCube";

        [QModPostPatch]
        public static void PostPatch()
        {
            //post patch because decorations mod does stuff to this recipe
            var redTabletTD = new TechData
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient(TechType.PrecursorIonCrystal, 1),
                    new Ingredient(TechType.AluminumOxide, 2)
                }
            };
            CraftDataHandler.SetTechData(TechType.PrecursorKey_Red, redTabletTD);
            var whiteTabletTD = new TechData
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient(TechType.PrecursorIonCrystal, 1),
                    new Ingredient(TechType.Silver, 2)
                }
            };
            CraftDataHandler.SetTechData(TechType.PrecursorKey_White, whiteTabletTD);

            //This stuff uses Architects Library resources so it must be patched AFTER that library
            warpCannon = new WarpCannonPrefab();
            warpCannon.Patch();
            PrecursorFabricatorService.SubscribeToFabricator(warpCannon.TechType, PrecursorFabricatorTab.Equipment);
            DisplayCaseServices.WhitelistTechType(warpCannon.TechType);

            warpCannonTerminal = new DataTerminalPrefab("WarpCannonTerminal", ency_warpCannonTerminal, terminalClassId: DataTerminalPrefab.orangeTerminalCID, techToAnalyze: warpMasterTech);
            warpCannonTerminal.Patch();

            gargPoster = new GargPoster();
            gargPoster.Patch();
            KnownTechHandler.SetAnalysisTechEntry(gargPoster.TechType, new List<TechType>() { gargPoster.TechType });
            buildableOmegaCubePedestal = new OmegaCubePedestal();
            buildableOmegaCubePedestal.Patch();
        }
        [QModPatch]
        public static void Patch()
        {
            assetBundle = ECCHelpers.LoadAssetBundleFromAssetsFolder(Assembly.GetExecutingAssembly(), assetBundleName);
            ECCAudio.RegisterClips(assetBundle);

            #region Static asset references
            CoroutineHost.StartCoroutine(LoadElectricalDefensePrefab());
            #endregion

            #region Translations
            LanguageHandler.SetLanguageLine("EncyPath_Lifeforms/Fauna/Titans", "Titans");
            LanguageHandler.SetLanguageLine(string.Format("EncyPath_{0}", modEncyPath_root), "Return of the Ancients");
            LanguageHandler.SetLanguageLine(string.Format("EncyPath_{0}", modEncyPath_analysis), "Analysis");
            LanguageHandler.SetLanguageLine(string.Format("EncyPath_{0}", modEncyPath_terminalInfo), "Information");
            LanguageHandler.SetLanguageLine(string.Format("EncyPath_{0}", modEncyPath_tech), "Technology");
            LanguageHandler.SetLanguageLine(string.Format("EncyPath_{0}", modEncyPath_relics), "Relics");
            LanguageHandler.SetLanguageLine(string.Format("EncyPath_{0}", modEncyPath_gargantuan), "Gargantuan Leviathan");
            LanguageHandler.SetLanguageLine(warpCannonSwitchFireModeCurrentlyWarpKey, "Teleport to base: {1}\nCurrent mode: Personal teleportation. Switch fire mode: {0}");
            LanguageHandler.SetLanguageLine(warpCannonSwitchFireModeCurrentlyCreatureKey, "Warp in creatures: {1}\nCurrent mode: Creature summon. Switch fire mode: {0}");
            LanguageHandler.SetLanguageLine(warpCannonSwitchFireModeCurrentlyManipulateFirePrimaryKey, "Create exit portal: {1}\nCurrent mode: Environment manipulation. Switch fire mode: {0}");
            LanguageHandler.SetLanguageLine(warpCannonSwitchFireModeCurrentlyManipulateFireSecondaryKey, "Create entrance portal: {1}\nCurrent mode: Environment manipulation. Switch fire mode: {0}");
            LanguageHandler.SetLanguageLine(warpCannonNotEnoughPowerError, "Insufficient power.");

            LanguageHandler.SetLanguageLine(omegaTerminalHoverText, "Omega terminal");
            LanguageHandler.SetLanguageLine(omegaTerminalInteract, "Insert data");
            LanguageHandler.SetLanguageLine(omegaTerminalRegenerateCube, "Generate omega cube");
            #endregion

            #region Tech
            architectElectricityMasterTech = TechTypeHandler.AddTechType("ArchitectElectricityMaster", "Ionic Pulse Technology", "Plasma-generating nanotechnology with defensive and offensive capabilities.", false);
            warpMasterTech = TechTypeHandler.AddTechType("WarpingMasterTech", "Handheld Warping Device", "An alien device that enables short-range teleportation.", false);
            #endregion

            #region Achievements
            AchievementServices.RegisterAchievement("CraftWarpCannon", "Hybrid Technology", assetBundle.LoadAsset<Sprite>("WarpCannon_Popup"), "This achievement is hidden.", "Obtained the Handheld Warping Device.", true);
            AchievementServices.RegisterAchievement("VisitVoidBase", "Structure over the Abyss", assetBundle.LoadAsset<Sprite>("Popup_Green"), "This achievement is hidden.", "Encountered the alien structure in the crater edge.", true);
            AchievementServices.RegisterAchievement("TouchBlackHole", "Do Not Touch", assetBundle.LoadAsset<Sprite>("Popup_Green"), "This achievement is hidden.", "Really...?", true);
            AchievementServices.RegisterAchievement("WarpFar", "Teleportation Master", assetBundle.LoadAsset<Sprite>("Warper_Popup"), "Warp 2000 meters.", "Warped 2000 meters.", true, 2000);
            AchievementServices.RegisterAchievement("DevSecretAchievement", "Unknown Architects", assetBundle.LoadAsset<Sprite>("Popup_Purple"), "This achievement is hidden.", "Found the Hallway of the Architects.", true);
            #endregion

            #region Modules
            electricalDefenseMk2 = new();
            electricalDefenseMk2.Patch();
            PrecursorFabricatorService.SubscribeToFabricator(electricalDefenseMk2.TechType, PrecursorFabricatorTab.UpgradeModules);

            exosuitZapModule = new();
            exosuitZapModule.Patch();
            PrecursorFabricatorService.SubscribeToFabricator(exosuitZapModule.TechType, PrecursorFabricatorTab.UpgradeModules);

            superDecoy = new();
            superDecoy.Patch();
            PrecursorFabricatorService.SubscribeToFabricator(superDecoy.TechType, PrecursorFabricatorTab.Devices);

            exosuitDashModule = new();
            exosuitDashModule.Patch();
            PrecursorFabricatorService.SubscribeToFabricator(exosuitDashModule.TechType, PrecursorFabricatorTab.UpgradeModules);
            #endregion

            #region Creatures
            gargJuvenilePrefab = new GargantuanJuvenile("GargantuanJuvenile", "Gargantuan Leviathan Juvenile", "A titan-class lifeform. How did it get in your inventory?", assetBundle.LoadAsset<GameObject>("GargJuvenile_Prefab"), null);
            gargJuvenilePrefab.Patch();

            gargVoidPrefab = new GargantuanVoid("GargantuanVoid", "Gargantuan Leviathan", "A titan-class lifeform. Indigineous to the void.", assetBundle.LoadAsset<GameObject>("GargAdult_Prefab"), null);
            gargVoidPrefab.Patch();

            gargBabyPrefab = new GargantuanBaby("GargantuanBaby", "Gargantuan Baby", "A very young specimen, raised in containment. Playful.", assetBundle.LoadAsset<GameObject>("GargBaby_Prefab"), assetBundle.LoadAsset<Texture2D>("GargantuanBaby_Icon"));
            gargBabyPrefab.Patch();

            spookySkeletonGargPrefab = new SkeletonGarg("SkeletonGargantuan", "Gargantuan Skeleton", "Spooky.", assetBundle.LoadAsset<GameObject>("SkeletonGarg_Prefab"), null);
            spookySkeletonGargPrefab.Patch();

            gargEgg = new GargantuanEgg();
            gargEgg.Patch();

            aquariumGuppy = new AquariumGuppy("AquariumGuppy", "Unknown Fish", "An interesting fish.", assetBundle.LoadAsset<GameObject>("AquariumGuppy"), null);
            aquariumGuppy.Patch();
            #endregion

            #region CreatureSpawns
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gargJuvenilePrefab, new Vector3(1245, -40, -716), "GargBehindAurora", 400f));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gargJuvenilePrefab, new Vector3(1450, -100, 180), "GargBehindAurora2", 400f));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gargJuvenilePrefab, new Vector3(-1386, -117, 346), "GargDunesMid", 400f));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gargJuvenilePrefab, new Vector3(-1244, -117, 989), "GargDunesMidUpper", 400f));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gargJuvenilePrefab, new Vector3(-1122, -117, 1420), "GargDunesUpper", 400f));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gargJuvenilePrefab, new Vector3(-1370, -60, -29f), "GargDunesLower", 400f));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gargJuvenilePrefab, new Vector3(1430, -300, 1547), "MountainsGarg", 400f));
            #endregion

            #region Initializers
            var expRoar = new ExplosionRoarInitializer();
            expRoar.Patch();

            var adultGargSpawner = new AdultGargSpawnerInitializer();
            adultGargSpawner.Patch();

            var gargSecretCommand = new SecretCommandInitializer();
            gargSecretCommand.Patch();

            var miscInitializers = new MiscInitializers();
            miscInitializers.Patch();
            #endregion

            #region Signals
            signal_cragFieldBase = new GenericSignalPrefab("OutpostCSignal", "Precursor_Symbol04", "Downloaded co-ordinates", alienSignalName, new Vector3(-11, -178, -1155), 3); //white tablet icon
            signal_cragFieldBase.Patch();

            signal_sparseReefBase = new GenericSignalPrefab("OutpostDSignal", "Precursor_Symbol01", "Downloaded co-ordinates", alienSignalName, new Vector3(-810f, -184f, -590f), 3); //red tablet icon
            signal_sparseReefBase.Patch();

            signal_kooshZoneBase = new GenericSignalPrefab("KooshZoneBaseSignal", "Precursor_Symbol05", "Downloaded co-ordinates", alienSignalName, new Vector3(1489, -420, 1337), 3, new(true, "KooshBaseSignalSubtitle", "KooshBaseEncounter", "Discover koosh zone base text.", Mod.assetBundle.LoadAsset<AudioClip>("GuardianEncounter"), 2f)); //purple tablet icon
            signal_kooshZoneBase.Patch();

            signal_ruinedGuardian = new GenericSignalPrefab("RuinedGuardianSignal", "RuinedGuardian_Ping", "Unidentified tracking chip", "Distress signal", new Vector3(367, -333, -1747), 0, new(true, "GuardianEncounterSubtitle", "GuardianEncounter", "This machine appears to have recently collapsed to the seafloor. Further research required.", Mod.assetBundle.LoadAsset<AudioClip>("GuardianEncounter"), 3f));
            signal_ruinedGuardian.Patch();

            signal_cache_bloodKelp = new GenericSignalPrefab("BloodKelpCacheSignal", "CacheSymbol1", "Blood Kelp Zone Sanctuary", alienSignalName + " (535m)", new Vector3(-554, -534, 1518), defaultColorIndex: 2);
            signal_cache_bloodKelp.Patch();

            signal_cache_sparseReef = new GenericSignalPrefab("SparseReefCacheSignal", "CacheSymbol2", "Deep Sparse Reef Sanctuary", alienSignalName + " (287m)", new Vector3(-929, -287, -760), defaultColorIndex: 1);
            signal_cache_sparseReef.Patch();

            signal_cache_dunes = new GenericSignalPrefab("DunesCacheSignal", "CacheSymbol3", "Dunes Sanctuary", alienSignalName + " (380m)", new Vector3(-1187, -378, 1130), defaultColorIndex: 4);
            signal_cache_dunes.Patch();

            signal_cache_lostRiver = new GenericSignalPrefab("LostRiverCacheSignal", "CacheSymbol4", "Lost River Laboratory Cache", alienSignalName + " (685m)", new Vector3(-1111, -685, -655), defaultColorIndex: 3);
            signal_cache_lostRiver.Patch();
            #endregion

            #region Ency entries
            PatchEncy(ency_tertiaryOutpostTerminalGrassy, modEncyPath_terminalInfo, "We've hidden all the PDA entries, for now. There's plenty to read in the future.", "Popup_Blue", "TO_G_Ency");

            #endregion

            #region Precursor base prefabs
            orangeTabletTerminal = new TabletTerminalPrefab("OrangeTabletTerminal", PrecursorKeyTerminal.PrecursorKeyType.PrecursorKey_Orange);
            orangeTabletTerminal.Patch();

            blueTabletTerminal = new TabletTerminalPrefab("BlueTabletTerminal", PrecursorKeyTerminal.PrecursorKeyType.PrecursorKey_Blue);
            blueTabletTerminal.Patch();

            purpleTabletTerminal = new TabletTerminalPrefab("PurpleTabletTerminal", PrecursorKeyTerminal.PrecursorKeyType.PrecursorKey_Purple);
            purpleTabletTerminal.Patch();

            redTabletTerminal = new TabletTerminalPrefab("RedTabletTerminal", PrecursorKeyTerminal.PrecursorKeyType.PrecursorKey_Red);
            redTabletTerminal.Patch();

            whiteTabletTerminal = new TabletTerminalPrefab("WhiteTabletTerminal", PrecursorKeyTerminal.PrecursorKeyType.PrecursorKey_White);
            whiteTabletTerminal.Patch();

            infectionTesterTerminal = new InfectionTesterTerminal("InfectionTesterTerminal");
            infectionTesterTerminal.Patch();

            door_supplyCache = new PrecursorDoorPrefab("SupplyCacheDoor", "Supply cache door", orangeTabletTerminal.ClassID, "SupplyCacheDoor", true, new Vector3(0f, -0.2f, 8f), new Vector3(0f, 0f, 0f));
            door_supplyCache.Patch();

            door_researchBase = new PrecursorDoorPrefab("ResearchBaseDoor", "Research base door", whiteTabletTerminal.ClassID, "ResearchBaseDoor", true, new Vector3(0f, -0.2f, 8f), new Vector3(0f, 0f, 0f));
            door_researchBase.Patch();

            door_kooshBase = new PrecursorDoorPrefab("KooshBaseDoor", "Bulb Zone base door", purpleTabletTerminal.ClassID, "KooshBaseDoor", true, new Vector3(0f, -0.2f, 8f), new Vector3(0f, 0f, 0f));
            door_kooshBase.Patch();

            const string bigDoor = "4ea69565-60e4-4554-bbdb-671eaba6dffb";
            const string smallDoor = "caaad5e8-4923-4f66-8437-f49914bc5347";
            voidDoor_red = new PrecursorDoorPrefab("VoidDoorRed", "Door", redTabletTerminal.ClassID, "VoidDoorRed", true, new Vector3(0f, 0f, 9.5f), Vector3.up * 0f, bigDoor, false);
            voidDoor_red.Patch();

            voidDoor_blue = new PrecursorDoorPrefab("VoidDoorBlue", "Door", blueTabletTerminal.ClassID, "VoidDoorBlue", true, new Vector3(-5f, 0f, 14.5f), Vector3.up * 90f, bigDoor, false);
            voidDoor_blue.Patch();

            voidDoor_purple = new PrecursorDoorPrefab("VoidDoorPurple", "Door", purpleTabletTerminal.ClassID, "VoidDoorPurple", true, new Vector3(-3.5f, 0f, 11f), Vector3.up * 45f, bigDoor, false);
            voidDoor_purple.Patch();

            voidDoor_orange = new PrecursorDoorPrefab("VoidDoorOrange", "Door", orangeTabletTerminal.ClassID, "VoidDoorOrange", true, new Vector3(3.5f, 0f, 11f), Vector3.up * 315f, bigDoor, false);
            voidDoor_orange.Patch();

            voidDoor_white = new PrecursorDoorPrefab("VoidDoorWhite", "Door", whiteTabletTerminal.ClassID, "VoidDoorWhite", true, new Vector3(5f, 0f, 14.5f), Vector3.up * -90f, bigDoor, false);
            voidDoor_white.Patch();

            voidDoor_interior_infectionTest = new PrecursorDoorPrefab("VoidDoorInfectionTest", "Door", infectionTesterTerminal.ClassID, "VoidDoorInfectionTest", true, new Vector3(-4f, 0f, 4f), Vector3.up * -90f, bigDoor, true);
            voidDoor_interior_infectionTest.Patch();

            voidInteriorForcefield = new VoidInteriorForcefield();
            voidInteriorForcefield.Patch();

            voidDoor_interior_left = new PrecursorDoorPrefab("VoidDoorInteriorL", "Door", purpleTabletTerminal.ClassID, "VoidDoorInterior", rootPrefabClassId: smallDoor, overrideTerminalPosition: true, terminalLocalPosition: new Vector3(-4f, 0f, -3f), terminalLocalRotation: Vector3.up * 90f);
            voidDoor_interior_left.Patch();

            voidDoor_interior_right = new PrecursorDoorPrefab("VoidDoorInteriorR", "Door", purpleTabletTerminal.ClassID, "VoidDoorInterior", rootPrefabClassId: smallDoor, overrideTerminalPosition: true, terminalLocalPosition: new Vector3(4f, 0f, -3f), terminalLocalRotation: Vector3.up * 270f);
            voidDoor_interior_right.Patch();

            prop_ruinedGuardian = new RuinedGuardianPrefab();
            prop_ruinedGuardian.Patch();
            MakeObjectScannable(prop_ruinedGuardian.TechType, ency_ruinedGuardian, 6f);

            secondaryBaseModel = new GenericWorldPrefab("SecondaryBaseModel", "Alien Structure", "A large alien structure.", assetBundle.LoadAsset<GameObject>("SmallCache_Prefab"), new UBERMaterialProperties(7f, 35f, 1f), LargeWorldEntity.CellLevel.Far);
            secondaryBaseModel.Patch();
            MakeObjectScannable(secondaryBaseModel.TechType, ency_secondaryBaseModel, 6f);

            voidBaseModel = new VoidBaseModel("VoidBaseModel", "Alien Structure", "A large alien structure.", assetBundle.LoadAsset<GameObject>("VoidBase_Prefab"), new UBERMaterialProperties(6f, 15f, 1f), LargeWorldEntity.CellLevel.VeryFar);
            voidBaseModel.Patch();
            MakeObjectScannable(voidBaseModel.TechType, ency_voidBaseModel, 6f);

            guardianTailfinModel = new GenericWorldPrefab("GuardianTailfin", "Mechanical Segment", "A tail.", assetBundle.LoadAsset<GameObject>("GuardianTailfin_Prefab"), new UBERMaterialProperties(7f, 1f, 1f), LargeWorldEntity.CellLevel.Near);
            guardianTailfinModel.Patch();
            MakeObjectScannable(guardianTailfinModel.TechType, ency_tailfin, 2f);

            ingotRelic = new AlienRelicPrefab("PrecursorIngotRelic", "Alien Structural Alloy", "An alien ingot.", assetBundle.LoadAsset<GameObject>("PrecursorIngot_Prefab"), 0.3f);
            ingotRelic.Patch();
            MakeObjectScannable(ingotRelic.TechType, ency_precingot, 2f);

            rifleRelic = new AlienRelicPrefab("PrecursorRifleRelic", "Alien Rifle", "An alien rifle.", assetBundle.LoadAsset<GameObject>("PrecursorRifle_Prefab"), 0.2f);
            rifleRelic.Patch();
            MakeObjectScannable(rifleRelic.TechType, ency_precrifle, 2f);

            bladeRelic = new AlienRelicPrefab("PrecursorBladeRelic", "Alien Knife", "An alien knife.", assetBundle.LoadAsset<GameObject>("PrecursorBlade_Prefab"), 0.8f);
            bladeRelic.Patch();
            MakeObjectScannable(bladeRelic.TechType, ency_precblade, 2f);

            builderRelic = new AlienRelicPrefab("PrecursorBuilderRelic", "Alien Construction Tool", "An alien construction tool.", assetBundle.LoadAsset<GameObject>("PrecursorBuilder_Prefab"), 0.8f);
            builderRelic.Patch();
            MakeObjectScannable(builderRelic.TechType, ency_precbuilder, 3f);

            aquariumSkeleton = new AquariumSkeleton("VoidbaseAquariumSkeleton", "Leviathan Skeletal Remains", "The remains of a juvenile leviathan specimen.", assetBundle.LoadAsset<GameObject>("AquariumSkeleton"), new UBERMaterialProperties(4f, 1f, 1f), LargeWorldEntity.CellLevel.Medium, false);
            aquariumSkeleton.Patch();
            MakeObjectScannable(aquariumSkeleton.TechType, ency_aquariumSkeleton, 5f);

            blackHole = new BlackHolePrefab();
            blackHole.Patch();
            MakeObjectScannable(blackHole.TechType, ency_blackHole, 3f);

            precursorAtmosphereVolume = new AtmosphereVolumePrefab("PrecursorAntechamberVolume");
            precursorAtmosphereVolume.Patch();

            omegaCubeFabricator = new OmegaCubeFabricator();
            omegaCubeFabricator.Patch();
            MakeObjectScannable(omegaCubeFabricator.TechType, ency_omegaCubeFabricator, 3f);
            #endregion

            #region Alien terminals
            tertiaryOutpostTerminalGrassy = new DataTerminalPrefab("TertiaryOutpostTerminal1", ency_tertiaryOutpostTerminalGrassy, new string[] { signal_cragFieldBase.ClassID, signal_sparseReefBase.ClassID, signal_kooshZoneBase.ClassID }, audioClipPrefix: "DataTerminalOutpost", delay: 5f, subtitles: "Detecting an alien broadcast. Uploading co-ordinates to PDA.");
            tertiaryOutpostTerminalGrassy.Patch();

            tertiaryOutpostTerminalSparseReef = new DataTerminalPrefab("TertiaryOutpostTerminal2", ency_tertiaryOutpostTerminalSparseReef, new string[] { signal_cragFieldBase.ClassID, signal_sparseReefBase.ClassID, signal_kooshZoneBase.ClassID }, audioClipPrefix: "DataTerminalOutpost", delay: 5f, subtitles: "Detecting an alien broadcast. Uploading co-ordinates to PDA.");
            tertiaryOutpostTerminalSparseReef.Patch();

            tertiaryOutpostTerminalLostRiver = new DataTerminalPrefab("TertiaryOutpostTerminal3", ency_tertiaryOutpostTerminalLostRiver, new string[] { signal_cragFieldBase.ClassID, signal_sparseReefBase.ClassID, signal_kooshZoneBase.ClassID }, audioClipPrefix: "DataTerminalOutpost", delay: 5f, subtitles: "Detecting an alien broadcast. Uploading co-ordinates to PDA.");
            tertiaryOutpostTerminalLostRiver.Patch();

            guardianTerminal = new DataTerminalPrefab("GuardianTerminal", ency_distressSignal, new string[] { signal_ruinedGuardian.ClassID }, "DataTerminalDistress", DataTerminalPrefab.blueTerminalCID, delay: 6f, subtitles: "Detecting an alien distress broadcast. Uploading co-ordinates to PDA.");
            guardianTerminal.Patch();

            supplyCacheTerminal = new DataTerminalPrefab("SupplyCacheTerminal", ency_supplyCacheTerminal, terminalClassId: DataTerminalPrefab.greenTerminalCID, audioClipPrefix: "DataTerminalEncy", delay: 5f, subtitles: "Downloading alien data... Download complete.");
            supplyCacheTerminal.Patch();

            researchBaseTerminal = new DataTerminalPrefab("ResearchBaseTerminal", ency_researchBaseTerminal, terminalClassId: DataTerminalPrefab.greenTerminalCID, delay: 5f, audioClipPrefix: "DataTerminalEncy", subtitles: "Downloading alien data... Download complete.");
            researchBaseTerminal.Patch();

            kooshBaseTerminal = new DataTerminalPrefab("KooshBaseTerminal", ency_kooshBaseTerminal, terminalClassId: DataTerminalPrefab.greenTerminalCID, delay: 5f, audioClipPrefix: "DataTerminalEncy", subtitles: "Downloading alien data... Download complete.");
            kooshBaseTerminal.Patch();

            archElectricityTerminal = new DataTerminalPrefab("ArchElectricityTerminal", null, terminalClassId: DataTerminalPrefab.orangeTerminalCID, techToUnlock: new[] { superDecoy.TechType, exosuitDashModule.TechType, exosuitZapModule.TechType, electricalDefenseMk2.TechType }, audioClipPrefix: "DataTerminalIonicPulse", delay: 4.6f, subtitles: "Snythesizing Ionic Energy Pulse blueprints from alien data. Blueprints stored to databank.");
            archElectricityTerminal.Patch();

            voidBaseTerminal = new DataTerminalPrefab("VoidBaseTerminal", ency_voidBaseTerminal, terminalClassId: DataTerminalPrefab.greenTerminalCID, delay: 5f, audioClipPrefix: "DataTerminalEncy", subtitles: "Downloading alien data... Download complete.");
            voidBaseTerminal.Patch();

            cachePingsTerminal = new DataTerminalPrefab("CachePingsTerminal", ency_cachePings, terminalClassId: DataTerminalPrefab.blueTerminalCID, audioClipPrefix: "DataTerminalOutpost", delay: 5f, subtitles: "Detecting an alien broadcast. Uploading co-ordinates to PDA.", pingClassId: new[] { signal_cache_bloodKelp.ClassID, signal_cache_sparseReef.ClassID, signal_cache_dunes.ClassID, signal_cache_lostRiver.ClassID });
            cachePingsTerminal.Patch();

            spamTerminal = new DataTerminalPrefab("SpamTerminal", ency_alienSpam, terminalClassId: DataTerminalPrefab.greenTerminalCID, delay: 5f, audioClipPrefix: "DataTerminalEncy", subtitles: "Downloading alien data... Download complete.");
            spamTerminal.Patch();

            eggRoomTerminal = new DataTerminalPrefab("EggRoomTerminal", ency_eggRoom, terminalClassId: DataTerminalPrefab.greenTerminalCID, delay: 5f, audioClipPrefix: "DataTerminalEncy", subtitles: "Downloading alien data... Download complete.");
            eggRoomTerminal.Patch();

            precursorMasterTechTerminal = new DataTerminalPrefab("MasterTechTerminal", null, terminalClassId: DataTerminalPrefab.orangeTerminalCID, techToAnalyze: AUHandler.AlienTechnologyMasterTech);
            precursorMasterTechTerminal.Patch();

            redTabletHolder = new DataTerminalPrefab("RedTabletHolder", null, hideSymbol: true, overrideColor: true, fxColor: new Color(1f, 0.5f, 0.5f), disableInteraction: true);
            redTabletHolder.Patch();

            devSecretTerminal = new DataTerminalPrefab("DevSecretTerminal", null, terminalClassId: DataTerminalPrefab.orangeTerminalCID, overrideColor: true, fxColor: new Color(1f, 0f, 0.75f), achievement: "DevSecretAchievement");
            devSecretTerminal.Patch();
            #endregion

            #region Teleporters
            TeleporterNetwork voidPcfNetwork = new TeleporterNetwork("VoidBasePCF", new Vector3(373, -400 + 18f - 0.5f, -1880 - 40f - 55f), 0f, new Vector3(321.88f, -1438.50f, -393.03f), 240f, true, false);
            voidPcfNetwork.Patch();

            TeleporterNetwork voidWeaponsNetwork = new TeleporterNetwork("VoidBaseWeaponsBase", new Vector3(373 - 50f, -400, -1880 - 40f - 10f), 0f, new Vector3(-857.80f, -189.89f - 0.4f, -641.00f - 14f), 0f, false, true);
            voidWeaponsNetwork.Patch();

            TeleporterNetwork voidSupplyNetwork = new TeleporterNetwork("VoidBaseSupplyCache", new Vector3(373 + 50f, -400, -1880 - 40f - 10f), 0f, new Vector3(-10.80f, -178.50f - 0.4f, -1183.00f - 14f), 0f, false, true);
            voidSupplyNetwork.Patch();

            TeleporterNetwork secretTeleporter = new TeleporterNetwork("SCFSecretTeleporter", new Vector3(218f, -1376, -260f), 150f, new Vector3(-959, -1440, 76f), 206f, false, false);
            secretTeleporter.Patch();

            TeleporterNetwork kooshBaseDevSecret = new TeleporterNetwork("KooshBaseDevSecret", new Vector3(1480 + 2.2f, -457 - 0.4f - 2.89f, 1457 - 14f), 0f, new Vector3(0f, -500f, 0f), 0f, false, true, true, new TeleporterPrimaryPrefab.CustomItemSettings(new TechType[] { omegaCube.TechType }, "KooshBasePortalTerminal", "Insert omega cube"));
            kooshBaseDevSecret.Patch();
            #endregion

            #region Alien bases
            var outpostAInitializer = new AlienBaseInitializer<OutpostBaseSpawner>("GargOutpostA", new Vector3(-702, -213, -780)); //Sparse reef
            outpostAInitializer.Patch();

            var outpostBInitializer = new AlienBaseInitializer<BonesFieldsOutpostSpawner>("GargOutpostB", new Vector3(-726, -757, -218)); //Bones fields
            outpostBInitializer.Patch();
            #endregion

            CraftDataHandler.SetItemSize(TechType.PrecursorKey_White, new Vector2int(1, 1));
            CraftDataHandler.AddToGroup(TechGroup.Personal, TechCategory.Equipment, TechType.PrecursorKey_Red);
            CraftDataHandler.AddToGroup(TechGroup.Personal, TechCategory.Equipment, TechType.PrecursorKey_White);
            CraftTreeHandler.AddCraftingNode(CraftTree.Type.Fabricator, TechType.PrecursorKey_White, new string[] { "Personal", "Equipment" });
            CraftTreeHandler.AddCraftingNode(CraftTree.Type.Fabricator, TechType.PrecursorKey_Red, new string[] { "Personal", "Equipment" });

            Harmony harmony = new Harmony($"ArchitectsOfTheUnknown_{myAssembly.GetName().Name}");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            FixMapModIfNeeded(harmony);
        }

        static IEnumerator LoadElectricalDefensePrefab()
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.Seamoth);
            yield return task;
            GameObject seamoth = task.GetResult();
            electricalDefensePrefab = seamoth.GetComponent<SeaMoth>().seamothElectricalDefensePrefab;
        }

        static void FixMapModIfNeeded(Harmony harmony)
        {
            var type = Type.GetType("SubnauticaMap.PingMapIcon, SubnauticaMap", false, false);
            if (type != null)
            {
                var pingOriginal = AccessTools.Method(type, "Refresh");
                var pingPrefix = new HarmonyMethod(AccessTools.Method(typeof(PingMapIcon_Patch), "Prefix"));
                harmony.Patch(pingOriginal, pingPrefix);
            }
        }

        public static void ApplyAlienUpgradeMaterials(Renderer renderer)
        {
            renderer.material.SetTexture("_MainTex", Mod.assetBundle.LoadAsset<Texture2D>("alienupgrademodule_diffuse"));
            renderer.material.SetTexture("_SpecTex", Mod.assetBundle.LoadAsset<Texture2D>("alienupgrademodule_spec"));
            renderer.material.SetTexture("_Illum", Mod.assetBundle.LoadAsset<Texture2D>("alienupgrademodule_illum"));
        }

        static void PatchEncy(string key, string path, string title, string desc, string popupName = null, string encyImageName = null)
        {
            Sprite popup = null;
            if (!string.IsNullOrEmpty(popupName))
            {
                popup = assetBundle.LoadAsset<Sprite>(popupName);
            }
            Texture2D encyImg = null;
            if (!string.IsNullOrEmpty(encyImageName))
            {
                encyImg = assetBundle.LoadAsset<Texture2D>(encyImageName);
            }
            PDAEncyclopediaHandler.AddCustomEntry(new PDAEncyclopedia.EntryData()
            {
                key = key,
                path = path,
                nodes = path.Split('/'),
                popup = popup,
                image = encyImg
            });
            LanguageHandler.SetLanguageLine("Ency_" + key, title);
            LanguageHandler.SetLanguageLine("EncyDesc_" + key, desc);
        }

        static void MakeObjectScannable(TechType techType, string encyKey, float scanTime)
        {
            PDAHandler.AddCustomScannerEntry(new PDAScanner.EntryData()
            {
                key = techType,
                encyclopedia = encyKey,
                scanTime = scanTime,
                isFragment = false
            });
        }

        private const string alienSpamEncyText = "This data terminal consists primarily of text in several unknown languages. Partially translated text is displayed below:\n\nTransfer of \u2580\u2596\u2517\u259b\u2584\u2596 failed. Sector Zero study of \u259c\u259a\u2523 \u259c\u259a\u2517\u2523\u2517\u252b\u2513\u250f\u2513 terminated for \u259b\u2584\u2596\u2505\u2517\u2596.\n\n\u2523\u2517\u250f\u259b\u2584\u2596\u259c\u250f\u2523 \u259a \u2596\u259e\u2523\u2517\u2596\u2517\u2523.\n\nVaccine progress: Awaiting termination.\n\nEmperor Apparatus status: Functioning.\n\n\u2523\u2517\u2596\u2503\u2580\u259a\u2597\u250f\u250f\u2513. \u2596\u251b\u2580\u2517\u259e\u2503\u250f\u2584 distress \u2580\u2596\u2517\u259b\u2596\u259c\u259a\u2523 data \u2505\u2596\u2517\u2501\u2596 \u2596\u2513\u252b\u259e\u2523 \u259a \u259b\u2584\u2505\u2517\u2596 \u259a \u2596\u259e\u2523\u2517\u2596\u2517\u2523 \u259a\u251b\u2598\u259e\u2501\u2596\u2505 \u259e\u2523\u2517\u2596\u2517\u2523.\n\n'Architects of the \u259a\u251b\u2598\u259e' status: missing. \u2501\u2596\u2505.\n\n\u2580\u2596\u2517\u259b\u259a\u2523 \u259c\u259a\u2517 \u259c\u259a.\n\nSpecimen of the Ancients terminated. Error: termination failed.";
    }
}
