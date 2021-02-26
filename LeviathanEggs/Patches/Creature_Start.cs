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
                            stagedGrowing.nextStageStartSize = 0.1f;
                        }
                        else
                        {
                            if (__instance.gameObject.GetComponent<WaterParkCreature>() != null)
                            {
                                StagedGrowing stagedGrowing = __instance.gameObject.EnsureComponent<StagedGrowing>();
                                stagedGrowing.daysToNextStage = 5;
                                stagedGrowing.nextStageTechType = TechType.SeaEmperorJuvenile;
                                stagedGrowing.nextStageStartSize = 0.1f;
                            }
                        }

                        break;
                    }

                case TechType.SeaEmperorJuvenile:
                    {
                        if (__instance.gameObject.GetComponent<StagedGrowing>() != null)
                        {
                            if (__instance.gameObject.transform.localScale.x != 0.1f)
                                __instance.gameObject.transform.localScale = Vector3.one * 0.1f;
                        }
                        break;
                    }

                case TechType.GhostLeviathanJuvenile:
                    {
                        if (!Main.Config.GlobalStagedGrowth)
                        {
                            StagedGrowing stagedGrowing = __instance.gameObject.EnsureComponent<StagedGrowing>();
                            stagedGrowing.daysToNextStage = 5;
                            stagedGrowing.nextStageTechType = TechType.GhostLeviathan;
                            stagedGrowing.nextStageStartSize = 0.65f;
                        }
                        else
                        {
                            if (__instance.gameObject.GetComponent<WaterParkCreature>() != null)
                            {
                                StagedGrowing stagedGrowing = __instance.gameObject.EnsureComponent<StagedGrowing>();
                                stagedGrowing.daysToNextStage = 5;
                                stagedGrowing.nextStageTechType = TechType.GhostLeviathan;
                                stagedGrowing.nextStageStartSize = 0.65f;
                            }
                        }
                        break;
                    }
                case TechType.GhostLeviathan:
                    {
                        if (__instance.gameObject.GetComponent<StagedGrowing>() != null)
                        {
                            if (__instance.gameObject.transform.localScale.x != 0.65f)
                                __instance.gameObject.transform.localScale = Vector3.one * 0.65f;
                        }
                        break;
                    }
                case TechType.ReefbackBaby:
                    {
                        if (!Main.Config.GlobalStagedGrowth)
                        {
                            StagedGrowing stagedGrowing = __instance.gameObject.EnsureComponent<StagedGrowing>();
                            stagedGrowing.daysToNextStage = 5;
                            stagedGrowing.nextStageTechType = TechType.Reefback;
                            stagedGrowing.nextStageStartSize = 0.3f;
                        }
                        else
                        {
                            if (__instance.gameObject.GetComponent<WaterParkCreature>() != null)
                            {
                                StagedGrowing stagedGrowing = __instance.gameObject.EnsureComponent<StagedGrowing>();
                                stagedGrowing.daysToNextStage = 5;
                                stagedGrowing.nextStageTechType = TechType.Reefback;
                                stagedGrowing.nextStageStartSize = 0.3f;
                            }
                        }
                        break;
                    }
                case TechType.Reefback:
                    {
                        if (__instance.gameObject.GetComponent<StagedGrowing>() != null)
                        {
                            if (__instance.gameObject.transform.localScale.x != 0.3f)
                                __instance.gameObject.transform.localScale = Vector3.one * 0.3f;
                        }
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
        }
    }
}
