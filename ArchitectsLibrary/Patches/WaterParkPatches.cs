using System.Collections.Generic;
using HarmonyLib;

namespace ArchitectsLibrary.Patches
{
    static class WaterParkPatches
    {
        internal static Dictionary<TechType, int> requiredAcuSize = new();
        
        internal static void Patch(Harmony harmony)
        {
            var orig = AccessTools.Method(typeof(WaterPark), nameof(WaterPark.CanDropItemInside));
            var prefix = AccessTools.Method(typeof(WaterParkPatches), nameof(CanDropItemInsidePrefix));
            harmony.Patch(orig, new HarmonyMethod(prefix));
        }

        static bool CanDropItemInsidePrefix(Pickupable pickupable, ref bool __result)
        {
            if (requiredAcuSize.TryGetValue(pickupable.GetTechType(), out var maxHeight))
            {
                var waterPark = Player.main.currentWaterPark;
                if (waterPark is not null && waterPark.height >= maxHeight)
                {
                    __result = true;
                    return false;
                }
            }

            return true;
        }
    }
}