using HarmonyLib;
using UnityEngine;
using LeviathanEggs.MonoBehaviours;
namespace LeviathanEggs.Patches
{
    [HarmonyPatch]
    class Creature_Start
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Creature), nameof(Creature.Start))]
        static void Creature_Postfix(Creature __instance)
        {
            if (__instance.gameObject.transform.position == Vector3.zero)
            {
                GameObject.DestroyImmediate(__instance.gameObject);
                return;
            }

            TechType techType = CraftData.GetTechType(__instance.gameObject);

            switch (techType)
            {
                case TechType.SeaEmperorBaby:
                    {
                        if (!Main.Config.GlobalStagedGrowth)
                        {
                            StagedGrowing stagedGrowing = __instance.gameObject.EnsureComponent<StagedGrowing>();
                            stagedGrowing.daysToNextStage = 5;
                            stagedGrowing.nextStageTechType = TechType.SeaEmperorJuvenile;
                        }

                        break;
                    }

                case TechType.GhostLeviathanJuvenile:
                    {
                        StagedGrowing stagedGrowing = __instance.gameObject.EnsureComponent<StagedGrowing>();
                        stagedGrowing.daysToNextStage = 5;
                        stagedGrowing.nextStageTechType = TechType.GhostLeviathan;
                        break;
                    }

                default:
                    if (Main.Config.GlobalGrowth)
                        __instance.gameObject.EnsureComponent<StagedGrowing>().daysToNextStage = 5;

                    break;
            }


            if (Main.TechTypesToSkyApply.Contains(techType))
            {
                SkyApplier skyApplier = __instance.gameObject.EnsureComponent<SkyApplier>();

                skyApplier.anchorSky = Skies.Auto;
                skyApplier.renderers = __instance.gameObject.GetAllComponentsInChildren<Renderer>();
                skyApplier.dynamic = true;
                skyApplier.emissiveFromPower = false;
                skyApplier.hideFlags = HideFlags.None;
                skyApplier.enabled = true;
            }

            if (Main.TechTypesToMakePickupable.Contains(techType))
            {
                Pickupable pickupable = __instance.gameObject.EnsureComponent<Pickupable>();
                pickupable.isPickupable = false;
            }

            if (Main.TechTypesToTweak.Contains(techType))
            {
                Pickupable pickupable = __instance.gameObject.EnsureComponent<Pickupable>();
                pickupable.isPickupable = true;

                AquariumFish aquariumFish = __instance.gameObject.EnsureComponent<AquariumFish>();
                aquariumFish.model = __instance.gameObject;

                Eatable eatable = __instance.gameObject.EnsureComponent<Eatable>();
                switch (techType)
                {
                    case TechType.Bleeder:
                        eatable.foodValue = -1f;
                        eatable.waterValue = -5f;
                        break;
                    default:
                        eatable.foodValue = 10f;
                        eatable.waterValue = 4f;
                        break;
                }
            }
        }
        
        [HarmonyPostfix]
        [HarmonyPatch(typeof(WaterParkCreature), nameof(WaterParkCreature.Start))]
        static void WaterParkCreature_Postfix(WaterParkCreature __instance)
        {
            if (__instance.gameObject.transform.position == Vector3.zero)
            {
                GameObject.DestroyImmediate(__instance.gameObject);
                return;
            }

            TechType techType = CraftData.GetTechType(__instance.gameObject);

            switch (techType)
            {
                case TechType.SeaEmperorBaby:
                    {
                        StagedGrowing stagedGrowing = __instance.gameObject.EnsureComponent<StagedGrowing>();
                        stagedGrowing.daysToNextStage = 5;
                        stagedGrowing.nextStageTechType = TechType.SeaEmperorJuvenile;
                        __instance.canBreed = false;
                        Pickupable pickupable = __instance.gameObject.EnsureComponent<Pickupable>();
                        pickupable.isPickupable = true;
                        pickupable.timeDropped = Time.time;
                        break;
                    }
                case TechType.SeaEmperorJuvenile:
                    {
                        StagedGrowing stagedGrowing = __instance.gameObject.EnsureComponent<StagedGrowing>();
                        stagedGrowing.daysToNextStage = 5;
                        stagedGrowing.nextStageTechType = TechType.SeaEmperor;
                        __instance.canBreed = false;
                        Pickupable pickupable = __instance.gameObject.EnsureComponent<Pickupable>();
                        pickupable.isPickupable = true;
                        pickupable.timeDropped = Time.time;
                        break;
                    }

                case TechType.GhostLeviathanJuvenile:
                    {
                        StagedGrowing stagedGrowing = __instance.gameObject.EnsureComponent<StagedGrowing>();
                        stagedGrowing.daysToNextStage = 5;
                        stagedGrowing.nextStageTechType = TechType.GhostLeviathan;
                        __instance.canBreed = false;
                        Pickupable pickupable = __instance.gameObject.EnsureComponent<Pickupable>();
                        pickupable.isPickupable = true;
                        pickupable.timeDropped = Time.time;
                        break;
                    }
                case TechType.GhostLeviathan:
                    {
                        Pickupable pickupable = __instance.gameObject.EnsureComponent<Pickupable>();
                        pickupable.isPickupable = true;
                        pickupable.timeDropped = Time.time;
                        break;
                    }
                case TechType.SeaEmperor:
                    {
                        Pickupable pickupable = __instance.gameObject.EnsureComponent<Pickupable>();
                        pickupable.isPickupable = true;
                        pickupable.timeDropped = Time.time;
                        break;
                    }
                case TechType.SeaDragon:
                    {
                        Pickupable pickupable = __instance.gameObject.EnsureComponent<Pickupable>();
                        pickupable.isPickupable = true;
                        pickupable.timeDropped = Time.time;
                        break;
                    }
                case TechType.PrecursorDroid:
                    {
                        Pickupable pickupable = __instance.gameObject.EnsureComponent<Pickupable>();
                        pickupable.isPickupable = true;
                        pickupable.timeDropped = Time.time;
                        break;
                    }

            }


            if (Main.TechTypesToSkyApply.Contains(techType))
            {
                SkyApplier skyApplier = __instance.gameObject.EnsureComponent<SkyApplier>();

                skyApplier.anchorSky = Skies.Auto;
                skyApplier.renderers = __instance.gameObject.GetAllComponentsInChildren<Renderer>();
                skyApplier.dynamic = true;
                skyApplier.emissiveFromPower = false;
                skyApplier.hideFlags = HideFlags.None;
                skyApplier.enabled = true;
            }

            if (Main.TechTypesToMakePickupable.Contains(techType))
            {
                Pickupable pickupable = __instance.gameObject.EnsureComponent<Pickupable>();
                pickupable.isPickupable = false;
            }

            if (Main.TechTypesToTweak.Contains(techType))
            {
                Pickupable pickupable = __instance.gameObject.EnsureComponent<Pickupable>();
                pickupable.isPickupable = true;

                AquariumFish aquariumFish = __instance.gameObject.EnsureComponent<AquariumFish>();
                aquariumFish.model = __instance.gameObject;

                Eatable eatable = __instance.gameObject.EnsureComponent<Eatable>();
                switch (techType)
                {
                    case TechType.Bleeder:
                        eatable.foodValue = -1f;
                        eatable.waterValue = -5f;
                        break;
                    default:
                        eatable.foodValue = 10f;
                        eatable.waterValue = 4f;
                        break;
                }

            }
        }
    }
}
