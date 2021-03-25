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

namespace ProjectAncients
{
    [QModCore]
    public class Mod
    {
        public static AssetBundle assetBundle;

        public static SeamothElectricalDefenseMK2 electricalDefenseMk2;

        public static GargantuanJuvenile gargJuvenilePrefab;
        public static GargantuanVoid gargVoidPrefab;
        public static GargantuanBaby gargBabyPrefab;
        public static SkeletonGarg skeletonGargPrefab;
        public static GargantuanEgg gargEgg;

        public static GenericSignalPrefab signal_outpostC;
        public static GenericSignalPrefab signal_outpostD;
        public static GenericSignalPrefab signal_ruinedGuardian;

        public static TabletTerminalPrefab purpleTabletTerminal;
        public static TabletTerminalPrefab redTabletTerminal;
        public static TabletTerminalPrefab whiteTabletTerminal;
        public static TabletTerminalPrefab orangeTabletTerminal;
        public static TabletTerminalPrefab blueTabletTerminal;

        public static PrecursorDoorPrefab door_supplyCache;
        public static PrecursorDoorPrefab door_researchBase;
        public static PrecursorDoorPrefab whiteTabletDoor;

        public static PrecursorDoorPrefab voidDoor_red;
        public static PrecursorDoorPrefab voidDoor_white;
        public static PrecursorDoorPrefab voidDoor_blue;
        public static PrecursorDoorPrefab voidDoor_purple;
        public static PrecursorDoorPrefab voidDoor_orange;

        public static DataTerminalPrefab tertiaryOutpostTerminal;
        public static DataTerminalPrefab guardianTerminal;
        public static DataTerminalPrefab researchBaseTerminal;
        public static DataTerminalPrefab supplyCacheTerminal;
        public static DataTerminalPrefab archElectricityTerminal;

        public static GenericWorldPrefab secondaryBaseModel;
        public static GenericWorldPrefab voidBaseModel;

        public static RuinedGuardianPrefab prop_ruinedGuardian;

        public static TechType architectElectricityMasterTech;

        /// <summary>
        /// this value is only used by this mod, please dont use it or it'll cause conflicts.
        /// </summary>
        internal static DamageType architectElect = (DamageType)259745135;

        const string coordinateDisplayName = "Downloaded co-ordinates";

        private const string assetBundleName = "projectancientsassets";

        private const string modEncyPath_root = "DownloadedData/Precursor/GargMod";
        private const string modEncyPath_information = "DownloadedData/Precursor/GargMod/GargModInformation";
        private const string modEncyPath_analysis = "DownloadedData/Precursor/GargMod/GargModPrecursorAnalysis";
        private const string modEncyPath_tech = "DownloadedData/Precursor/GargMod/GargModPrecursorTech";

        private const string ency_tertiaryOutpostTerminal = "TertiaryOutpostTerminalData";
        private const string ency_supplyCacheTerminal = "SupplyCacheData";
        private const string ency_researchBaseTerminal = "ResearchBaseData";
        private const string ency_archElectricityTerminal = "ArchitectElectricityData";
        private const string ency_ruinedGuardian = "RuinedGuardian";
        private const string ency_distressSignal = "GuardianTerminalData";

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


            #region Translations
            LanguageHandler.SetLanguageLine("EncyPath_Lifeforms/Fauna/Titans", "Titans");
            LanguageHandler.SetLanguageLine(string.Format("EncyPath_{0}", modEncyPath_root), "Anomaly");
            LanguageHandler.SetLanguageLine(string.Format("EncyPath_{0}", modEncyPath_analysis), "Analysis");
            LanguageHandler.SetLanguageLine(string.Format("EncyPath_{0}", modEncyPath_information), "Information");
            LanguageHandler.SetLanguageLine(string.Format("EncyPath_{0}", modEncyPath_tech), "Technology");
            #endregion

            #region Tech
            architectElectricityMasterTech = TechTypeHandler.AddTechType("ArchitectElectricityMaster", "Ionic Pulse Technology", "Plasma-generating nanotechnology with defensive and offensive capabilities.", false);
            #endregion

            #region Modules
            electricalDefenseMk2 = new();
            electricalDefenseMk2.Patch();
            #endregion

            #region Creatures
            gargJuvenilePrefab = new GargantuanJuvenile("GargantuanJuvenile", "Gargantuan leviathan juvenile", "A titan-class lifeform. How did it get in your inventory?", assetBundle.LoadAsset<GameObject>("GargJuvenile_Prefab"), null);
            gargJuvenilePrefab.Patch();

            gargVoidPrefab = new GargantuanVoid("GargantuanVoid", "Gargantuan leviathan", "A titan-class lifeform. Indigineous to the void.", assetBundle.LoadAsset<GameObject>("GargAdult_Prefab"), null);
            gargVoidPrefab.Patch();

