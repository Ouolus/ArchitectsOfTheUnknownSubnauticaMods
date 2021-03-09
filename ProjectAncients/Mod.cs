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

        public static GenericSignalPrefab outpostCSignal;
        public static GenericSignalPrefab outpostDSignal;

        public static TabletTerminalPrefab redTabletTerminal;
        public static TabletTerminalPrefab whiteTabletTerminal;
        public static PrecursorDoorPrefab redTabletDoor;
        public static PrecursorDoorPrefab whiteTabletDoor;
        public static DataTerminalPrefab outpostABTerminal;

        public static RuinedGuardianPrefab prop_ruinedGuardian;

        const string coordinateDisplayName = "Downloaded co-ordinates";

        private const string assetBundleName = "projectancientsassets";

        private const string modEncyPath = "DownloadedData/Precursor/GargMod";

        [QModPatch]
        public static void Patch()
        {
            assetBundle = ECCHelpers.LoadAssetBundleFromAssetsFolder(Assembly.GetExecutingAssembly(), assetBundleName);
            ECCAudio.RegisterClips(assetBundle);

            LanguageHandler.SetLanguageLine("EncyPath_Lifeforms/Fauna/Titans", "Titans");
            LanguageHandler.SetLanguageLine(string.Format("EncyPath_{0}", modEncyPath), "Gargantuan Mod (name WIP)");

            gargJuvenilePrefab = new GargantuanJuvenile("GargantuanJuvenile", "Gargantuan leviathan juvenile", "A titan-class lifeform. How did it get in your inventory?", assetBundle.LoadAsset<GameObject>("GargJuvenile_Prefab"), null);
            gargJuvenilePrefab.Patch();

            gargVoidPrefab = new GargantuanVoid("GargantuanVoid", "Gargantuan leviathan", "A titan-class lifeform. Indigineous to the void.", assetBundle.LoadAsset<GameObject>("GargJuvenile_Prefab"), null);
            gargVoidPrefab.Patch();

            var expRoar = new ExplosionRoarInitializer();
            expRoar.Patch();

            var adultGargSpawner = new AdultGargSpawnerInitializer();
            adultGargSpawner.Patch();

            #region Signals
            outpostCSignal = new GenericSignalPrefab("OutpostCSignal", "Precursor_Symbol04", coordinateDisplayName, "Signal A", new Vector3(500f, 0f, 0f), 3);
            outpostCSignal.Patch();

            outpostDSignal = new GenericSignalPrefab("OutpostDSignal", "Precursor_Symbol01", coordinateDisplayName, "Alien Signal B", new Vector3(-500f, 0f, 0f), 3);
            outpostDSignal.Patch();
            #endregion


            #region Ency
            PatchEncy("TertiaryOutpostData", modEncyPath, "Tertiary Outpost Data", "This data terminal contains co-ordinates pointing to two secondary outposts. The existence for this outpost is unknown. There may have been more of these at one point, acting as a sort of interconnected navigational system.", "SignalPopup", "BlueGlyph_Ency");

            PatchEncy("RuinedGuardian", modEncyPath, "Mysterious Wreckage", "This large object appears to be the remnants of an autonomous defensive machine. A single, large dent indicates it was taken out with extreme ease.\n\n1. Design:\nThis machine resembles a large shark-like creature. With an interesting color scheme and unique engravings, it is certainly alien in design. Several prongs lining either side of the body suggest an electric defense ability.\n\n2. Purpose:\nThe purpose of this machine is largely a mystery. It may have been a simple form of transportation for those who constructed it, or a resilient defense unit. Whatever its objective, it has failed.", "Guardian_Popup", "Guardian_Ency");

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
            MakeObjectScannable(prop_ruinedGuardian.TechType, "RuinedGuardian", 6f);
            #endregion

            outpostABTerminal = new DataTerminalPrefab("OutpostATerminal", "TertiaryOutpostData", new string[] { outpostCSignal.ClassID, outpostDSignal.ClassID });
            outpostABTerminal.Patch();


            var outpostAInitializer = new AlienBaseInitializer<OutpostBaseSpawner>("GargOutpostA", new Vector3(-702, -213, -780));
            outpostAInitializer.Patch();

            var outpostBInitializer = new AlienBaseInitializer<OutpostBaseSpawner>("GargOutpostB", Vector3.forward * -50f);
            outpostBInitializer.Patch();

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
