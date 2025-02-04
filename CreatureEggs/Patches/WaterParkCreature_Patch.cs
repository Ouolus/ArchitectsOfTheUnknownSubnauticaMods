﻿namespace CreatureEggs.Patches
{
    using ArchitectsLibrary.Utility;
    using HarmonyLib;
    using UnityEngine;
    using static Helpers.AssetsBundleHelper;
    
    [HarmonyPatch(typeof(WaterParkCreature))]
    class WaterParkCreature_Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(WaterParkCreature.Start))]
        static void Start_Patch(WaterParkCreature __instance)
        {
            TechType techType = CraftData.GetTechType(__instance.gameObject);

            // TODO: fix the staged growth for ACU creatures or get rid of it completely.
            switch (techType)
            {
                case TechType.SeaEmperorBaby:
                {
                        var materials = __instance.GetComponentInChildren<Renderer>().materials;
                        for (int i = 0; i < materials.Length; i++)
                        {
                            materials[i].EnableKeyword("_EMISSION");
                        }
                        
                        MaterialUtils.ApplySNShaders(__instance.gameObject);

                        for (int i = 0; i < materials.Length; i++)
                        {
                            var name = $"Leviathan_01_0{i + 1}";
                            materials[i].SetTexture(ShaderPropertyID._Illum, LoadTexture2D($"{name}_illum"));
                        }
                        
                        SeaEmperorBaby seb = __instance.gameObject.GetComponent<SeaEmperorBaby>();
                        if (seb != null)
                        {
                            SafeAnimator.SetBool(seb.GetAnimator(), "hatched", true);
                            seb.hatched = true;
                        }
                        /*StagedGrowing stagedGrowing = __instance.gameObject.EnsureComponent<StagedGrowing>();
                        stagedGrowing.daysToNextStage = 5;
                        stagedGrowing.nextStageTechType = TechType.SeaEmperorJuvenile;
                        stagedGrowing.nextStageStartSize = 0.1f;
                        __instance.canBreed = false;*/
                        break;
                    }
                case TechType.SeaEmperorJuvenile:
                    {
                        /*StagedGrowing stagedGrowing = __instance.gameObject.EnsureComponent<StagedGrowing>();
                        stagedGrowing.daysToNextStage = 5;
                        __instance.canBreed = true;*/
                        break;
                    }

                case TechType.GhostLeviathanJuvenile:
                    {
                        /*StagedGrowing stagedGrowing = __instance.gameObject.EnsureComponent<StagedGrowing>();
                        stagedGrowing.daysToNextStage = 5;
                        stagedGrowing.nextStageTechType = TechType.GhostLeviathan;
                        stagedGrowing.nextStageStartSize = 0.65f;
                        __instance.canBreed = false;*/
                        break;
                    }
                case TechType.GhostLeviathan:
                    {
                        /*Pickupable pickupable = __instance.gameObject.EnsureComponent<Pickupable>();
                        pickupable.isPickupable = true;*/
                        break;
                    }
                case TechType.ReefbackBaby:
                    {
                        /*StagedGrowing stagedGrowing = __instance.gameObject.EnsureComponent<StagedGrowing>();
                        stagedGrowing.daysToNextStage = 5;
                        stagedGrowing.nextStageTechType = TechType.Reefback;
                        stagedGrowing.nextStageStartSize = 0.3f;*/
                        //__instance.canBreed = false;
                        break;
                    }
                case TechType.PrecursorDroid:
                    {
                        __instance.canBreed = false;
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
        }
    }
}