            gargBabyPrefab = new GargantuanBaby("GargantuanBaby", "Gargantuan baby", "A very young specimen, raised in containment.", assetBundle.LoadAsset<GameObject>("GargBaby_Prefab"), null);
            gargBabyPrefab.Patch();

            skeletonGargPrefab = new SkeletonGarg("SkeletonGargantuan", "Gargantuan skeleton", "Spooky.", assetBundle.LoadAsset<GameObject>("SkeletonGarg_Prefab"), null);
            skeletonGargPrefab.Patch();

            gargEgg = new GargantuanEgg();
            gargEgg.Patch();
            #endregion

            #region Initializers
            var expRoar = new ExplosionRoarInitializer();
            expRoar.Patch();

            var adultGargSpawner = new AdultGargSpawnerInitializer();
            adultGargSpawner.Patch();
            #endregion

            #region Signals
            signal_outpostC = new GenericSignalPrefab("OutpostCSignal", "Precursor_Symbol04", coordinateDisplayName, "Alien Signal A", new Vector3(-11, -178, -1155), 3);
            signal_outpostC.Patch();

            signal_outpostD = new GenericSignalPrefab("OutpostDSignal", "Precursor_Symbol01", coordinateDisplayName, "Alien Signal B", new Vector3(-500f, 0f, 0f), 3);
            signal_outpostD.Patch();

            signal_ruinedGuardian = new GenericSignalPrefab("RuinedGuardianSignal", "RuinedGuardian_Ping", "Unidentified tracking chip", "Distress signal", new Vector3(367, -333, -1747));
            signal_ruinedGuardian.Patch();
            #endregion


            #region Ency
            PatchEncy(ency_tertiaryOutpostTerminal, modEncyPath_information, "Tertiary Outpost Anaylsis", "This data terminal contains co-ordinates pointing to two secondary outposts. The existence for this outpost is unknown. There may have been more of these at one point, acting as a sort of interconnected navigational system.", "SignalPopup", "BlueGlyph_Ency");

            PatchEncy(ency_supplyCacheTerminal, modEncyPath_information, "Supply Cache Anaylsis", "This appears to be a large structure designed to hold valuabe resources for potential future use. Large pillar-shaped storage units line either side of the interior. The materials inside are condensed as far as physically possible in order to maintain a minuscule volume.");

            PatchEncy(ency_researchBaseTerminal, modEncyPath_information, "Destructive Technology Research Base", "This outpost acted as a hub for the testing of potentially dangerous technology. Examples include a powerful ionic pulse defense mechanism and some kind of sentry unit. The usage of this technology appears to have contributed to the destruction of the local ecosystem which was once flourishing with life.\n\nThe technology in this base may be exploited for personal use. Use with caution.", "SignalPopup", "BlueGlyph_Ency");

            PatchEncy(ency_ruinedGuardian, modEncyPath_analysis, "Mysterious Wreckage", "The shattered remains of a vast alien machine.\n\n1. Purpose:\nThe exact purpose of this device remains vague, but the hydrodynamic build, reinforced structure and various defence mechanisms suggest a mobile sentry. It was presumably tasked with guarding a location of significant importance from nearby roaming leviathan class lifeforms.\n\n2. Damage:\nAnalysis of the wreck reveals extensive damage in various places, which resulted in a near total system failure. The damage is consistent with being crushed, despite the extraordinary integrity of the construction material. The current state of the remains indicate the incident occurred recently and within the vicinity, despite no obvious culprit being found nearby. Whatever its purpose, it has obviously failed.\n\nAssessment: Further Research Required. Caution is advised.", "Guardian_Popup");

            PatchEncy(ency_distressSignal, modEncyPath_tech, "Abnormal Distress Signal", "This terminal has given your PDA access to a complicated tracking device. Frequent and intense electromagnetic pulses suggest it is under distress. Make sure to come prepared.", "Guardian_Popup");

            PatchEncy(ency_archElectricityTerminal, modEncyPath_tech, "Ionic Pulse Nanotechnology", "This data terminal contains the blueprints for an advanced nanotechnology used to generate a powerful plasma-based charge with a distinctive green glow. The applications of this medium include transferring high amounts of energy and incapacitating large fauna.\n\nYour PDA has generated several new upgrade blueprints which exploit this discovery.", "IonicCharge_Popup", "OrangeGlyph_Ency");

            #endregion

            #region Generic precursor stuff
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

            door_supplyCache = new PrecursorDoorPrefab("SupplyCacheDoor", "Supply cache door", "SupplyCacheDoor", orangeTabletTerminal.ClassID, true, new Vector3(0f, -0.2f, 8f), new Vector3(0f, 0f, 0f));
            door_supplyCache.Patch();

