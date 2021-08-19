namespace RotA
{
    using System.Runtime.InteropServices;
    using FMOD;
    using FMODUnity;
    using ArchitectsLibrary.Handlers;
    using ArchitectsLibrary.API;
    using ECCLibrary;
    using HarmonyLib;
    using QModManager.API.ModLoading;
    using Commands;
    using Patches;
    using Prefabs;
    using Prefabs.AlienBase;
    using Prefabs.AlienBase.DataTerminal;
    using Prefabs.Creatures;
    using Prefabs.Creatures.Skeletons;
    using Prefabs.Equipment;
    using Prefabs.Initializers;
    using Prefabs.Modules;
    using Prefabs.Placeable;
    using Prefabs.Signals;
    using SMLHelper.V2.Crafting;
    using SMLHelper.V2.Handlers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEngine;
    using UWE;
    using Debug = UnityEngine.Debug;
    
    [QModCore]
    public static partial class Mod
    {
        public static AssetBundle assetBundle;
        public static AssetBundle gargAssetBundle;
        
        public static GargantuanJuvenile gargJuvenilePrefab;
        public static GargantuanVoid gargVoidPrefab;
        public static GargantuanBaby gargBabyPrefab;
        public static SkeletonGarg spookySkeletonGargPrefab;
        public static GargantuanEgg gargEgg;
        public static AquariumGuppy aquariumGuppy;
        public static Guardian guardian;
        
        public static GhostSkeletonPose1 ghostSkeletonPose1;
        public static GhostSkeletonPose2 ghostSkeletonPose2;

        public static GameObject electricalDefensePrefab;
        static SeamothElectricalDefenseMK2 electricalDefenseMk2;
        static ExosuitZapModule exosuitZapModule;
        static ExosuitDashModule exosuitDashModule;
        static SuperDecoy superDecoy;

        static GenericSignalPrefab signal_cragFieldBase;
        static GenericSignalPrefab signal_sparseReefBase;
        static GenericSignalPrefab signal_kooshZoneBase;
        static GenericSignalPrefab signal_ruinedGuardian;
        static GenericSignalPrefab signal_cache_bloodKelp;
        static GenericSignalPrefab signal_cache_sparseReef;
        static GenericSignalPrefab signal_cache_dunes;
        static GenericSignalPrefab signal_cache_lostRiver;
        
        public static AtmosphereVolumePrefab precursorAtmosphereVolume;

        public static AlienRelicPrefab ingotRelic;
        public static AlienRelicPrefab rifleRelic;
        public static AlienRelicPrefab bladeRelic;
        public static AlienRelicPrefab builderRelic;

        internal static GargPoster gargPoster;
        internal static GargantuanAdultToy gargAdultToy;
        internal static GargantuanAdultToyNoHat gargAdultToyNoHat;
        internal static GargantuanJuvenileToy gargJuvenileToy;

        public static WarpCannonPrefab warpCannon;
        public static IonKnifePrefab ionKnife;

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
        private const string gargAssetBundleName = "gargantuanassets";
        
        private const string alienSignalName = "Alien Signal";

        public static Config config = OptionsPanelHandler.Main.RegisterModOptions<Config>();

        private static Assembly myAssembly = Assembly.GetExecutingAssembly();

        [QModPatch]
        public static void Patch()
        {
            assetBundle = ECCHelpers.LoadAssetBundleFromAssetsFolder(Assembly.GetExecutingAssembly(), assetBundleName);
            ECCAudio.RegisterClips(assetBundle);

            gargAssetBundle = ECCHelpers.LoadAssetBundleFromAssetsFolder(Assembly.GetExecutingAssembly(), gargAssetBundleName);
            ECCAudio.RegisterClips(gargAssetBundle);

            CraftDataHandler.SetTechData(TechType.RocketStage2, new() { craftAmount = 1, Ingredients = new List<Ingredient>() { new(TechType.PlasteelIngot, 1), new(TechType.Sulphur, 4), new(TechType.Kyanite, 4), new(TechType.PrecursorIonPowerCell, 1), new(AUHandler.OmegaCubeTechType, 1) } });

            #region Static asset references
            CoroutineHost.StartCoroutine(LoadElectricalDefensePrefab());
            #endregion

            var sound = CreateSoundFromAudioClip(assetBundle.LoadAsset<AudioClip>("PDAExplosionRoar"));

            if (sound.HasValue)
                CustomSoundHandler.RegisterCustomSound("explosionSoundTest", sound.Value);
            
            ConsoleCommandsHandler.Main.RegisterConsoleCommand("soundtest", typeof(Mod), nameof(SoundTest));
            ConsoleCommandsHandler.Main.RegisterConsoleCommands(typeof(RotACommands));

            PatchLanguage();

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
            
            PatchCraftablesAndBuildables();

            PatchCreatures();

            InitSpawns();
            
            PatchInitializers();

            PatchSignals();
            
            PatchEncys();

            PatchAlienBasePrefabs();

            PatchAlienTerminals();

            PatchTeleporters();

            PatchAlienBases();

            CraftDataHandler.SetItemSize(TechType.PrecursorKey_White, new Vector2int(1, 1));
            CraftDataHandler.AddToGroup(TechGroup.Personal, TechCategory.Equipment, TechType.PrecursorKey_Red);
            CraftDataHandler.AddToGroup(TechGroup.Personal, TechCategory.Equipment, TechType.PrecursorKey_White);
            CraftDataHandler.SetTechData(TechType.RocketStage2, new TechData() { craftAmount = 1, Ingredients = new List<Ingredient>() { new Ingredient(TechType.PlasteelIngot, 1), new Ingredient(TechType.Sulphur, 4), new Ingredient(TechType.Kyanite, 4), new Ingredient(TechType.PrecursorIonPowerCell, 1), new Ingredient(AUHandler.OmegaCubeTechType, 1) } });
            CraftTreeHandler.AddCraftingNode(CraftTree.Type.Fabricator, TechType.PrecursorKey_White, "Personal", "Equipment");
            CraftTreeHandler.AddCraftingNode(CraftTree.Type.Fabricator, TechType.PrecursorKey_Red, "Personal", "Equipment");

            Harmony harmony = new Harmony($"ArchitectsOfTheUnknown_{myAssembly.GetName().Name}");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            FixMapModIfNeeded(harmony);
        }
        
        [QModPostPatch]
        public static void PostPatch()
        {
            //post patch because decorations mod does stuff to this recipe
            var redTabletTD = new TechData
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>
                {
                    new(TechType.PrecursorIonCrystal, 1),
                    new(TechType.AluminumOxide, 2)
                }
            };
            CraftDataHandler.SetTechData(TechType.PrecursorKey_Red, redTabletTD);
            var whiteTabletTD = new TechData
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>
                {
                    new(TechType.PrecursorIonCrystal, 1),
                    new(TechType.Silver, 2)
                }
            };
            CraftDataHandler.SetTechData(TechType.PrecursorKey_White, whiteTabletTD);
        }

        static void PatchCreatures()
        {
            gargJuvenilePrefab = new GargantuanJuvenile("GargantuanJuvenile", "Gargantuan Leviathan Juvenile", "A titan-class lifeform. How did it get in your inventory?", gargAssetBundle.LoadAsset<GameObject>("GargJuvenile_Prefab"), null);
            gargJuvenilePrefab.Patch();

            gargVoidPrefab = new GargantuanVoid("GargantuanVoid", "Gargantuan Leviathan", "A titan-class lifeform. Indigineous to the void.", gargAssetBundle.LoadAsset<GameObject>("GargAdult_Prefab"), null);
            gargVoidPrefab.Patch();

            gargBabyPrefab = new GargantuanBaby("GargantuanBaby", "Gargantuan Baby", "A very young specimen, raised in containment. Playful.", gargAssetBundle.LoadAsset<GameObject>("GargBaby_Prefab"), gargAssetBundle.LoadAsset<Texture2D>("GargantuanBaby_Icon"));
            gargBabyPrefab.Patch();

            spookySkeletonGargPrefab = new SkeletonGarg("SkeletonGargantuan", "Gargantuan Skeleton", "Spooky.", gargAssetBundle.LoadAsset<GameObject>("SkeletonGarg_Prefab"), null);
            spookySkeletonGargPrefab.Patch();

            gargEgg = new GargantuanEgg();
            gargEgg.Patch();

            aquariumGuppy = new AquariumGuppy("AquariumGuppy", "Unknown Fish", "An interesting fish.", assetBundle.LoadAsset<GameObject>("AquariumGuppy"), null);
            aquariumGuppy.Patch();

            guardian = new Guardian("Guardian", "Guardian", "Guardian that makes me go yes", assetBundle.LoadAsset<GameObject>("GuardianCreature_Prefab"), null);
            guardian.Patch();

            var g = new Guardian("Guardian2", "Guardian 2", "Guardian that makes me go yes", assetBundle.LoadAsset<GameObject>("GuardianCreature2_Prefab"), null);
            g.Patch();

            ghostSkeletonPose1 = new();
            ghostSkeletonPose1.Patch();

            ghostSkeletonPose2 = new();
            ghostSkeletonPose2.Patch();
        }

        static void InitSpawns()
        {
            CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(gargJuvenilePrefab.TechType, new Vector3(1245, -40, -716)));
            CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(gargJuvenilePrefab.TechType, new Vector3(1450, -100, 180)));
            CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(gargJuvenilePrefab.TechType, new Vector3(-1386, -117, 346)));
            CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(gargJuvenilePrefab.TechType, new Vector3(-1244, -117, 989)));
            CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(gargJuvenilePrefab.TechType, new Vector3(-1122, -117, 1420)));
            CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(gargJuvenilePrefab.TechType, new Vector3(-1370, -60, -29f)));
            CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(gargJuvenilePrefab.TechType, new Vector3(1430, -300, 1547)));
        }

        static void PatchInitializers()
        {
            var expRoar = new ExplosionRoarInitializer();
            expRoar.Patch();

            var adultGargSpawner = new AdultGargSpawnerInitializer();
            adultGargSpawner.Patch();

            var miscInitializers = new MiscInitializers();
            miscInitializers.Patch();
        }

        static void PatchCraftablesAndBuildables()
        {
            warpCannon = new();
            warpCannon.Patch();
            PrecursorFabricatorService.SubscribeToFabricator(warpCannon.TechType, PrecursorFabricatorTab.Equipment);
            DisplayCaseServices.WhitelistTechType(warpCannon.TechType);

            ionKnife = new();
            ionKnife.Patch();
            PrecursorFabricatorService.SubscribeToFabricator(ionKnife.TechType, PrecursorFabricatorTab.Equipment);
            DisplayCaseServices.WhitelistTechType(ionKnife.TechType);

            gargPoster = new();
            gargPoster.Patch();
            KnownTechHandler.SetAnalysisTechEntry(gargPoster.TechType, new List<TechType>() { gargPoster.TechType });

            gargAdultToy = new();
            gargAdultToy.Patch();

            gargAdultToyNoHat = new();
            gargAdultToyNoHat.Patch();

            gargJuvenileToy = new();
            gargJuvenileToy.Patch();

            electricalDefenseMk2 = new();
            electricalDefenseMk2.Patch();
            PrecursorFabricatorService.SubscribeToFabricator(electricalDefenseMk2.TechType, PrecursorFabricatorTab.UpgradeModules);
            DisplayCaseServices.WhitelistTechType(electricalDefenseMk2.TechType);

            exosuitZapModule = new();
            exosuitZapModule.Patch();
            PrecursorFabricatorService.SubscribeToFabricator(exosuitZapModule.TechType, PrecursorFabricatorTab.UpgradeModules);
            DisplayCaseServices.WhitelistTechType(exosuitZapModule.TechType);

            superDecoy = new();
            superDecoy.Patch();
            PrecursorFabricatorService.SubscribeToFabricator(superDecoy.TechType, PrecursorFabricatorTab.Devices);
            DisplayCaseServices.WhitelistTechType(superDecoy.TechType);
            DisplayCaseServices.SetScaleInRelicTank(superDecoy.TechType, 0.7f);

            exosuitDashModule = new();
            exosuitDashModule.Patch();
            PrecursorFabricatorService.SubscribeToFabricator(exosuitDashModule.TechType, PrecursorFabricatorTab.UpgradeModules);
            DisplayCaseServices.WhitelistTechType(exosuitDashModule.TechType);
        }

        static void PatchSignals()
        {
            signal_cragFieldBase = new("OutpostCSignal", "Precursor_Symbol04", "Downloaded co-ordinates", alienSignalName, new Vector3(3, -173, -1069), 5, new(true, "SupplyCacheBaseSubtitle", "SupplyCacheBaseEncounter", "Biological signal interference detected. True signal source is likely to be somewhere in the area.", Mod.assetBundle.LoadAsset<AudioClip>("PDAKooshZoneBaseEncounter"), 2f)); //white tablet icon
            signal_cragFieldBase.Patch();

            signal_sparseReefBase = new("OutpostDSignal", "Precursor_Symbol01", "Downloaded co-ordinates", alienSignalName, new Vector3(-617, -182f, -598f), 5, new(true, "ResearchBaseSubtitle", "ResearchBaseEncounter", "Biological signal interference detected. True signal source is likely to be somewhere in the area.", Mod.assetBundle.LoadAsset<AudioClip>("PDAKooshZoneBaseEncounter"), 2f)); //red tablet icon
            signal_sparseReefBase.Patch();

            signal_kooshZoneBase = new("KooshZoneBaseSignal", "Precursor_Symbol05", "Downloaded co-ordinates", alienSignalName, new Vector3(1489, -420, 1337), 5, new(true, "KooshBaseSignalSubtitle", "KooshBaseEncounter", "Biological signal interference detected. True signal source is likely to be somewhere in the area.", Mod.assetBundle.LoadAsset<AudioClip>("PDAKooshZoneBaseEncounter"), 2f)); //purple tablet icon
            signal_kooshZoneBase.Patch();

            signal_ruinedGuardian = new("RuinedGuardianSignal", "RuinedGuardian_Ping", "Unidentified tracking chip", "Distress signal", new Vector3(367, -333, -1747), 0, new(true, "GuardianEncounterSubtitle", "GuardianEncounter", "This machine appears to have recently collapsed to the seafloor. Further research required.", Mod.assetBundle.LoadAsset<AudioClip>("PDAGuardianEncounter"), 3f));
            signal_ruinedGuardian.Patch();

            signal_cache_bloodKelp = new("BloodKelpCacheSignal", "CacheSymbol1", "Blood Kelp Zone Sanctuary", alienSignalName + " (535m)", new Vector3(-554, -534, 1518), defaultColorIndex: 2);
            signal_cache_bloodKelp.Patch();

            signal_cache_sparseReef = new("SparseReefCacheSignal", "CacheSymbol2", "Deep Sparse Reef Sanctuary", alienSignalName + " (287m)", new Vector3(-929, -287, -760), defaultColorIndex: 1);
            signal_cache_sparseReef.Patch();

            signal_cache_dunes = new("DunesCacheSignal", "CacheSymbol3", "Dunes Sanctuary", alienSignalName + " (380m)", new Vector3(-1187, -378, 1130), defaultColorIndex: 4);
            signal_cache_dunes.Patch();

            signal_cache_lostRiver = new("LostRiverCacheSignal", "CacheSymbol4", "Lost River Laboratory Cache", alienSignalName + " (685m)", new Vector3(-1111, -685, -655), defaultColorIndex: 3);
            signal_cache_lostRiver.Patch();
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
            renderer.material.SetTexture("_MainTex", assetBundle.LoadAsset<Texture2D>("alienupgrademodule_diffuse"));
            renderer.material.SetTexture("_SpecTex", assetBundle.LoadAsset<Texture2D>("alienupgrademodule_spec"));
            renderer.material.SetTexture("_Illum", assetBundle.LoadAsset<Texture2D>("alienupgrademodule_illum"));
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
        
        static Sound? CreateSoundFromAudioClip(AudioClip audioClip)
        {
            var samplesSize = audioClip.samples * audioClip.channels;
            var samples = new float[samplesSize];
            audioClip.GetData(samples, 0);

            var bytesLength = (uint) (samplesSize * sizeof(float));

            var soundInfo = new CREATESOUNDEXINFO
            {
                cbsize = Marshal.SizeOf(typeof(CREATESOUNDEXINFO)),
                length = bytesLength,
                format = SOUND_FORMAT.PCMFLOAT,
                defaultfrequency = audioClip.frequency,
                numchannels = audioClip.channels
            };

            Sound sound;
            var result = RuntimeManager.LowlevelSystem.createSound("", MODE.OPENUSER, ref soundInfo, out sound);
            if (result == RESULT.OK)
            {
                IntPtr ptr1, ptr2;
                uint len1, len2;
                result = sound.@lock(0, bytesLength, out ptr1, out ptr2, out len1, out len2);
                if (result == RESULT.OK)
                {
                    var samplesLength = (int) (len1 / sizeof(float));
                    Marshal.Copy(samples, 0, ptr1, samplesLength);
                    if (len2 > 0)
                    {
                        Marshal.Copy(samples, samplesLength, ptr2, (int) (len2 / sizeof(float)));
                    }

                    result = sound.unlock(ptr1, ptr2, len1, len2);
                    if (result == RESULT.OK)
                    {
                        result = sound.setMode(MODE.DEFAULT);
                        if (result == RESULT.OK)
                        {
                            Debug.Log("Successfully loaded fmod sound!");
                            return sound;
                        }
                        else
                        {
                            Debug.LogError("Failed to set mode to loop for fmod sound with error: " + result);
                        }
                    }
                    else
                    {
                        Debug.LogError("Failed to unlock fmod sound with error: " + result);
                    }
                }
                else
                {
                    Debug.LogError("Failed to lock fmod sound with error: " + result);
                }
            }
            else
            {
                Debug.LogError("Failed to load fmod sound with error: " + result);
            }

            return null;
        }

        static void SoundTest()
        {
            
            FMODUWE.PlayOneShot("explosionSoundTest", Player.main.transform.position);
        }
    }
}
