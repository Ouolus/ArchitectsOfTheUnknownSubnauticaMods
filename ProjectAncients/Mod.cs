using QModManager.API.ModLoading;
using ECCLibrary;
using UnityEngine;
using System.Reflection;
using ProjectAncients.Prefabs;
using SMLHelper.V2.Handlers;
using HarmonyLib;
using System;
using ProjectAncients.Patches;
using ProjectAncients.Prefabs.AlienBase;
using ProjectAncients.Mono.AlienBaseSpawners;

namespace ProjectAncients
{
    [QModCore]
    public class Mod
    {
        public static AssetBundle assetBundle;

        public static GargantuanJuvenile gargJuvenilePrefab;
        public static GargantuanVoid gargVoidPrefab;
        public static GargantuanBaby gargBabyPrefab;

        public static GenericSignalPrefab signal_outpostC;
        public static GenericSignalPrefab signal_outpostD;
        public static GenericSignalPrefab signal_ruinedGuardian;

        public static TabletTerminalPrefab redTabletTerminal;
        public static TabletTerminalPrefab whiteTabletTerminal;
        public static PrecursorDoorPrefab redTabletDoor;
        public static PrecursorDoorPrefab whiteTabletDoor;

        public static DataTerminalPrefab tertiaryOutpostTerminal;
        public static DataTerminalPrefab guardianTerminal;

        public static RuinedGuardianPrefab prop_ruinedGuardian;

        const string coordinateDisplayName = "Downloaded co-ordinates";

        private const string assetBundleName = "projectancientsassets";

        private const string modEncyPath = "DownloadedData/Precursor/GargMod";

        private const string ency_tertiaryOutpostTerminal = "TertiaryOutpostTerminalData";
        private const string ency_ruinedGuardian = "RuinedGuardian";
        private const string ency_distressSignal = "GuardianTerminalData";

