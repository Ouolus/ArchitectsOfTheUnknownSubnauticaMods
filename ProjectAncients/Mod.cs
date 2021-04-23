using QModManager.API.ModLoading;
using ECCLibrary;
using UnityEngine;
using System.Reflection;
using ProjectAncients.Prefabs;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Crafting;
using HarmonyLib;
using System;
using ProjectAncients.Patches;
using ProjectAncients.Prefabs.AlienBase;
using ProjectAncients.Mono.AlienBaseSpawners;
using ProjectAncients.Prefabs.Modules;
using System.Collections;
using System.Collections.Generic;
using UWE;

namespace ProjectAncients
{
    [QModCore]
    public class Mod
    {
        public static AssetBundle assetBundle;

        public static SeamothElectricalDefenseMK2 electricalDefenseMk2;
        public static GameObject electricalDefensePrefab;
        public static ExosuitZapModule exosuitZapModule;
        public static SuperDecoy superDecoy;

        public static GargantuanJuvenile gargJuvenilePrefab;
        public static GargantuanVoid gargVoidPrefab;
        public static GargantuanBaby gargBabyPrefab;
        public static SkeletonGarg skeletonGargPrefab;
        public static GargantuanEgg gargEgg;

        public static GenericSignalPrefab signal_outpostC;
        public static GenericSignalPrefab signal_outpostD;
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

        public static PrecursorDoorPrefab door_supplyCache;
        public static PrecursorDoorPrefab door_researchBase;
        public static PrecursorDoorPrefab whiteTabletDoor;

        public static VoidInteriorForcefield voidInteriorForcefield;
        public static PrecursorDoorPrefab voidDoor_red;
        public static PrecursorDoorPrefab voidDoor_white;
        public static PrecursorDoorPrefab voidDoor_blue;
        public static PrecursorDoorPrefab voidDoor_purple;
        public static PrecursorDoorPrefab voidDoor_orange;
        public static PrecursorDoorPrefab voidDoor_interior_left;
        public static PrecursorDoorPrefab voidDoor_interior_right;

        public static DataTerminalPrefab tertiaryOutpostTerminalGrassy;
        public static DataTerminalPrefab tertiaryOutpostTerminalSparseReef;
        public static DataTerminalPrefab tertiaryOutpostTerminalLostRiver;
        public static DataTerminalPrefab guardianTerminal;
        public static DataTerminalPrefab researchBaseTerminal;
        public static DataTerminalPrefab supplyCacheTerminal;
        public static DataTerminalPrefab archElectricityTerminal;
        public static DataTerminalPrefab voidBaseTerminal;
        public static DataTerminalPrefab cachePingsTerminal;
        public static DataTerminalPrefab spamTerminal;

        public static GenericWorldPrefab secondaryBaseModel;
        public static GenericWorldPrefab voidBaseModel;
        public static GenericWorldPrefab guardianTailfinModel;

        public static AlienRelicPrefab ingotRelic;
        public static AlienRelicPrefab rifleRelic;
        public static AlienRelicPrefab bladeRelic;
        public static AlienRelicPrefab builderRelic;

        public static RuinedGuardianPrefab prop_ruinedGuardian;

        public static TechType architectElectricityMasterTech;

        /// <summary>
        /// this value is only used by this mod, please dont use it or it'll cause conflicts.
        /// </summary>
        internal static DamageType architectElect = (DamageType)259745135;

        /// <summary>
        /// this value is only used by this mod, please dont use it or it'll cause conflicts.
        /// </summary>
        internal static EcoTargetType superDecoyTargetType = (EcoTargetType)49013491;

        public const float voidBaseZOffset = -20;

        private const string assetBundleName = "projectancientsassets";

        private const string modEncyPath_root = "GargMod";
        private const string modEncyPath_terminalInfo = "GargMod/GargModInformation";
        private const string modEncyPath_analysis = "GargMod/GargModPrecursorAnalysis";
        private const string modEncyPath_tech = "GargMod/GargModPrecursorTech";
        private const string modEncyPath_relics = "GargMod/GargModPrecursorRelics";

