namespace RotA
{
    using ArchitectsLibrary.Handlers;
    using Mono.AlienBaseSpawners;
    using Prefabs.AlienBase.Teleporter;
    using Prefabs.Initializers;
    using Prefabs;
    using Prefabs.AlienBase;
    using Prefabs.AlienBase.DataTerminal;
    using ECCLibrary;
    using UnityEngine;
    
    public partial class Mod
    {
        public static PrecursorDoorPrefab door_supplyCache;
        public static PrecursorDoorPrefab door_researchBase;
        public static PrecursorDoorPrefab door_kooshBase;
        public static PrecursorDoorPrefab whiteTabletDoor;
        
        public static GenericWorldPrefab secondaryBaseModel;
        public static GenericWorldPrefab secretBaseModel;
        public static VoidBaseModel voidBaseModel;
        public static GenericWorldPrefab guardianTailfinModel;
        public static AquariumSkeleton aquariumSkeleton;
        public static BlackHolePrefab blackHole;
        public static OmegaCubeFabricator omegaCubeFabricator;

        public static DataTerminalBuilder terminalBuilder = new();
        
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
        public static DataTerminalPrefab voidbaseSpamTerminal;
        public static DataTerminalPrefab eggRoomTerminal;
        public static DataTerminalPrefab warpCannonTerminal;
        public static DataTerminalPrefab precursorMasterTechTerminal;
        public static DataTerminalPrefab redTabletHolder;
        public static DataTerminalPrefab sonicDeterrentTerminal;

        public static SecretBaseTerminal devSecretTerminal;
        public static DataTerminalPrefab devTerminalAlan;
        public static DataTerminalPrefab devTerminalHipnox;
        public static DataTerminalPrefab devTerminalLee23;
        public static DataTerminalPrefab devTerminalMetious;
        public static DataTerminalPrefab devTerminalN8crafter;
        public static DataTerminalPrefab devTerminalSlendyPlayz;
        public static DataTerminalPrefab devTerminalTori;

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

            voidDoor_interior_left = new PrecursorDoorPrefab("VoidDoorInteriorL", "Door", redTabletTerminal.ClassID, "VoidDoorInterior", rootPrefabClassId: smallDoor, overrideTerminalPosition: true, terminalLocalPosition: new Vector3(-4f, 0f, -3f), terminalLocalRotation: Vector3.up * 90f);
            voidDoor_interior_left.Patch();

            voidDoor_interior_right = new PrecursorDoorPrefab("VoidDoorInteriorR", "Door", redTabletTerminal.ClassID, "VoidDoorInterior", rootPrefabClassId: smallDoor, overrideTerminalPosition: true, terminalLocalPosition: new Vector3(4f, 0f, -3f), terminalLocalRotation: Vector3.up * 270f);
            voidDoor_interior_right.Patch();

            prop_ruinedGuardian = new RuinedGuardianPrefab();
            prop_ruinedGuardian.Patch();
            MakeObjectScannable(prop_ruinedGuardian.TechType, ency_ruinedGuardian, 6f);

            secondaryBaseModel = new GenericWorldPrefab("SecondaryBaseModel", "Alien Structure", "A large alien structure.", assetBundle.LoadAsset<GameObject>("SmallCache_Prefab"), new UBERMaterialProperties(7f, 35f, 1f), LargeWorldEntity.CellLevel.Far);
            secondaryBaseModel.Patch();
            MakeObjectScannable(secondaryBaseModel.TechType, ency_secondaryBaseModel, 6f);

            secretBaseModel = new GenericWorldPrefab("SecretBaseModel", "Alien Structure", "A large alien structure. (Lee23 7/9/2021 6:59 PM EST)", assetBundle.LoadAsset<GameObject>("SecretRoom_Prefab"), new UBERMaterialProperties(8f, 3f, 1f), LargeWorldEntity.CellLevel.Far);
            secretBaseModel.Patch();

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

            bladeRelic = new AlienRelicPrefab("PrecursorBladeRelic", "Alien Knife", "An alien knife.", assetBundle.LoadAsset<GameObject>("PrecursorBlade_Prefab"), 0.8f, true);
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

            //supply cache (Crag field)
            var supplyCachePos = new Vector3(-13, -175.81f, -1183);

            var supplyCacheBase = new AlienBaseInitializer<SupplyCacheBaseSpawner>("SupplyCacheBase", supplyCachePos);
            supplyCacheBase.Patch();

            var supplyCacheExterior = new AlienBaseInitializer<CacheBaseExteriorSpawner>("SupplyCacheBaseExterior", supplyCachePos, LargeWorldEntity.CellLevel.Far);
            supplyCacheExterior.Patch();

