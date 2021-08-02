using HarmonyLib;
using RotA.Mono.AlienTech;
using UnityEngine;

namespace RotA.Patches
{
    [HarmonyPatch(typeof(PrecursorTeleporter))]
    public class PrecursorTeleporter_Patches
    {
        [HarmonyPatch(nameof(PrecursorTeleporter.Start))]
        [HarmonyPostfix]
        public static void Start_Postfix(PrecursorTeleporter __instance)
        {
            var rt = __instance.gameObject.GetComponent<RotaTeleporter>();
            if (rt != null)
            {
                if (rt.shouldOverrideColor)
                {
                    if (__instance.portalFxControl != null)
                    {
                        __instance.portalFxControl.GetComponentInChildren<Renderer>(true).material.SetColor("_ColorStrength", rt.fxColor);
                    }
                }
            }
        }
    }
}
