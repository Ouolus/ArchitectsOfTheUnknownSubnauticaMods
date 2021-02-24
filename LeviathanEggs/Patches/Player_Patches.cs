using System.Collections;
using HarmonyLib;
using UWE;
using UnityEngine;
namespace LeviathanEggs.Patches
{
    [HarmonyPatch(typeof(Player))]
    class Player_Patches
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.Awake))]
        static void Postfix()
        {
            foreach (var techType in Main.TechTypesToTweak)
                CoroutineHost.StartCoroutine(TweakCreatures(techType));
        }
        static IEnumerator TweakCreatures(TechType techType)
        {
            yield return new WaitForSeconds(10f);

            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(techType, false);
            yield return task;

            GameObject prefab = task.GetResult();
            if (prefab != null)
            {
                Pickupable pickupable = prefab.EnsureComponent<Pickupable>();
                pickupable.isPickupable = true;
            }

            yield break;
        }
    }
}