        private const string ency_tertiaryOutpostTerminalGrassy = "TertiaryOutpostTerminal1Ency";
        private const string ency_tertiaryOutpostTerminalSparseReef = "TertiaryOutpostTerminal2Ency";
        private const string ency_tertiaryOutpostTerminalLostRiver = "TertiaryOutpostTerminal3Ency";
        private const string ency_supplyCacheTerminal = "SupplyCacheTerminalEncy";
        private const string ency_researchBaseTerminal = "ResearchBaseTerminalEncy";
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

        private const string alienSignalName = "Alien Signal";

        public static Config config = OptionsPanelHandler.Main.RegisterModOptions<Config>();

        private static Assembly myAssembly = Assembly.GetExecutingAssembly();

        [QModPostPatch]
        public static void PostPatch()
        {
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
            #endregion

            #region Tech
            architectElectricityMasterTech = TechTypeHandler.AddTechType("ArchitectElectricityMaster", "Ionic Pulse Technology", "Plasma-generating nanotechnology with defensive and offensive capabilities.", false);
            #endregion

            #region Modules
            electricalDefenseMk2 = new();
            electricalDefenseMk2.Patch();

            exosuitZapModule = new();
            exosuitZapModule.Patch();

            superDecoy = new();
            superDecoy.Patch();
            #endregion

            #region Creatures
            gargJuvenilePrefab = new GargantuanJuvenile("GargantuanJuvenile", "Gargantuan Leviathan Juvenile", "A titan-class lifeform. How did it get in your inventory?", assetBundle.LoadAsset<GameObject>("GargJuvenile_Prefab"), null);
            gargJuvenilePrefab.Patch();

            gargVoidPrefab = new GargantuanVoid("GargantuanVoid", "Gargantuan Leviathan", "A titan-class lifeform. Indigineous to the void.", assetBundle.LoadAsset<GameObject>("GargAdult_Prefab"), null);
            gargVoidPrefab.Patch();

            gargBabyPrefab = new GargantuanBaby("GargantuanBaby", "Gargantuan Baby", "A very young specimen, raised in containment. Playful.", assetBundle.LoadAsset<GameObject>("GargBaby_Prefab"), assetBundle.LoadAsset<Texture2D>("GargantuanBaby_Icon"));
            gargBabyPrefab.Patch();

            skeletonGargPrefab = new SkeletonGarg("SkeletonGargantuan", "Gargantuan Skeleton", "Spooky.", assetBundle.LoadAsset<GameObject>("SkeletonGarg_Prefab"), null);
            skeletonGargPrefab.Patch();

            gargEgg = new GargantuanEgg();
            gargEgg.Patch();
            #endregion

            #region CreatureSpawns
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gargJuvenilePrefab, new Vector3(1245, -40, -716), "GargBehindAurora", 400f));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gargJuvenilePrefab, new Vector3(1450, -220, 180), "GargBehindAurora2", 400f));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gargJuvenilePrefab, new Vector3(-1386, -117 ,346), "GargDunesMid", 400f));
            #endregion

            #region Initializers
            var expRoar = new ExplosionRoarInitializer();
            expRoar.Patch();

            var adultGargSpawner = new AdultGargSpawnerInitializer();
            adultGargSpawner.Patch();

            var gargSecretCommand = new SecretCommandInitializer();
            gargSecretCommand.Patch();
            #endregion

            #region Signals
            signal_outpostC = new GenericSignalPrefab("OutpostCSignal", "Precursor_Symbol04", "Downloaded co-ordinates", alienSignalName, new Vector3(-11, -178, -1155), 3);
            signal_outpostC.Patch();

            signal_outpostD = new GenericSignalPrefab("OutpostDSignal", "Precursor_Symbol01", "Downloaded co-ordinates", alienSignalName, new Vector3(-810f, -184f, -590f), 3);
            signal_outpostD.Patch();

            signal_ruinedGuardian = new GenericSignalPrefab("RuinedGuardianSignal", "RuinedGuardian_Ping", "Unidentified tracking chip", "Distress signal", new Vector3(367, -333, -1747));
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
            PatchEncy(ency_tertiaryOutpostTerminalGrassy, modEncyPath_terminalInfo, "Tertiary Outpost A Analysis", "An alien structure. The reason for the existence of this outpost is unknown. There may have been more of these at one point, acting as a sort of interconnected navigational system.\n\n1. Data Terminal:\nAn alien data terminal with a blue holographic symbol. Contains co-ordinates pointing to two secondary outposts.\n\n2. Claw device:\nA claw-shaped device with mechanical parts. Likely used a charging station for alien machines.", "Popup_Blue", "TO_G_Ency");

            PatchEncy(ency_tertiaryOutpostTerminalSparseReef, modEncyPath_terminalInfo, "Tertiary Outpost B Analysis", "An alien structure. The reason for the existence of this outpost is unknown. There may have been more of these at one point, acting as a sort of interconnected navigational system.\n\n1. Data Terminal:\nAn alien data terminal with a blue holographic symbol. Contains co-ordinates pointing to two secondary outposts.\n\n2. Claw device:\nA claw-shaped device with mechanical parts. Likely used a charging station for alien machines.\n\n3. Alien robot:\nThese devices are a common occurence in all alien technology. However, they are likely present in this base only to repair alien machinery while it is charging in the claw device.", "Popup_Blue", "TO_S_Ency");

            PatchEncy(ency_tertiaryOutpostTerminalLostRiver, modEncyPath_terminalInfo, "Tertiary Outpost C Analysis", "An alien structure. The reason for the existence of this outpost is unknown. There may have been more of these at one point, acting as a sort of interconnected navigational system.\n\n1. Data Terminal:\nAn alien data terminal with a blue holographic symbol. Contains co-ordinates pointing to two secondary outposts.\n\n2. Claw device:\nA claw-shaped device with mechanical parts. Likely used a charging station for alien machines.\n\n3. Orange Tablet:\nA rare alien artifact. It was likely put here for future use, but was never reclaimed.", "Popup_Blue", "TO_LR_Ency");

            PatchEncy(ency_supplyCacheTerminal, modEncyPath_terminalInfo, "Alien Supply Cache", "This large structure appears to be designed to hold valuabe resources for potential future use.\n\nAnalysis:\n- Large pillar-shaped storage units line either side of the interior. The materials inside are condensed as far as physically possible in order to maintain a minuscule volume.\n- Several exploitable mineral deposits are found loosely scattered in the base. A potential reason for this is an overflow of dedicated storage.\n- Several small alien structural alloy ingots are on display in the base. Their purpose appears to be aesthetic. Retrieval methods are still unknown.\n- The arch-like structure situated in the center of the cache, if not decorational, was likely used for quick transportation of supplies.", "Popup_Green", "SupplyCache_Ency");

            PatchEncy(ency_researchBaseTerminal, modEncyPath_terminalInfo, "Destructive Technology Research Base", "This outpost acted as a hub for the testing of potentially dangerous technology. Examples of technology include a powerful ionic pulse defense mechanism, a kind of sentry unit, and a uniquely designed weapon.\n\nAnalysis:\n- Lacking extensive decorations and structures, this base appears to be solely dedicated to research of destructive technology.\n- Mentions of a project under the name \"GUARDIAN\" are present, but any files that may have pertained to this project are either missing, corrupt, or encrypted.\n- Several alien robots wandering about the facility suggests they were used as a sort of test subject for weaponry, or even as tools for construction of said weaponry.\n- The development and usage of this technology appears to have contributed to the destruction of the local ecosystem, which was once flourishing with life.\n\nThe technology in this base may be exploited for personal use. Use with caution.", "Popup_green", "ResearchBase_Ency");

            PatchEncy(ency_ruinedGuardian, modEncyPath_analysis, "Mysterious Wreckage", "The shattered remains of a vast alien machine.\n\n1. Purpose:\nThe exact purpose of this device remains vague, but the hydrodynamic build, reinforced structure and various defence mechanisms suggest a mobile sentry. It was presumably tasked with guarding a location of significant importance from nearby roaming leviathan class lifeforms.\n\n2. Damage:\nAnalysis of the wreck reveals extensive damage in various places, which resulted in a near total system failure. The damage is consistent with being crushed, despite the extraordinary integrity of the construction material. The current state of the remains indicate the incident occurred recently and within the vicinity, despite no obvious culprit being found nearby. Whatever its purpose, it has obviously failed.\n\nAssessment: Further Research Required. Caution is advised.", "Guardian_Popup", "Guardian_Ency");

            PatchEncy(ency_distressSignal, modEncyPath_tech, "Alien Distress Signal", "This Data Terminal has given your PDA access to an encrypted tracking network. The only activity on the network is a distress signal from a distant location. Proceed with caution.", "Popup_Blue", "BlueGlyph_Ency");

            PatchEncy(ency_archElectricityTerminal, modEncyPath_tech, "Ionic Pulse Nanotechnology", "This Data Terminal contains the blueprints for an advanced nanotechnology used to generate a powerful plasma-based charge with a distinctive green glow. The applications of this medium include transferring high amounts of energy and incapacitating large fauna.\n\nYour PDA has generated several new upgrade blueprints which exploit this discovery.\n\nSynthesized blueprints:\n- Seamoth Perimeter Defense MK2\n- Prawn Suit Ion Defense Module\n- Creature Decoy MK2", "Popup_Orange", "OrangeGlyph_Ency");

            PatchEncy(ency_voidBaseTerminal, modEncyPath_terminalInfo, "Emperor Communications Apparatus", "This data terminal contains schematics and statistics relating to the facility. Analysis is shown below.\n\nDue to the outbreak of the kharaa bacterium, the aliens were desperate to develop a vaccine. The only known cure at the time, found in the last known 'Emperor', is too diluted to provide any definite use.\n\nDue to belief that more of these Emperors may exist, far away from the crater, this apparatus was constructed. While initially appearing similar to any other alien structure on the planet, schematics show an odd ability to expand downwards over half a kilometer, exposing a significant number of complex machines.\n\nThis machinery was designed to communicate with and attract any stray Emperors. Obviously, this plan has failed. However, it did attract another unusual juvenile specimen.\n\nThe base was eventually repurposed for the private studies of an individual.", "Popup_green", "GreenGlyph_Ency");

            PatchEncy(ency_tailfin, modEncyPath_analysis, "Alien Machine Tail Segment", "The tail of some sort of segmented machine. A lack of obvious damage suggests it was designed for intentional uncoupling when in danger.");

            PatchEncy(ency_secondaryBaseModel, modEncyPath_analysis, "Cache Structure", "A large structure with a mysterious design, used as long-term storage of data and resources. The entrance is forcefield-protected and airlocked, most likely to protect the valuables inside.");

            PatchEncy(ency_voidBaseModel, modEncyPath_analysis, "Suspended Platform", "A massive structure, over 300 meters in height. It is impossible to determine any applications of a base this large in such a dangerous area.\n\nThe unique design with the majority of the interior being inaccessible suggests a non-conventional use. More information may be located inside the structure.");

            PatchEncy(ency_precingot, modEncyPath_relics, "Alien Structural Alloy", "An unnamed alloy with unprecedented integrity. Appears to be non-malleable with any known technology. Luminescent detailing also suggests complex inner circuitry.\n\nNo practical applications can be simulated for this object.", "PrecIngot_Popup", "PrecIngot_Ency");

            PatchEncy(ency_cachePings, modEncyPath_terminalInfo, "Caches Location Data", "This Data Terminal contains a map with the co-ordinates of many locations, with data related to each. These co-ordinates have been uploaded to your PDA.\n\nLocational data:\n- Sanctuary Alpha: Found in the depths of a barren biome. Requires a purple tablet.\n- Sanctuary Beta: Found deep underwater in a dark cave system. Requires a purple tablet.\n- Sanctuary Gamma: Found near a mysterious crater. Requires a purple tablet.\n- Laboratory: Found in a well-lit cave system. Requires an orange tablet.\n\nCo-ordinates that are unusually pointing to the equator have not been uploaded. Traveling that far of a distance would be close to impossible.", "Popup_Blue", "BlueGlyph_Ency");

            PatchEncy(ency_precrifle, modEncyPath_relics, "Alien Rifle Variant", "This weapon strongly resembles a similar alien device found on the planet. The coloration however appears more close to the distinct architectural style of the aliens. Being powered by ion energy, it must have been extremely powerful.", "PrecRifle_Popup", "PrecRifle_Ency");

            PatchEncy(ency_precblade, modEncyPath_relics, "Alien Knife", "An alien knife with obvious applications. A lack of luminosity, which is almost always found in alien technology, suggests it is no longer powered.");

            PatchEncy(ency_precbuilder, modEncyPath_relics, "Alien Construction Tool", "An ancient construction tool that appears uncannily similar to the Alterra Habitat Builder.\n\nThis device was likely used to design and create large structures with ease, including all of the alien structures found on the planet. The fact that it has been left in stasis on site suggests this was the last structure it has ever built.", "PrecursorBuilder_Popup", "PrecursorBuilder_Ency");

            PatchEncy(ency_alienSpam, modEncyPath_terminalInfo, "Alien Document", alienSpamEncyText, "Popup_green", "GreenGlyph_Ency");
            #endregion

            #region Precursor base prefabs
            orangeTabletTerminal = new TabletTerminalPrefab("OrangeTabletTerminal", PrecursorKeyTerminal.PrecursorKeyType.PrecursorKey_Orange);
            orangeTabletTerminal.Patch();

            blueTabletTerminal = new TabletTerminalPrefab("BlueTabletTerminal", PrecursorKeyTerminal.PrecursorKeyType.PrecursorKey_Blue);
            blueTabletTerminal.Patch();

            purpleTabletTerminal = new TabletTerminalPrefab("PurpleTabletTerminal", PrecursorKeyTerminal.PrecursorKeyType.PrecursorKey_Purple);
            purpleTabletTerminal.Patch();

            redTabletTerminal = new TabletTerminalPrefab("RedTabletTerminal", PrecursorKeyTerminal.PrecursorKeyType.PrecursorKey_Red, 5f);
            redTabletTerminal.Patch();

            whiteTabletTerminal = new TabletTerminalPrefab("WhiteTabletTerminal", PrecursorKeyTerminal.PrecursorKeyType.PrecursorKey_White);
            whiteTabletTerminal.Patch();

            door_supplyCache = new PrecursorDoorPrefab("SupplyCacheDoor", "Supply cache door", orangeTabletTerminal.ClassID, "SupplyCacheDoor", true, new Vector3(0f, -0.2f, 8f), new Vector3(0f, 0f, 0f));
            door_supplyCache.Patch();

            door_researchBase = new PrecursorDoorPrefab("ResearchBaseDoor", "Research base door", whiteTabletTerminal.ClassID, "ResearchBaseDoor", true, new Vector3(0f, -0.2f, 8f), new Vector3(0f, 0f, 0f));
            door_researchBase.Patch();

            const string bigDoor = "4ea69565-60e4-4554-bbdb-671eaba6dffb";
            const string smallDoor = "caaad5e8-4923-4f66-8437-f49914bc5347";
            voidDoor_red = new PrecursorDoorPrefab("VoidDoorRed", "Door", redTabletTerminal.ClassID, "VoidDoorRed", true, new Vector3(0f, 0f, 16f), Vector3.up * 0f, bigDoor, false);
            voidDoor_red.Patch();

            voidDoor_blue = new PrecursorDoorPrefab("VoidDoorBlue", "Door", blueTabletTerminal.ClassID, "VoidDoorBlue", true, new Vector3(-3.5f, 0f, 14.5f), Vector3.up * -45f, bigDoor, false);
            voidDoor_blue.Patch();

            voidDoor_purple = new PrecursorDoorPrefab("VoidDoorPurple", "Door", purpleTabletTerminal.ClassID, "VoidDoorPurple", true, new Vector3(-5f, 0f, 11f), Vector3.up * 270f, bigDoor, false);
            voidDoor_purple.Patch();

            voidDoor_orange = new PrecursorDoorPrefab("VoidDoorOrange", "Door", orangeTabletTerminal.ClassID, "VoidDoorOrange", true, new Vector3(5f, 0f, 11f), Vector3.up * -270f, bigDoor, false);
            voidDoor_orange.Patch();

            voidDoor_white = new PrecursorDoorPrefab("VoidDoorWhite", "Door", whiteTabletTerminal.ClassID, "VoidDoorWhite", true, new Vector3(3.5f, 0f, 14.5f), Vector3.up * 45f, bigDoor, false);
            voidDoor_white.Patch();

            voidInteriorForcefield = new VoidInteriorForcefield();
            voidInteriorForcefield.Patch();

            voidDoor_interior_left = new PrecursorDoorPrefab("VoidDoorInteriorL", "Door", purpleTabletTerminal.ClassID, "VoidDoorInterior", rootPrefabClassId: smallDoor, overrideTerminalPosition: true, terminalLocalPosition: new Vector3(-4f, 0f, -3f), terminalLocalRotation: Vector3.up * 90f);
            voidDoor_interior_left.Patch();

            voidDoor_interior_right = new PrecursorDoorPrefab("VoidDoorInteriorL", "Door", purpleTabletTerminal.ClassID, "VoidDoorInterior", rootPrefabClassId: smallDoor, overrideTerminalPosition: true, terminalLocalPosition: new Vector3(4f, 0f, -3f), terminalLocalRotation: Vector3.up * 270f);
            voidDoor_interior_right.Patch();

            prop_ruinedGuardian = new RuinedGuardianPrefab();
            prop_ruinedGuardian.Patch();
            MakeObjectScannable(prop_ruinedGuardian.TechType, ency_ruinedGuardian, 6f);

            secondaryBaseModel = new GenericWorldPrefab("SecondaryBaseModel", "Alien Structure", "A large alien structure.", assetBundle.LoadAsset<GameObject>("SmallCache_Prefab"), new UBERMaterialProperties(7f, 35f, 1f), LargeWorldEntity.CellLevel.Far);
            secondaryBaseModel.Patch();
            MakeObjectScannable(secondaryBaseModel.TechType, ency_secondaryBaseModel, 6f);

            voidBaseModel = new VoidBaseModel("VoidBaseModel", "Alien Structure", "A large alien structure.", assetBundle.LoadAsset<GameObject>("VoidBase_Prefab"), new UBERMaterialProperties(6f, 15f, 1f), LargeWorldEntity.CellLevel.Far);
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
            #endregion

            #region Alien terminals
            tertiaryOutpostTerminalGrassy = new DataTerminalPrefab("TertiaryOutpostTerminal1", ency_tertiaryOutpostTerminalGrassy, new string[] { signal_outpostC.ClassID, signal_outpostD.ClassID }, audioClipPrefix: "DataTerminalOutpost", delay: 5f, subtitles: "Detecting an alien broadcast. Uploading co-ordinates to PDA.");
            tertiaryOutpostTerminalGrassy.Patch();

            tertiaryOutpostTerminalSparseReef = new DataTerminalPrefab("TertiaryOutpostTerminal2", ency_tertiaryOutpostTerminalSparseReef, new string[] { signal_outpostC.ClassID, signal_outpostD.ClassID }, audioClipPrefix: "DataTerminalOutpost", delay: 5f, subtitles: "Detecting an alien broadcast. Uploading co-ordinates to PDA.");
            tertiaryOutpostTerminalSparseReef.Patch();

            tertiaryOutpostTerminalLostRiver = new DataTerminalPrefab("TertiaryOutpostTerminal3", ency_tertiaryOutpostTerminalLostRiver, new string[] { signal_outpostC.ClassID, signal_outpostD.ClassID }, audioClipPrefix: "DataTerminalOutpost", delay: 5f, subtitles: "Detecting an alien broadcast. Uploading co-ordinates to PDA.");
            tertiaryOutpostTerminalLostRiver.Patch();

            guardianTerminal = new DataTerminalPrefab("GuardianTerminal", ency_distressSignal, new string[] { signal_ruinedGuardian.ClassID }, "DataTerminalDistress", DataTerminalPrefab.blueTerminalCID, delay: 6f, subtitles: "Detecting an alien distress broadcast. Uploading co-ordinates to PDA.");
            guardianTerminal.Patch();

            supplyCacheTerminal = new DataTerminalPrefab("SupplyCacheTerminal", ency_supplyCacheTerminal, terminalClassId: DataTerminalPrefab.greenTerminalCID, audioClipPrefix: "DataTerminalEncy", delay: 5f, subtitles: "Downloading alien data... Download complete.");
            supplyCacheTerminal.Patch();

            researchBaseTerminal = new DataTerminalPrefab("ResearchBaseTerminal", ency_researchBaseTerminal, terminalClassId: DataTerminalPrefab.greenTerminalCID, delay: 5f, audioClipPrefix: "DataTerminalEncy", subtitles: "Downloading alien data... Download complete.");
            researchBaseTerminal.Patch();

            archElectricityTerminal = new DataTerminalPrefab("ArchElectricityTerminal", ency_archElectricityTerminal, terminalClassId: DataTerminalPrefab.orangeTerminalCID, techToUnlock: architectElectricityMasterTech, audioClipPrefix: "DataTerminalIonicPulse", delay: 4.6f, subtitles: "Snythesizing Ionic Energy Pulse blueprints from alien data. Blueprints stored to databank.");
            archElectricityTerminal.Patch();

            voidBaseTerminal = new DataTerminalPrefab("VoidBaseTerminal", ency_voidBaseTerminal, terminalClassId: DataTerminalPrefab.greenTerminalCID, delay: 5f, audioClipPrefix: "DataTerminalEncy", subtitles: "Downloading alien data... Download complete.");
            voidBaseTerminal.Patch();

            cachePingsTerminal = new DataTerminalPrefab("CachePingsTerminal", ency_cachePings, terminalClassId: DataTerminalPrefab.blueTerminalCID, audioClipPrefix: "DataTerminalOutpost", delay: 5f, subtitles: "Detecting an alien broadcast. Uploading co-ordinates to PDA.", pingClassId: new[] { signal_cache_bloodKelp.ClassID, signal_cache_sparseReef.ClassID, signal_cache_dunes.ClassID, signal_cache_lostRiver.ClassID});
            cachePingsTerminal.Patch();

            spamTerminal = new DataTerminalPrefab("SpamTerminal", ency_alienSpam, terminalClassId: DataTerminalPrefab.greenTerminalCID, delay: 5f, audioClipPrefix: "DataTerminalEncy", subtitles: "Downloading alien data... Download complete.");
            spamTerminal.Patch();
            #endregion

            #region Teleporters
            TeleporterNetwork voidPcfNetwork = new TeleporterNetwork("VoidBasePCF", new Vector3(373, -400 + 18f - 0.5f, -1880 + voidBaseZOffset - 55f), 0f, new Vector3(267.30f, -1439f, -350.44f), 150f, true, true);
            voidPcfNetwork.Patch();

            TeleporterNetwork voidWeaponsNetwork = new TeleporterNetwork("VoidBaseWeaponsBase", new Vector3(373 - 50f, -400, -1880 + voidBaseZOffset - 10f), 0f, new Vector3(-857.80f, -189.89f, -641.00f), 0f);
            voidWeaponsNetwork.Patch();

            TeleporterNetwork voidSupplyNetwork = new TeleporterNetwork("VoidBaseSupplyCache", new Vector3(373 + 50f, -400, -1880 + voidBaseZOffset - 10f), 0f, new Vector3(-10.80f, -178.50f, -1183.00f), 0f);
            voidSupplyNetwork.Patch();

            #endregion

            #region Alien bases
            var outpostAInitializer = new AlienBaseInitializer<OutpostBaseSpawner>("GargOutpostA", new Vector3(-702, -213, -780)); //Sparse reef
            outpostAInitializer.Patch();

            var outpostBInitializer = new AlienBaseInitializer<BonesFieldsOutpostSpawner>("GargOutpostB", new Vector3(-726, -757, -218)); //Bones fields
            outpostBInitializer.Patch();

            var towerOutpostInitializer = new AlienBaseInitializer<TowerOutpostSpawner>("TowerOutpost", new Vector3(-526, -58, 22)); //Grassy plateaus
            towerOutpostInitializer.Patch();

            var guardianCablesInitializer = new AlienBaseInitializer<CablesNearGuardian>("GuardianCables", new Vector3(373, -358, -1762)); //Crag field
            guardianCablesInitializer.Patch();

            var supplyCacheBase = new AlienBaseInitializer<SupplyCacheBaseSpawner>("SupplyCacheBase", new Vector3(-13, -175.81f, -1183)); //Crag field
            supplyCacheBase.Patch();

            var researchBase = new AlienBaseInitializer<ResearchBaseSpawner>("ResearchBase", new Vector3(-860, -187, -641)); //Sparse reef
            researchBase.Patch();

            var eggBase = new AlienBaseInitializer<VoidBaseSpawner>("VoidBase", new Vector3(373, -400, -1880 + voidBaseZOffset), 300f, LargeWorldEntity.CellLevel.Far); //Void
            eggBase.Patch();

            var eggBaseInterior = new AlienBaseInitializer<VoidBaseInteriorSpawner>("VoidBaseInterior", new Vector3(373, -400, -1880 + voidBaseZOffset), 90, LargeWorldEntity.CellLevel.Medium); //Void
            eggBaseInterior.Patch();
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

        private const string alienSpamEncyText = "This data terminal consists primarily of text in several unknown languages. Partially translated text is displayed below:\n\nTransfer of \u2580\u2596\u2517\u259b\u2584\u2596 failed. Sector Zero study of \u259c\u259a\u2523 \u259c\u259a\u2517\u2523\u2517\u252b\u2513\u250f\u2513 terminated for \u259b\u2584\u2596\u2505\u2517\u2596.\n\n\u2523\u2517\u250f\u259b\u2584\u2596\u259c\u250f\u2523 \u259a \u2596\u259e\u2523\u2517\u2596\u2517\u2523.\n\nVaccine progress: Awaiting termination.\n\nEmperor Apparatus status: Functioning.\n\n\u2523\u2517\u2596\u2503\u2580\u259a\u2597\u250f\u250f\u2513. \u2596\u251b\u2580\u2517\u259e\u2503\u250f\u2584 distress \u2580\u2596\u2517\u259b\u2596\u259c\u259a\u2523 data \u2505\u2596\u2517\u2501\u2596 \u2596\u2513\u252b\u259e\u2523 \u259a \u259b\u2584\u2505\u2517\u2596 \u259a \u2596\u259e\u2523\u2517\u2596\u2517\u2523 \u259a\u251b\u2598\u259e\u2501\u2596\u2505 \u259e\u2523\u2517\u2596\u2517\u2523.\n\n'Architects of the \u259a\u251b\u2598\u259e' status: missing. \u2501\u2596\u2505.\n\n\u2580\u2596\u2517\u259b\u259a\u2523 \u259c\u259a\u2517 \u259c\u259a.\n\nSpecimen of the Ancients terminated.";
    }
}
