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
        public static DataTerminalPrefab peeperTerminalTest;

        private const string assetBundleName = "projectancientsassets";

        [QModPatch]
        public static void Patch()
        {
            assetBundle = ECCHelpers.LoadAssetBundleFromAssetsFolder(Assembly.GetExecutingAssembly(), assetBundleName);
            ECCAudio.RegisterClips(assetBundle);

            LanguageHandler.SetLanguageLine("EncyPath_Lifeforms/Fauna/Titans", "Titans");

            gargJuvenilePrefab = new GargantuanJuvenile("GargantuanJuvenile", "Gargantuan leviathan juvenile", "A titan-class lifeform. How did it get in your inventory?", assetBundle.LoadAsset<GameObject>("GargJuvenile_Prefab"), null);
            gargJuvenilePrefab.Patch();

            gargVoidPrefab = new GargantuanVoid("GargantuanVoid", "Gargantuan leviathan", "A titan-class lifeform. Indigineous to the void.", assetBundle.LoadAsset<GameObject>("GargJuvenile_Prefab"), null);
            gargVoidPrefab.Patch();

            var expRoar = new ExplosionRoarInitializer();
            expRoar.Patch();

            var adultGargSpawner = new AdultGargSpawnerInitializer();
            adultGargSpawner.Patch();

            #region Signals
            outpostCSignal = new GenericSignalPrefab("OutpostCSignal", "EggBasePingIcon", "OutpostC", "Outpost C", new Vector3(500f, 0f, 0f), 3);
            outpostCSignal.Patch();

            outpostDSignal = new GenericSignalPrefab("OutpostDSignal", "EggBasePingIcon", "OutpostD", "Outpost D", new Vector3(-500f, 0f, 0f), 3);
            outpostDSignal.Patch();
            #endregion

            #region Data download ency data
            PatchEncy("PrimaryOutpostData", "DownloadedData/Codes", "Primary Outpost Data", "This outpost contains coordinates for two secondary outposts.");
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
            #endregion

            peeperTerminalTest = new DataTerminalPrefab("OutpostATerminal", "PrimaryOutpostData", new string[] { outpostCSignal.ClassID, outpostDSignal.ClassID });
            peeperTerminalTest.Patch();


            var outpostInitializer = new OutpostBaseInitializer();
            outpostInitializer.Patch();

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

        static void PatchEncy(string key, string path, string title, string desc)
        {
            PDAEncyclopediaHandler.AddCustomEntry(new PDAEncyclopedia.EntryData()
            {
                key = key,
                path = path,
                nodes = path.Split('/')
            });
            LanguageHandler.SetLanguageLine("Ency_" + key, title);
            LanguageHandler.SetLanguageLine("EncyDesc_" + key, desc);
        }
    }
}