            //Research base (Sparse reef)
            var researchBasePos = new Vector3(-860, -187, -641);

            var researchBase = new AlienBaseInitializer<ResearchBaseSpawner>("ResearchBase", researchBasePos);
            researchBase.Patch();

            var researchBaseExterior = new AlienBaseInitializer<CacheBaseExteriorSpawner>("ResearchBaseExterior", researchBasePos, LargeWorldEntity.CellLevel.Far);
            researchBaseExterior.Patch();

            //Koosh zone base (Koosh/bulb zone)
            var kooshBasePos = new Vector3(1480, -457, 1457);

            var kooshBase = new AlienBaseInitializer<KooshBaseSpawner>("KooshZoneBase", kooshBasePos);
            kooshBase.Patch();

            var kooshBaseExterior = new AlienBaseInitializer<CacheBaseExteriorSpawner>("KooshZoneBaseExterior", kooshBasePos, LargeWorldEntity.CellLevel.Far);
            kooshBaseExterior.Patch();

            //Void base
            var voidBasePos = new Vector3(373, -400, -1920);

            var voidBase = new AlienBaseInitializer<VoidBaseSpawner>("VoidBase", voidBasePos, LargeWorldEntity.CellLevel.Far); //Void
            voidBase.Patch();

            var voidBaseInterior = new AlienBaseInitializer<VoidBaseInteriorSpawner>("VoidBaseInterior", voidBasePos, LargeWorldEntity.CellLevel.Medium); //Void
            voidBaseInterior.Patch();

            var secondaryContainmentFacility = new AlienBaseInitializer<SecondaryContainmentFacility>("SecondaryContaimentFacility", new Vector3(-1088, -1440, 192), LargeWorldEntity.CellLevel.Far); //Dunes (Out of bounds)
            secondaryContainmentFacility.Patch();

            var secretBase = new AlienBaseInitializer<SecretBaseSpawner>("SecretBaseSpawner", new Vector3(1500f, -2000f, 0f), LargeWorldEntity.CellLevel.Far); //Under aurora (Out of bounds)
            secretBase.Patch();
        }

        #endregion

        #region Alien Terminals

