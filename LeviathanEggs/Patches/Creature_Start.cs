﻿using HarmonyLib;
using UnityEngine;
using LeviathanEggs.MonoBehaviours;
namespace LeviathanEggs.Patches
{
    [HarmonyPatch(typeof(Creature), nameof(Creature.Start))]
    class Creature_Start
    {
        [HarmonyPostfix]
        static void Postfix(Creature __instance)
        {
            if (__instance.gameObject.TryGetComponent(out WaterParkCreature waterParkCreature) && waterParkCreature.IsInsideWaterPark())
                return;

            TechType techType = CraftData.GetTechType(__instance.gameObject);

            ReadyToGrow rtg = __instance.gameObject.GetComponent<ReadyToGrow>();

            if (techType == TechType.SeaEmperorBaby && rtg != null)
            {
                StagedGrowing stagedGrowing = __instance.gameObject.EnsureComponent<StagedGrowing>();
                stagedGrowing.daysToNextStage = 5;
                stagedGrowing.nextStageTechType = TechType.SeaEmperorJuvenile;
            }
            else if (techType == TechType.GhostLeviathanJuvenile && rtg != null)
            {
                StagedGrowing stagedGrowing = __instance.gameObject.EnsureComponent<StagedGrowing>();
                stagedGrowing.daysToNextStage = 5;
                stagedGrowing.nextStageTechType = TechType.GhostLeviathan;
            }

            if (__instance.gameObject.transform.position == Vector3.zero)
                GameObject.DestroyImmediate(__instance.gameObject);

            foreach (TechType tt in Main.TechTypesToSkyApply)
            {
                if (techType == tt)
                {
                    SkyApplier skyApplier = __instance.gameObject.EnsureComponent<SkyApplier>();

                    skyApplier.anchorSky = Skies.Auto;
                    skyApplier.renderers = __instance.gameObject.GetAllComponentsInChildren<Renderer>();
                    skyApplier.dynamic = true;
                    skyApplier.emissiveFromPower = false;
                    skyApplier.hideFlags = HideFlags.None;
                    skyApplier.enabled = true;
                }
            }
            foreach (TechType tt in Main.TechTypesToMakePickupable)
            {
                if (techType == tt)
                {
                    Pickupable pickupable = __instance.gameObject.EnsureComponent<Pickupable>();
                    pickupable.isPickupable = false;
                }
            }
            foreach (TechType tt in Main.TechTypesToTweak)
            {
                if (techType == tt)
                {
                    Pickupable pickupable = __instance.gameObject.EnsureComponent<Pickupable>();
                    pickupable.isPickupable = true;

                    AquariumFish aquariumFish = __instance.gameObject.EnsureComponent<AquariumFish>();
                    aquariumFish.model = __instance.gameObject;

                    Eatable eatable = __instance.gameObject.EnsureComponent<Eatable>();
                    if (tt == TechType.Bleeder)
                    {
                        eatable.foodValue = -1f;
                        eatable.waterValue = -5f;
                    }
                    else
                    {
                        eatable.foodValue = 10f;
                        eatable.waterValue = 4f;
                    }

                }
            }
        }
    }
}
