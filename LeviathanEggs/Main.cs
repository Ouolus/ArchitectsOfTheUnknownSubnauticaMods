using System.Collections.Generic;
using HarmonyLib;
using QModManager.API.ModLoading;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
using System.IO;
using System.Reflection;
using LeviathanEggs.Prefabs;
using UnityEngine;
using static LootDistributionData;
using UWE;
using LeviathanEggs.Configurations;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
namespace LeviathanEggs
{
    [QModCore]
    public static class Main
    {
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();
        private static string ModPath = Path.GetDirectoryName(myAssembly.Location);
        internal static string AssetsFolder = Path.Combine(ModPath, "Assets");
        public const string version = "1.0.0.0";
        internal static Config Config { get; private set; }
        internal static AssetBundle assetBundle = AssetBundle.LoadFromFile(Path.Combine(AssetsFolder, "creatureeggs"));
        internal static SeaEmperorEgg seaEmperorEgg = new SeaEmperorEgg();
        internal static SeaDragonEgg seaDragonEgg = new SeaDragonEgg();
        internal static GhostEgg ghostEgg = new GhostEgg();
        internal static RobotEgg robotEgg = new RobotEgg();
        internal static ReaperEgg reaperEgg = new ReaperEgg();
        internal static SeaTreaderEgg seaTreaderEgg = new SeaTreaderEgg();
        internal static LavaLarvaEgg lavaLarvaEgg = new LavaLarvaEgg();
        internal static GhostRayRedEgg ghostRayRedEgg = new GhostRayRedEgg();
        internal static GhostRayBlueEgg ghostRayBlueEgg = new GhostRayBlueEgg();
        internal static BloodCrawlerEgg shuttleBugEgg = new BloodCrawlerEgg();
        internal static CaveCrawlerEgg caveCrawlerEgg = new CaveCrawlerEgg();
        internal static WarperEgg warperEgg = new WarperEgg();
        internal static SpineEelEgg spineEelEgg = new SpineEelEgg();

        public static List<TechType> TechTypesToSkyApply = new List<TechType>() { TechType.SeaDragon, TechType.GhostLeviathan, TechType.GhostLeviathanJuvenile, TechType.SeaEmperorJuvenile, TechType.SeaEmperorBaby, TechType.SeaEmperor };
        public static List<TechType> TechTypesToMakePickupable = new List<TechType>() { TechType.GhostLeviathan, TechType.GhostLeviathanJuvenile, TechType.SeaDragon };
        public static List<TechType> TechTypesToTweak = new List<TechType>() { TechType.Bleeder, TechType.Rockgrub, TechType.Biter, TechType.Blighter };

