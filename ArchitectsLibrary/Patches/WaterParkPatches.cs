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

        static bool CanDropItemInsidePrefix(Pickupable item, ref bool __result)
        {
            var tt = CraftData.GetTechType(item.gameObject);
            if (requiredAcuSize.TryGetValue(tt, out var maxHeight))
            {
                var waterPark = Player.main.currentWaterPark;
                if (waterPark is not null && waterPark.height >= maxHeight)
                {
                    __result = true;
                }
                else
                {
                    ErrorMessage.AddMessage($"Cannot drop {tt} in the ACU, requires at least {maxHeight} stacked ACUs to be dropped in.");
                    __result = false;
                }

                return false;
            }

            return true;
        }
    }
}