            door_researchBase = new PrecursorDoorPrefab("ResearchBaseDoor", "Research base door", "ResearchBaseDoor", whiteTabletTerminal.ClassID, true, new Vector3(0f, -0.2f, 8f), new Vector3(0f, 0f, 0f));
            door_researchBase.Patch();

            voidDoor_red = new PrecursorDoorPrefab("VoidDoorRed", "Door", redTabletTerminal.ClassID, "VoidDoorRed", true, new Vector3(0f, 0f, 16f), Vector3.up * 180f, "4ea69565-60e4-4554-bbdb-671eaba6dffb");
            voidDoor_red.Patch();

            voidDoor_blue = new PrecursorDoorPrefab("VoidDoorBlue", "Door", blueTabletTerminal.ClassID, "VoidDoorBlue", true, new Vector3(-3.5f, 0f, 14.5f), Vector3.up * -225f, "4ea69565-60e4-4554-bbdb-671eaba6dffb");
            voidDoor_blue.Patch();

            voidDoor_purple = new PrecursorDoorPrefab("VoidDoorPurple", "Door", purpleTabletTerminal.ClassID, "VoidDoorPurple", true, new Vector3(-5f, 0f, 11f), Vector3.up * -270f, "4ea69565-60e4-4554-bbdb-671eaba6dffb");
            voidDoor_purple.Patch();

            voidDoor_orange = new PrecursorDoorPrefab("VoidDoorOrange", "Door", orangeTabletTerminal.ClassID, "VoidDoorOrange", true, new Vector3(5f, 0f, 11f), Vector3.up * 270f, "4ea69565-60e4-4554-bbdb-671eaba6dffb");
            voidDoor_orange.Patch();

            voidDoor_white = new PrecursorDoorPrefab("VoidDoorWhite", "Door", whiteTabletTerminal.ClassID, "VoidDoorWhite", true, new Vector3(3.5f, 0f, 14.5f), Vector3.up * 225f, "4ea69565-60e4-4554-bbdb-671eaba6dffb");
            voidDoor_white.Patch();

            prop_ruinedGuardian = new RuinedGuardianPrefab();
            prop_ruinedGuardian.Patch();
            MakeObjectScannable(prop_ruinedGuardian.TechType, ency_ruinedGuardian, 6f);

            tertiaryOutpostTerminal = new DataTerminalPrefab("TertiaryOutpostTerminal", ency_tertiaryOutpostTerminal, new string[] { signal_outpostC.ClassID, signal_outpostD.ClassID });
            tertiaryOutpostTerminal.Patch();

            guardianTerminal = new DataTerminalPrefab("GuardianTerminal", ency_distressSignal, new string[] { signal_ruinedGuardian.ClassID }, "DataTerminal2", DataTerminalPrefab.blueTerminalCID, delay: 10f);
            guardianTerminal.Patch();

            supplyCacheTerminal = new DataTerminalPrefab("SupplyCacheTerminal", ency_supplyCacheTerminal, terminalClassId: DataTerminalPrefab.greenTerminalCID);
            supplyCacheTerminal.Patch();

            researchBaseTerminal = new DataTerminalPrefab("ResearchBaseTerminal", ency_researchBaseTerminal, terminalClassId: DataTerminalPrefab.greenTerminalCID, delay: 0f);
            researchBaseTerminal.Patch();

            archElectricityTerminal = new DataTerminalPrefab("ArchElectricityTerminal", ency_archElectricityTerminal, terminalClassId: DataTerminalPrefab.orangeTerminalCID, techToUnlock: architectElectricityMasterTech);
            archElectricityTerminal.Patch();

            secondaryBaseModel = new GenericWorldPrefab("SecondaryBaseModel", "Alien Structure", "A large alien structure.", assetBundle.LoadAsset<GameObject>("SmallCache_Prefab"), new UBERMaterialProperties(6f, 1f, 1f), LargeWorldEntity.CellLevel.Far);
            secondaryBaseModel.Patch();

            voidBaseModel = new GenericWorldPrefab("VoidBaseModel", "Alien Structure", "A large alien structure.", assetBundle.LoadAsset<GameObject>("VoidBase_Prefab"), new UBERMaterialProperties(6f, 1f, 1f), LargeWorldEntity.CellLevel.Far);
            voidBaseModel.Patch();
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

            var supplyCacheBase = new AlienBaseInitializer<SupplyCacheBaseSpawner>("SupplyCacheBase", new Vector3(-13, -175.81f, -1183)); //Sparse reef
            supplyCacheBase.Patch();

            var researchBase = new AlienBaseInitializer<ResearchBaseSpawner>("ResearchBase", new Vector3(-860, -180, -650)); //Crag field
            researchBase.Patch();

            var eggBase = new AlienBaseInitializer<VoidBaseSpawner>("VoidBase", new Vector3(373, -400, -1820)); //Void
            eggBase.Patch();
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
    }
}