        [QModPatch]
        public static void Patch()
        {
            assetBundle = ECCHelpers.LoadAssetBundleFromAssetsFolder(Assembly.GetExecutingAssembly(), assetBundleName);
            ECCAudio.RegisterClips(assetBundle);

            #region Translations
            LanguageHandler.SetLanguageLine("EncyPath_Lifeforms/Fauna/Titans", "Titans");
            LanguageHandler.SetLanguageLine(string.Format("EncyPath_{0}", modEncyPath), "Anomaly");
            #endregion

            #region Creatures
            gargJuvenilePrefab = new GargantuanJuvenile("GargantuanJuvenile", "Gargantuan leviathan juvenile", "A titan-class lifeform. How did it get in your inventory?", assetBundle.LoadAsset<GameObject>("GargJuvenile_Prefab"), null);
            gargJuvenilePrefab.Patch();

            gargVoidPrefab = new GargantuanVoid("GargantuanVoid", "Gargantuan leviathan", "A titan-class lifeform. Indigineous to the void.", assetBundle.LoadAsset<GameObject>("GargJuvenile_Prefab"), null);
            gargVoidPrefab.Patch();

            gargBabyPrefab = new GargantuanBaby("GargantuanBaby", "Gargantuan baby", "A very young specimen, raised in containment.", assetBundle.LoadAsset<GameObject>("GargBaby_Prefab"), null);
            gargBabyPrefab.Patch();
            #endregion

            #region Initializers
            var expRoar = new ExplosionRoarInitializer();
            expRoar.Patch();

            var adultGargSpawner = new AdultGargSpawnerInitializer();
            adultGargSpawner.Patch();
            #endregion

            #region Signals
            signal_outpostC = new GenericSignalPrefab("OutpostCSignal", "Precursor_Symbol04", coordinateDisplayName, "Alien Signal A", new Vector3(500f, 0f, 0f), 3);
            signal_outpostC.Patch();

            signal_outpostD = new GenericSignalPrefab("OutpostDSignal", "Precursor_Symbol01", coordinateDisplayName, "Alien Signal B", new Vector3(-500f, 0f, 0f), 3);
            signal_outpostD.Patch();

            signal_ruinedGuardian = new GenericSignalPrefab("RuinedGuardianSignal", "RuinedGuardian_Ping", "Unidentified tracking chip", "Distress signal", new Vector3(367, -333, -1747));
            signal_ruinedGuardian.Patch();
            #endregion


            #region Ency
            PatchEncy(ency_tertiaryOutpostTerminal, modEncyPath, "Tertiary Outpost Data", "This data terminal contains co-ordinates pointing to two secondary outposts. The existence for this outpost is unknown. There may have been more of these at one point, acting as a sort of interconnected navigational system.", "SignalPopup", "BlueGlyph_Ency");

            PatchEncy(ency_ruinedGuardian, modEncyPath, "Mysterious Wreckage", "The shattered remains of a vast alien machine.\n\n1. Purpose:\nThe exact purpose of this device remains vague, but the hydrodynamic build, reinforced structure and various defence mechanisms suggest a mobile sentry. It was presumably tasked with guarding a location of significant importance from nearby roaming leviathan class lifeforms.\n\n2. Damage:\n\nAnalysis of the wreck reveals extensive damage in various places, which resulted in a near total system failure. The damage is consistent with being crushed, despite the extraordinary integrity of the construction material. The current state of the remains indicate the incident occurred recently and within the vicinity, despite no obvious culprit being found nearby. Whatever its purpose, it has obviously failed.\n\nAssessment: Further Research Required. Caution is advised.", "Guardian_Popup", "Guardian_Ency");
            
            PatchEncy(ency_distressSignal, modEncyPath, "Abnormal Distress Signal", "This terminal has given your PDA access to a mysterious tracking chip. Frequent, powerful pulses suggest it is under distress. Make sure to come prepared.", "Guardian_Popup");

            #endregion

            #region Generic precursor stuff
            redTabletTerminal = new TabletTerminalPrefab("RedTabletTerminal", PrecursorKeyTerminal.PrecursorKeyType.PrecursorKey_Red);
            redTabletTerminal.Patch();

            whiteTabletTerminal = new TabletTerminalPrefab("WhiteTabletTerminal", PrecursorKeyTerminal.PrecursorKeyType.PrecursorKey_White);
            whiteTabletTerminal.Patch();

            redTabletDoor = new PrecursorDoorPrefab("RedTabletDoor", "Red tablet door", redTabletTerminal.ClassID);
            redTabletDoor.Patch();

            whiteTabletDoor = new PrecursorDoorPrefab("WhiteTabletDoor", "White tablet door", whiteTabletTerminal.ClassID);
            whiteTabletDoor.Patch();

            prop_ruinedGuardian = new RuinedGuardianPrefab();
            prop_ruinedGuardian.Patch();
            MakeObjectScannable(prop_ruinedGuardian.TechType, ency_ruinedGuardian, 6f);

            tertiaryOutpostTerminal = new DataTerminalPrefab("TertiaryOutpostTerminal", ency_tertiaryOutpostTerminal, new string[] { signal_outpostC.ClassID, signal_outpostD.ClassID });
            tertiaryOutpostTerminal.Patch();

            guardianTerminal = new DataTerminalPrefab("GuardianTerminal", ency_distressSignal, new string[] { signal_ruinedGuardian.ClassID }, "DataTerminal2", "81cf2223-455d-4400-bac3-a5bcd02b3638");
            guardianTerminal.Patch();
            #endregion


            #region Alien bases
            var outpostAInitializer = new AlienBaseInitializer<OutpostBaseSpawner>("GargOutpostA", new Vector3(-702, -213, -780));
            outpostAInitializer.Patch();

            var outpostBInitializer = new AlienBaseInitializer<OutpostBaseSpawner>("GargOutpostB", new Vector3(967, -62, 184));
            outpostBInitializer.Patch();

            var towerOutpostInitializer = new AlienBaseInitializer<TowerOutpostSpawner>("TowerOutpost", new Vector3(-526, -58, 22));
            towerOutpostInitializer.Patch();

            var guardianCablesInitializer = new AlienBaseInitializer<CablesNearGuardian>("GuardianCables", new Vector3(373, -358, -1762));
            guardianCablesInitializer.Patch();
            #endregion

            SMLHelper.V2.Handler.CraftDataHandler.SetItemSize(TechType.PrecursorKey_White, new Vector2int(1, 1));

            Harmony harmony = new Harmony("SCC.ProjectAncients");
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
