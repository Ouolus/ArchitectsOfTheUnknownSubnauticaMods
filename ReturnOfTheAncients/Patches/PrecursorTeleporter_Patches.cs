using HarmonyLib;
using RotA.Mono.AlienTech;

namespace RotA.Patches
{
    [HarmonyPatch(typeof(PrecursorTeleporter))]
    public class PrecursorTeleporter_Patches
    {
        [HarmonyPatch(nameof(PrecursorTeleporter.Start))]
        [HarmonyPostfix]
        public static void Start_Postfix(PrecursorTeleporter __instance)
        {
            if (__instance.gameObject.GetComponent<RotaTeleporter>() != null)
            {
                if (__instance.portalFxControl != null)
                {
                    __instance.portalFxControl.gameObject.SetActive(true);
                }
            }
        }
    }
}
