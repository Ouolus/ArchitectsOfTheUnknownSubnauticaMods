using System.Collections;
using HarmonyLib;
using UWE;
using UnityEngine;
namespace CreatureEggs.Patches
{
    [HarmonyPatch(typeof(Player))]
    class Player_Patches
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.Awake))]
        static void Postfix(Player __instance)
        {
            foreach (var techType in Main.TechTypesToTweak)
                __instance.StartCoroutine(TweakCreatures(techType));
        }
        static IEnumerator TweakCreatures(TechType techType)
        {
            yield return new WaitForSeconds(2f);

            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(techType, false);
            yield return task;

            GameObject prefab = task.GetResult();
            if (prefab != null)
            {
                Pickupable pickupable = prefab.EnsureComponent<Pickupable>();
                pickupable.isPickupable = true;

                AquariumFish aquariumFish = prefab.EnsureComponent<AquariumFish>();

                Eatable eatable = prefab.EnsureComponent<Eatable>();
                int modelIndex = 0;
                switch (techType)
                {
                    case TechType.Bleeder:
                        eatable.foodValue = -3f;
                        eatable.waterValue = 6f;
                        break;
                    case TechType.Biter:
                        eatable.foodValue = 22f;
                        eatable.waterValue = 4f;
                        modelIndex = 1;
                        break;
                    case TechType.Blighter:
                        eatable.foodValue = 19f;
                        eatable.waterValue = 5f;
                        modelIndex = 1;
                        break;
                    default:
                        eatable.foodValue = 10f;
                        eatable.waterValue = 2f;
                        break;
                }

                aquariumFish.model = prefab.transform.GetChild(modelIndex).gameObject;
            }
        }
    }
}
