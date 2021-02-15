using HarmonyLib;
using UnityEngine;
namespace LeviathanEggs.Patches
{
    [HarmonyPatch(typeof(Survival))]
    class Survival_Patches
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(Survival.Eat))]
        static void EatPostfix(GameObject useObj)
        {
            TechType techType = CraftData.GetTechType(useObj);

            if (techType == TechType.Bleeder)
            {
                Player.main.GetComponent<LiveMixin>().TakeDamage(5f, type: DamageType.Poison);
            }
        }
    }
}
