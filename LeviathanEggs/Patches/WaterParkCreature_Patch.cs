using HarmonyLib;
using UnityEngine;
namespace LeviathanEggs.Patches
{
    [HarmonyPatch(typeof(WaterParkCreature))]
    class WaterParkCreature_Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(WaterParkCreature.Start))]
        static void Start_Patch(WaterParkCreature __instance)
        {
            TechType techType = CraftData.GetTechType(__instance.gameObject);

            if (techType == TechType.SeaEmperorBaby)
            {
                SeaEmperorBaby seb = __instance.gameObject.GetComponent<SeaEmperorBaby>();
                if (seb != null)
                {
                    SafeAnimator.SetBool(seb.GetAnimator(), "hatched", true);
                    seb.hatched = true;
                }
            }
        }
        [HarmonyPostfix]
        [HarmonyPatch(nameof(WaterParkCreature.Update))]
        static void Update_Patch(WaterParkCreature __instance)
        {
            TechType techType = CraftData.GetTechType(__instance.gameObject);
            foreach (TechType tt in Main.TechTypesToSkyApply)
            {
                if (techType == tt)
                {
                    SkyApplier skyApplier = __instance.gameObject.EnsureComponent<SkyApplier>();

                    if (__instance.gameObject.TryGetComponent(out __instance) && __instance.IsInsideWaterPark())
                        skyApplier.anchorSky = Skies.BaseInterior;
                    else
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
}