        static void PatchAlienTerminals()
        {
            #region Outposts
            terminalBuilder.SetupStoryGoal(ency_tertiaryOutpostTerminalGrassy);
            terminalBuilder.SetupPingClassIds(new[] { signal_cragFieldBase.ClassID, signal_sparseReefBase.ClassID, signal_kooshZoneBase.ClassID });
            terminalBuilder.SetupAudio("DataTerminalOutpost", "Detecting an alien broadcast. Uploading co-ordinates to PDA.");
            tertiaryOutpostTerminalGrassy = new DataTerminalPrefab("TertiaryOutpostTerminal1", terminalBuilder.GetTerminal());
            tertiaryOutpostTerminalGrassy.Patch();

            terminalBuilder.SetupStoryGoal(ency_tertiaryOutpostTerminalSparseReef);
            terminalBuilder.SetupPingClassIds(new[] { signal_cragFieldBase.ClassID, signal_sparseReefBase.ClassID, signal_kooshZoneBase.ClassID });
            tertiaryOutpostTerminalSparseReef = new DataTerminalPrefab("TertiaryOutpostTerminal2", terminalBuilder.GetTerminal());
            tertiaryOutpostTerminalSparseReef.Patch();

            terminalBuilder.SetupStoryGoal(ency_tertiaryOutpostTerminalLostRiver);
            terminalBuilder.SetupPingClassIds(new[] { signal_cragFieldBase.ClassID, signal_sparseReefBase.ClassID, signal_kooshZoneBase.ClassID });
            tertiaryOutpostTerminalLostRiver = new DataTerminalPrefab("TertiaryOutpostTerminal3", terminalBuilder.GetTerminal());
            tertiaryOutpostTerminalLostRiver.Patch();
            #endregion

            #region Cache bases
            terminalBuilder.SetupStoryGoal(ency_distressSignal, 6);
            terminalBuilder.SetupPingClassIds(new [] { signal_ruinedGuardian.ClassID });
            terminalBuilder.SetupAudio("DataTerminalDistress", "Detecting an alien distress broadcast. Uploading co-ordinates to PDA.");
            guardianTerminal = new DataTerminalPrefab("GuardianTerminal", terminalBuilder.GetTerminal());
            guardianTerminal.Patch();

            terminalBuilder.SetupStoryGoal(ency_supplyCacheTerminal);
            terminalBuilder.SetupTemplateTerminal(DataTerminalPrefab.greenTerminalCID);
            terminalBuilder.SetupAudio("DataTerminalEncy", "Downloading alien data... Download complete.");
            supplyCacheTerminal = new DataTerminalPrefab("SupplyCacheTerminal", terminalBuilder.GetTerminal());
            supplyCacheTerminal.Patch();

            terminalBuilder.SetupStoryGoal(ency_researchBaseTerminal);
            terminalBuilder.SetupTemplateTerminal(DataTerminalPrefab.greenTerminalCID);
            terminalBuilder.SetupAudio("DataTerminalEncy", "Downloading alien data... Download complete.");
            researchBaseTerminal = new DataTerminalPrefab("ResearchBaseTerminal", terminalBuilder.GetTerminal());
            researchBaseTerminal.Patch();

            terminalBuilder.SetupStoryGoal(ency_kooshBaseTerminal);
            terminalBuilder.SetupTemplateTerminal(DataTerminalPrefab.greenTerminalCID);
            terminalBuilder.SetupAudio("DataTerminalEncy", "Downloading alien data... Download complete.");
            kooshBaseTerminal = new DataTerminalPrefab("KooshBaseTerminal", terminalBuilder.GetTerminal());
            kooshBaseTerminal.Patch();

            terminalBuilder.SetupTemplateTerminal(DataTerminalPrefab.orangeTerminalCID);
            terminalBuilder.SetupUnlockables(new[] { superDecoy.TechType, exosuitDashModule.TechType, exosuitZapModule.TechType, ionKnife.TechType, electricalDefenseMk2.TechType }, delay: 4.6f);
            terminalBuilder.SetupAudio("DataTerminalIonicPulse", "Synthesizing Ionic Energy Pulse blueprints from alien data. Blueprints stored to databank.");
            archElectricityTerminal = new DataTerminalPrefab("ArchElectricityTerminal", terminalBuilder.GetTerminal());
            archElectricityTerminal.Patch();

            terminalBuilder.SetupTemplateTerminal(DataTerminalPrefab.orangeTerminalCID);
            terminalBuilder.SetupUnlockables(techTypeToAnalyze: AUHandler.AlienTechnologyMasterTech);
            precursorMasterTechTerminal = new DataTerminalPrefab("MasterTechTerminal", terminalBuilder.GetTerminal());
            precursorMasterTechTerminal.Patch();

            terminalBuilder.SetupFX(new Color(1f, 0.5f, 0.5f), true);
            terminalBuilder.SetupInteractable(false);
            redTabletHolder = new DataTerminalPrefab("RedTabletHolder", terminalBuilder.GetTerminal());
            redTabletHolder.Patch();
            #endregion

            #region Voidbase
            terminalBuilder.SetupStoryGoal(ency_voidBaseTerminal);
            terminalBuilder.SetupTemplateTerminal(DataTerminalPrefab.greenTerminalCID);
            terminalBuilder.SetupAudio("DataTerminalEncy", "Downloading alien data... Download complete.");
            voidBaseTerminal = new DataTerminalPrefab("VoidBaseTerminal", terminalBuilder.GetTerminal());
            voidBaseTerminal.Patch();

            terminalBuilder.SetupStoryGoal(ency_cachePings);
            terminalBuilder.SetupPingClassIds(new[] { signal_cache_bloodKelp.ClassID, signal_cache_sparseReef.ClassID, signal_cache_dunes.ClassID, signal_cache_lostRiver.ClassID });
            terminalBuilder.SetupTemplateTerminal(DataTerminalPrefab.blueTerminalCID);
            terminalBuilder.SetupAudio("DataTerminalOutpost", "Detecting an alien broadcast. Uploading co-ordinates to PDA.");
            cachePingsTerminal = new DataTerminalPrefab("CachePingsTerminal", terminalBuilder.GetTerminal());
            cachePingsTerminal.Patch();

            terminalBuilder.SetupStoryGoal(ency_alienSpam);
            terminalBuilder.SetupTemplateTerminal(DataTerminalPrefab.greenTerminalCID);
            terminalBuilder.SetupAudio("DataTerminalEncy", "Downloading alien data... Download complete.");
            voidbaseSpamTerminal = new DataTerminalPrefab("SpamTerminal", terminalBuilder.GetTerminal());
            voidbaseSpamTerminal.Patch();

            terminalBuilder.SetupStoryGoal(ency_eggRoom);
            terminalBuilder.SetupTemplateTerminal(DataTerminalPrefab.greenTerminalCID);
            terminalBuilder.SetupAudio("DataTerminalEncy", "Downloading alien data... Download complete.");
            eggRoomTerminal = new DataTerminalPrefab("EggRoomTerminal", terminalBuilder.GetTerminal());
            eggRoomTerminal.Patch();
            
            terminalBuilder.SetupStoryGoal(ency_warpCannonTerminal);
            terminalBuilder.SetupTemplateTerminal(DataTerminalPrefab.orangeTerminalCID);
            terminalBuilder.SetupUnlockables(techTypeToAnalyze: warpMasterTech);
            terminalBuilder.SetupAudio("DataTerminalEncy", "Downloading alien data... Download complete.");
            warpCannonTerminal = new DataTerminalPrefab("WarpCannonTerminal", terminalBuilder.GetTerminal());
            warpCannonTerminal.Patch();

            terminalBuilder.SetupStoryGoal(ency_buildablesonicdeterrent);
            terminalBuilder.SetupTemplateTerminal(DataTerminalPrefab.orangeTerminalCID);
            terminalBuilder.SetupUnlockables(techTypesToUnlock: new[] { AUHandler.BuildableSonicDeterrentTechType }, delay: 4f);
            terminalBuilder.SetupAudio("DataTerminalEncy", "Downloading alien data... Download complete.");
            sonicDeterrentTerminal = new DataTerminalPrefab("SonicDeterrentTerminal", terminalBuilder.GetTerminal());
            sonicDeterrentTerminal.Patch();
            #endregion

            #region Secret base
            terminalBuilder.SetupTemplateTerminal(DataTerminalPrefab.orangeTerminalCID);
            terminalBuilder.SetupFX(new Color(0.65f, 0f, 0.75f), false);
            terminalBuilder.SetupAchievement("DevSecretAchievement");
            terminalBuilder.SetupAudio("PDAThanksForDownloading", "Thank you for downloading the Return of the Ancients mod. The Architects of the Unknown team appreciates your support.");
            devSecretTerminal = new SecretBaseTerminal("DevSecretTerminal", terminalBuilder.GetTerminal());
            devSecretTerminal.Patch();

            devTerminalAlan = GetDevNameTerminal("DevSecretAlan", "PDAAlan", "Al-An", new Color(1f, 0f, 0.75f));
            devTerminalAlan.Patch();

            devTerminalHipnox = GetDevNameTerminal("DevSecretHipnox", "PDAHipnox", "Hipnox", new Color(0.28f, 0.69f, 1f));
            devTerminalHipnox.Patch();

            devTerminalLee23 = GetDevNameTerminal("DevSecretLee23", "PDALee23", "Lee23", new Color(0.33f, 1f, 0.64f));
            devTerminalLee23.Patch();

            devTerminalMetious = GetDevNameTerminal("DevSecretMetious", "PDAMetious", "Metious", Color.magenta);
            devTerminalMetious.Patch();

            devTerminalN8crafter = GetDevNameTerminal("DevSecretN8", "PDAN8Crafter", "N8Crafter", Color.cyan);
            devTerminalN8crafter.Patch();

            devTerminalSlendyPlayz = GetDevNameTerminal("DevSecretSlendy", "PDASlendyPlayz", "SlendyPlayz", new Color(0.8f, 0.8f, 0.8f));
            devTerminalSlendyPlayz.Patch();

            devTerminalTori = GetDevNameTerminal("DevSecretTori", "PDATori", "Tori Chibi", new Color(1f, 0f, 0f));
            devTerminalTori.Patch();
            #endregion
        }

        static DataTerminalPrefab GetDevNameTerminal(string classId, string audioFile, string name, Color color)
        {
            terminalBuilder.SetupTemplateTerminal(DataTerminalPrefab.greenTerminalCID);
            terminalBuilder.SetupFX(color, false);
            terminalBuilder.SetupAudio(audioFile, name);
            return new DataTerminalPrefab(classId, terminalBuilder.GetTerminal());
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

            var kooshBaseDevSecret = new TeleporterNetwork("KooshBaseDevSecret", new Vector3(1480 + 2.2f, -457 - 0.4f - 2.89f, 1457 - 14f), 0f, new Vector3(1500f, -2000f, 0f), 180f, false, false, new TeleporterPrimaryPrefab.CustomItemSettings(new TechType[] { AUHandler.OmegaCubeTechType }, "KooshBasePortalTerminal", "Insert omega cube"), "SecretBaseAuxiliary");
            kooshBaseDevSecret.SetNetworkColor(new Color(4, 4, 11));
            kooshBaseDevSecret.Patch();
        }

        #endregion
    }
}