        private static List<TechType> techTypesToAddEntry = new List<TechType>()
        {
            seaDragonEgg.TechType, seaEmperorEgg.TechType, ghostEgg.TechType, robotEgg.TechType, reaperEgg.TechType,
            seaTreaderEgg.TechType, lavaLarvaEgg.TechType, ghostRayRedEgg.TechType, ghostRayBlueEgg.TechType,
            shuttleBugEgg.TechType, caveCrawlerEgg.TechType,
            warperEgg.TechType, spineEelEgg.TechType
        };
        [QModPatch]
        public static void Load()
        {
            Config = OptionsPanelHandler.RegisterModOptions<Config>();
            IngameMenuHandler.RegisterOnSaveEvent(Config.Save);

            seaEmperorEgg.Patch(); 
            seaDragonEgg.Patch();
            ghostEgg.Patch();
            robotEgg.Patch();
            reaperEgg.Patch();
            seaTreaderEgg.Patch();
            lavaLarvaEgg.Patch();
            ghostRayRedEgg.Patch();
            ghostRayBlueEgg.Patch();
            shuttleBugEgg.Patch();
            caveCrawlerEgg.Patch();
            warperEgg.Patch();
            spineEelEgg.Patch();

            foreach (TechType tt in techTypesToAddEntry)
            {
                PDAHandler.AddCustomScannerEntry(new PDAScanner.EntryData()
                {
                    key = tt,
                    encyclopedia = "UnknownEgg",
                    scanTime = 2f,
                    isFragment = false
                });
            }
            
            WaterParkCreatureParametersSettings();

            Harmony.CreateAndPatchAll(myAssembly, $"ArchitectsOfTheUnknown_{myAssembly.GetName().Name}");
        }
        private static void WaterParkCreatureParametersSettings()
        {
            #region TechTypeNames
            LanguageHandler.SetTechTypeName(TechType.SeaEmperorBaby, "Sea Emperor Baby");
            LanguageHandler.SetTechTypeName(TechType.SeaEmperorJuvenile, "Sea Emperor Juvenile");
            #endregion
            #region Tooltips
            LanguageHandler.SetTechTypeTooltip(TechType.SeaEmperorBaby, "Gigantic sapient filter-feeder, raised in containment.");
            LanguageHandler.SetTechTypeTooltip(TechType.SeaEmperorJuvenile, "Gigantic sapient filter-feeder, raised in containment.");
            LanguageHandler.SetTechTypeTooltip(TechType.SeaEmperor, "Gigantic sapient filter-feeder, raised in containment.");

            LanguageHandler.SetTechTypeTooltip(TechType.SeaDragon, "Colossal reptilian carnivore, raised in containment.");

            
            LanguageHandler.SetTechTypeTooltip(TechType.GhostLeviathanJuvenile, "Massive territorial eel-like predator, raised in containment.");
            LanguageHandler.SetTechTypeTooltip(TechType.GhostLeviathan, "Massive territorial eel-like predator, raised in containment.");
            
            LanguageHandler.SetTechTypeTooltip(TechType.ReaperLeviathan, "Huge aggressive streamlined hunter, raised in containment.");

            LanguageHandler.SetTechTypeTooltip(TechType.SeaTreader, "Vast bipedal herbivore, raised in containment.");
            
            LanguageHandler.SetTechTypeTooltip(TechType.LavaLarva, "Grub-like energy parasite, raised in containment.");
            
            LanguageHandler.SetTechTypeTooltip(TechType.GhostRayRed, "Large docile scavenger ray, raised in containment");
            
            LanguageHandler.SetTechTypeTooltip(TechType.GhostRayBlue, "Large translucent herbivorous deep-sea ray, raised in containment.");
            
            LanguageHandler.SetTechTypeTooltip(TechType.Shuttlebug, "Agile, territorial, amphibious carrion-feeders, raised in containment.");
            LanguageHandler.SetTechTypeTooltip(TechType.CaveCrawler, "Agile, territorial deep-sea scavenger, raised in containment.");
            
            LanguageHandler.SetTechTypeTooltip(TechType.SpineEel, "Fast, agile, eel-like predator, raised in containment.");
            
            LanguageHandler.SetTechTypeTooltip(TechType.Warper, "Mechanically enhanced, genetically modified, Self-Warping Quarantine Enforcement Unit, raised in containment.\nWARNING; Will exterminate any infected individuals.");
            
            LanguageHandler.SetTechTypeTooltip(TechType.PrecursorDroid, "Small, efficient, durable maintenance unit, deployed in containment.");

            LanguageHandler.SetTechTypeTooltip(TechType.Bleeder, "Small, blood-sucking parasite. Tastes slimy and awful.");

            LanguageHandler.SetTechTypeTooltip(TechType.Biter, "Small, aggressive, edible pack-hunter. High protein count.");
            LanguageHandler.SetTechTypeTooltip(TechType.Blighter, "Small, edible ambush predator. High protein count.");

            LanguageHandler.SetTechTypeTooltip(TechType.Rockgrub, "Small, bioluminescent scavenger. Tastes like shrimp");
            #endregion
            #region Sprites
            SpriteHandler.RegisterSprite(TechType.SeaEmperorBaby, LoadSprite("SeaEmperorBaby"));
            SpriteHandler.RegisterSprite(TechType.SeaEmperorJuvenile, LoadSprite("SeaEmperor"));
            SpriteHandler.RegisterSprite(TechType.SeaEmperor, LoadSprite("SeaEmperor"));

            SpriteHandler.RegisterSprite(TechType.SeaDragon, LoadSprite("SeaDragon"));

            SpriteHandler.RegisterSprite(TechType.GhostLeviathan, LoadSprite("Ghost"));
            SpriteHandler.RegisterSprite(TechType.GhostLeviathanJuvenile, LoadSprite("Ghost"));
            
            SpriteHandler.RegisterSprite(TechType.ReaperLeviathan, LoadSprite("Reaper"));

            SpriteHandler.RegisterSprite(TechType.SeaTreader, LoadSprite("SeaTreader"));

            SpriteHandler.RegisterSprite(TechType.LavaLarva, LoadSprite("LavaLarva"));
            
            SpriteHandler.RegisterSprite(TechType.GhostRayRed, LoadSprite("CrimsonRay"));
            SpriteHandler.RegisterSprite(TechType.GhostRayBlue, LoadSprite("Ghostray"));
            
            SpriteHandler.RegisterSprite(TechType.Shuttlebug, LoadSprite("CaveCrawler"));
            SpriteHandler.RegisterSprite(TechType.CaveCrawler, LoadSprite("BloodCrawler"));
            
            SpriteHandler.RegisterSprite(TechType.SpineEel, LoadSprite("RiverProwler"));
            
            SpriteHandler.RegisterSprite(TechType.Warper, LoadSprite("Warper"));
            
            SpriteHandler.RegisterSprite(TechType.PrecursorDroid, LoadSprite("PrecursorDroid"));

            SpriteHandler.RegisterSprite(TechType.Rockgrub, LoadSprite("Rockgrub"));

            SpriteHandler.RegisterSprite(TechType.Bleeder, LoadSprite("Bleeder"));

            SpriteHandler.RegisterSprite(TechType.Biter, LoadSprite("Biter"));

            SpriteHandler.RegisterSprite(TechType.Blighter, LoadSprite("Blighter"));
            #endregion
            #region ItemSizes
            CraftDataHandler.SetItemSize(TechType.SeaEmperorBaby, new Vector2int(3, 3));
            CraftDataHandler.SetItemSize(TechType.SeaEmperorJuvenile, new Vector2int(4, 4));
            CraftDataHandler.SetItemSize(TechType.SeaEmperor, new Vector2int(4, 4));

            CraftDataHandler.SetItemSize(TechType.SeaDragon, new Vector2int(4, 4));

            CraftDataHandler.SetItemSize(TechType.GhostLeviathanJuvenile, new Vector2int(4, 4));
            CraftDataHandler.SetItemSize(TechType.GhostLeviathan, new Vector2int(4, 4));
            
            CraftDataHandler.SetItemSize(TechType.ReefbackBaby, new Vector2int(4, 4));
            
            CraftDataHandler.SetItemSize(TechType.ReaperLeviathan, new Vector2int(4, 4));
            
            CraftDataHandler.SetItemSize(TechType.SeaTreader, new Vector2int(4, 4));

            CraftDataHandler.SetItemSize(TechType.PrecursorDroid, new Vector2int(2, 2));
            
            CraftDataHandler.SetItemSize(TechType.LavaLarva, new Vector2int(2, 2));

            CraftDataHandler.SetItemSize(TechType.GhostRayRed, new Vector2int(3, 3));
            CraftDataHandler.SetItemSize(TechType.GhostRayBlue, new Vector2int(3, 3));
            
            CraftDataHandler.SetItemSize(TechType.Shuttlebug, new Vector2int(2, 2));
            CraftDataHandler.SetItemSize(TechType.CaveCrawler, new Vector2int(2, 2));
            
            CraftDataHandler.SetItemSize(TechType.Warper, new Vector2int(3, 3));
            
            CraftDataHandler.SetItemSize(TechType.SpineEel, new Vector2int(3, 3));
            
            CraftDataHandler.SetItemSize(TechType.Bleeder, new Vector2int(1, 1));

            CraftDataHandler.SetItemSize(TechType.Rockgrub, new Vector2int(1, 1));

            CraftDataHandler.SetItemSize(TechType.Biter, new Vector2int(1, 1));
            CraftDataHandler.SetItemSize(TechType.Blighter, new Vector2int(1, 1));
            #endregion
            #region WaterParkCreatureParameters
            WaterParkCreature.waterParkCreatureParameters[TechType.SeaEmperor] = new WaterParkCreatureParameters(0.03f, 0.04f, 0.07f, 1f, false);
            WaterParkCreature.waterParkCreatureParameters[TechType.SeaEmperorJuvenile] = new WaterParkCreatureParameters(0.03f, 0.04f, 0.07f, 1f, false);
            WaterParkCreature.waterParkCreatureParameters[TechType.SeaEmperorBaby] = new WaterParkCreatureParameters(0.3f, 0.5f, 0.5f, 1f, false);

            WaterParkCreature.waterParkCreatureParameters[TechType.SeaDragon] = new WaterParkCreatureParameters(0.03f, 0.04f, 0.07f, 1f, false);

            WaterParkCreature.waterParkCreatureParameters[TechType.GhostLeviathanJuvenile] = new WaterParkCreatureParameters(0.03f, 0.05f, 0.07f, 1f, false);
            WaterParkCreature.waterParkCreatureParameters[TechType.GhostLeviathan] = new WaterParkCreatureParameters(0.03f, 0.05f, 0.07f, 1f, false);

            WaterParkCreature.waterParkCreatureParameters[TechType.Bleeder] = new WaterParkCreatureParameters(0.2f, 0.7f, 1f, 1f, true);

            WaterParkCreature.waterParkCreatureParameters[TechType.Biter] = new WaterParkCreatureParameters(0.2f, 0.7f, 1f, 1f, true);
            WaterParkCreature.waterParkCreatureParameters[TechType.Blighter] = new WaterParkCreatureParameters(0.2f, 0.7f, 1f, 1f, true);

            WaterParkCreature.waterParkCreatureParameters[TechType.Rockgrub] = new WaterParkCreatureParameters(0.4f, 1f, 1f, 1f, true);

            WaterParkCreature.waterParkCreatureParameters[TechType.PrecursorDroid] = new WaterParkCreatureParameters(0.6f, 0.6f, 0.8f, 1f, false);

            WaterParkCreature.waterParkCreatureParameters[TechType.Shuttlebug] = new WaterParkCreatureParameters(0.2f, 0.4f, 0.6f, 1f, false);
            WaterParkCreature.waterParkCreatureParameters[TechType.CaveCrawler] = new WaterParkCreatureParameters(0.2f, 0.5f, 0.7f, 1f, false);
            
            WaterParkCreature.waterParkCreatureParameters[TechType.ReefbackBaby] = new WaterParkCreatureParameters(0.02f, 0.1f, 0.2f, 1f, false);

            WaterParkCreature.waterParkCreatureParameters[TechType.ReaperLeviathan] = new WaterParkCreatureParameters(0.02f, 0.05f, 0.07f, 1f, false);

            WaterParkCreature.waterParkCreatureParameters[TechType.SeaTreader] = new WaterParkCreatureParameters(0.02f, 0.05f, 0.07f, 1f, false);
            
            WaterParkCreature.waterParkCreatureParameters[TechType.LavaLarva] = new WaterParkCreatureParameters(0.2f, 0.7f, 1f, 1f, false);
            
            WaterParkCreature.waterParkCreatureParameters[TechType.GhostRayRed] = new WaterParkCreatureParameters(0.3f, 0.4f, 0.5f, 1f, false);
            WaterParkCreature.waterParkCreatureParameters[TechType.GhostRayBlue] = new WaterParkCreatureParameters(0.3f, 0.4f, 0.5f, 1f, false);
            
            WaterParkCreature.waterParkCreatureParameters[TechType.Warper] = new WaterParkCreatureParameters(0.3f, 0.4f, 0.5f, 1f, false);
            
            WaterParkCreature.waterParkCreatureParameters[TechType.SpineEel] = new WaterParkCreatureParameters(0.3f, 0.4f, 0.5f, 1f, false);
            #endregion
            #region Creature Eggs
            WaterParkCreature.creatureEggs[TechType.GhostLeviathan] = ghostEgg.TechType;
            WaterParkCreature.creatureEggs[TechType.SeaDragon] = seaDragonEgg.TechType;
            WaterParkCreature.creatureEggs[TechType.SeaEmperorJuvenile] = seaEmperorEgg.TechType;
            WaterParkCreature.creatureEggs[TechType.PrecursorDroid] = robotEgg.TechType;
            WaterParkCreature.creatureEggs[TechType.ReaperLeviathan] = reaperEgg.TechType;
            WaterParkCreature.creatureEggs[TechType.SeaTreader] = seaTreaderEgg.TechType;
            WaterParkCreature.creatureEggs[TechType.LavaLarva] = lavaLarvaEgg.TechType;
            WaterParkCreature.creatureEggs[TechType.GhostRayRed] = ghostRayRedEgg.TechType;
            WaterParkCreature.creatureEggs[TechType.GhostRayBlue] = ghostRayBlueEgg.TechType;
            WaterParkCreature.creatureEggs[TechType.Shuttlebug] = shuttleBugEgg.TechType;
            WaterParkCreature.creatureEggs[TechType.CaveCrawler] = caveCrawlerEgg.TechType;
            WaterParkCreature.creatureEggs[TechType.Warper] = warperEgg.TechType;
            WaterParkCreature.creatureEggs[TechType.SpineEel] = spineEelEgg.TechType;
            WaterParkCreature.creatureEggs[TechType.ReefbackBaby] = TechType.ReefbackEgg;

            WaterParkCreature.creatureEggs.Remove(TechType.Spadefish);
            WaterParkCreature.creatureEggs.Remove(TechType.Reefback);
            #endregion
            #region Loot Distributon
            #region Reefback Egg Spawns
            Dictionary<TechType, List<BiomeData>> reefbackEggBiomes = new Dictionary<TechType, List<BiomeData>>()
            {
                {
                    TechType.ReefbackEgg,
                    new List<BiomeData>()
                    {
                        new BiomeData()
                        {
                            biome = BiomeType.KooshZone_CaveFloor,
                            probability = 0.08f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.KooshZone_Mountains,
                            probability = 0.08f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.KooshZone_Sand,
                            probability = 0.01f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.GrassyPlateaus_Grass,
                            probability = 0.09f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.GrassyPlateaus_Sand,
                            probability = 0.02f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.MushroomForest_Grass,
                            probability = 0.05f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.MushroomForest_GiantTreeExterior,
                            probability = 0.02f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.SparseReef_Sand,
                            probability = 0.04f,
                            count = 1,
                        }
                    }
                }
            };
            foreach (KeyValuePair<TechType, List<BiomeData>> pair in reefbackEggBiomes)
            {
                string reefbackEggClassId = CraftData.GetClassIdForTechType(TechType.ReefbackEgg);
                if (PrefabDatabase.TryGetPrefabFilename(reefbackEggClassId, out string prefabpath))
                {
                    if (!WorldEntityDatabase.TryGetInfo(reefbackEggClassId, out WorldEntityInfo info))
                    {
                        info = new WorldEntityInfo()
                        {
                            cellLevel = LargeWorldEntity.CellLevel.Medium,
                            classId = reefbackEggClassId,
                            prefabZUp = false,
                            slotType = EntitySlot.Type.Medium,
                            techType = pair.Key,
                            localScale = Vector3.one
                        };
                    }
                    WorldEntityDatabaseHandler.AddCustomInfo(reefbackEggClassId, info);
                }
                SrcData data = new SrcData() { distribution = pair.Value, prefabPath = prefabpath };
                LootDistributionHandler.AddLootDistributionData(reefbackEggClassId, data);
            }
            #endregion
            #region Jumper Egg Spawns
            Dictionary<TechType, List<BiomeData>> jumperEggBiomes = new Dictionary<TechType, List<BiomeData>>()
            {
                {
                    TechType.JumperEgg,
                    new List<BiomeData>()
                    {
                        new BiomeData()
                        {
                            biome = BiomeType.SparseReef_DeepFloor,
                            probability = 0.5f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.SparseReef_CaveFloor,
                            probability = 0.5f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.Dunes_CaveFloor,
                            probability = 0.1f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.GrassyPlateaus_CaveFloor,
                            probability = 0.01f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.MushroomForest_CaveFloor,
                            probability = 0.01f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.MushroomForest_CaveSpecial,
                            probability = 0.01f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.SafeShallows_CaveFloor,
                            probability = 0.01f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.SafeShallows_CaveSpecial,
                            probability = 0.01f,
                            count = 1,
                        },
                        new BiomeData()
                        {
                            biome = BiomeType.SeaTreaderPath_CaveFloor,
                            probability = 0.01f,
                            count = 1,
                        },
                    }
                }
            };
            foreach (KeyValuePair<TechType, List<BiomeData>> kvp in jumperEggBiomes)
            {
                string jumperEggClassId = CraftData.GetClassIdForTechType(TechType.JumperEgg);
                if (PrefabDatabase.TryGetPrefabFilename(jumperEggClassId, out string prefabpath))
                {
                    if (!WorldEntityDatabase.TryGetInfo(jumperEggClassId, out WorldEntityInfo info))
                    {
                        info = new WorldEntityInfo()
                        {
                            cellLevel = LargeWorldEntity.CellLevel.Medium,
                            classId = jumperEggClassId,
                            prefabZUp = false,
                            slotType = EntitySlot.Type.Medium,
                            techType = kvp.Key,
                            localScale = Vector3.one
                        };
                    }
                    WorldEntityDatabaseHandler.AddCustomInfo(jumperEggClassId, info);
                }
                SrcData data = new SrcData() { distribution = kvp.Value, prefabPath = prefabpath };
                LootDistributionHandler.AddLootDistributionData(jumperEggClassId, data);

            }
            #endregion
            #region Spadefish Egg Removal
            string spadeFishEggClassId = CraftData.GetClassIdForTechType(TechType.SpadefishEgg);
            LootDistributionHandler.EditLootDistributionData(spadeFishEggClassId, BiomeType.SparseReef_DeepCoral, 0f, 0);
            LootDistributionHandler.EditLootDistributionData(spadeFishEggClassId, BiomeType.GrassyPlateaus_Grass, 0f, 0);
            LootDistributionHandler.EditLootDistributionData(spadeFishEggClassId, BiomeType.GrassyPlateaus_CaveFloor, 0f, 0);
            LootDistributionHandler.EditLootDistributionData(spadeFishEggClassId, BiomeType.UnderwaterIslands_CaveFloor_Obsolete, 0f, 0);
            LootDistributionHandler.EditLootDistributionData(spadeFishEggClassId, BiomeType.UnderwaterIslands_ValleyLedge, 0f, 0);
            LootDistributionHandler.EditLootDistributionData(spadeFishEggClassId, BiomeType.GrandReef_Grass, 0f, 0);
            #endregion
            #endregion
        }
    }
}
