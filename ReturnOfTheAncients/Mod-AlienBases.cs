namespace RotA
{
    using ArchitectsLibrary.Handlers;
    using Mono.AlienBaseSpawners;
    using Prefabs.AlienBase.Teleporter;
    using Prefabs.Initializers;
    using Prefabs;
    using Prefabs.AlienBase;
    using ECCLibrary;
    using UnityEngine;
    
    public partial class Mod
    {
        public static PrecursorDoorPrefab door_supplyCache;
        public static PrecursorDoorPrefab door_researchBase;
        public static PrecursorDoorPrefab door_kooshBase;
        public static PrecursorDoorPrefab whiteTabletDoor;
        
        public static GenericWorldPrefab secondaryBaseModel;
        public static VoidBaseModel voidBaseModel;
        public static GenericWorldPrefab guardianTailfinModel;
        public static AquariumSkeleton aquariumSkeleton;
        public static BlackHolePrefab blackHole;
        public static OmegaCubeFabricator omegaCubeFabricator;

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

        public static VoidInteriorForcefield voidInteriorForcefield;
        public static PrecursorDoorPrefab voidDoor_red;
        public static PrecursorDoorPrefab voidDoor_white;
        public static PrecursorDoorPrefab voidDoor_blue;
        public static PrecursorDoorPrefab voidDoor_purple;
        public static PrecursorDoorPrefab voidDoor_orange;
        public static PrecursorDoorPrefab voidDoor_interior_left;
        public static PrecursorDoorPrefab voidDoor_interior_right;
        public static PrecursorDoorPrefab voidDoor_interior_infectionTest;
        
        static TabletTerminalPrefab purpleTabletTerminal;
        static TabletTerminalPrefab redTabletTerminal;
        static TabletTerminalPrefab whiteTabletTerminal;
        static TabletTerminalPrefab orangeTabletTerminal;
        static TabletTerminalPrefab blueTabletTerminal;
        static InfectionTesterTerminal infectionTesterTerminal;

        static RuinedGuardianPrefab prop_ruinedGuardian;
        
        #region Alien Base Prefabs
        
        static void PatchAlienBasePrefabs()
        {
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
        }

        #endregion

        #region Alien Bases

        static void PatchAlienBases()
        {
            var outpostAInitializer = new AlienBaseInitializer<OutpostBaseSpawner>("GargOutpostA", new Vector3(-702, -213, -780)); //Sparse reef
            outpostAInitializer.Patch();

            var outpostBInitializer = new AlienBaseInitializer<BonesFieldsOutpostSpawner>("GargOutpostB", new Vector3(-726, -757, -218)); //Bones fields
            outpostBInitializer.Patch();

            var guardianCablesInitializer = new AlienBaseInitializer<CablesNearGuardian>("GuardianCables", new Vector3(373, -358, -1762)); //Crag field
            guardianCablesInitializer.Patch();

            var supplyCacheBase = new AlienBaseInitializer<SupplyCacheBaseSpawner>("SupplyCacheBase", new Vector3(-13, -175.81f, -1183)); //Crag field
            supplyCacheBase.Patch();

            var researchBase = new AlienBaseInitializer<ResearchBaseSpawner>("ResearchBase", new Vector3(-860, -187, -641)); //Sparse reef
            researchBase.Patch();

            var kooshBase = new AlienBaseInitializer<KooshBaseSpawner>("KooshZoneBase", new Vector3(1480, -457, 1457)); //Koosh/bulb zone
            kooshBase.Patch();

            var eggBase = new AlienBaseInitializer<VoidBaseSpawner>("VoidBase", new Vector3(373, -400, -1880 - 40f), 300f, LargeWorldEntity.CellLevel.Far); //Void
            eggBase.Patch();

            var eggBaseInterior = new AlienBaseInitializer<VoidBaseInteriorSpawner>("VoidBaseInterior", new Vector3(373, -400, -1880 - 40f), 90, LargeWorldEntity.CellLevel.Medium); //Void
            eggBaseInterior.Patch();

            var secondaryContainmentFacility = new AlienBaseInitializer<SecondaryContainmentFacility>("SecondaryContaimentFacility", new Vector3(-1088, -1440, 192), 350f, LargeWorldEntity.CellLevel.Far); //Dunes (Out of bounds)
            secondaryContainmentFacility.Patch();
        }

        #endregion

        #region Alien Terminals

        static void PatchAlienTerminals()
        {
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
        }

        #endregion

        #region Teleporters

        static void PatchTeleporters()
        {
            var voidPcfNetwork = new TeleporterNetwork("VoidBasePCF", new Vector3(373, -400 + 18f - 0.5f, -1880 - 40f - 55f), 0f, new Vector3(321.88f, -1438.50f, -393.03f), 240f, true, false);
            voidPcfNetwork.Patch();

            var voidWeaponsNetwork = new TeleporterNetwork("VoidBaseWeaponsBase", new Vector3(373 - 50f, -400, -1880 - 40f - 10f), 0f, new Vector3(-857.80f, -189.89f - 0.4f, -641.00f - 14f), 0f, false, true);
            voidWeaponsNetwork.Patch();

            var voidSupplyNetwork = new TeleporterNetwork("VoidBaseSupplyCache", new Vector3(373 + 50f, -400, -1880 - 40f - 10f), 0f, new Vector3(-10.80f, -178.50f - 0.4f, -1183.00f - 14f), 0f, false, true);
            voidSupplyNetwork.Patch();

            var secretTeleporter = new TeleporterNetwork("SCFSecretTeleporter", new Vector3(218f, -1376, -260f), 150f, new Vector3(-959, -1440, 76f), 206f, false, false);
            secretTeleporter.Patch();

            var kooshBaseDevSecret = new TeleporterNetwork("KooshBaseDevSecret", new Vector3(1480 + 2.2f, -457 - 0.4f - 2.89f, 1457 - 14f), 0f, new Vector3(0f, -500f, 0f), 0f, false, true, true, new TeleporterPrimaryPrefab.CustomItemSettings(new TechType[] { omegaCube.TechType }, "KooshBasePortalTerminal", "Insert omega cube"));
            kooshBaseDevSecret.Patch();
        }

        #endregion
    }
}