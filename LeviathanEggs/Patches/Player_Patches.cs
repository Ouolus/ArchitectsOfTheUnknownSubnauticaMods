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

                AquariumFish aquariumFish = prefab.EnsureComponent<AquariumFish>();

                Eatable eatable = prefab.EnsureComponent<Eatable>();
                switch (techType)
                {
                    case TechType.Bleeder:
                        eatable.foodValue = -3f;
                        eatable.waterValue = 6f;
                        break;
                    case TechType.Biter:
                        eatable.foodValue = 22f;
                        eatable.waterValue = 4f;
                        break;
                    case TechType.Blighter:
                        eatable.foodValue = 19f;
                        eatable.waterValue = 5f;
                        break;
                    default:
                        eatable.foodValue = 10f;
                        eatable.waterValue = 2f;
                        break;
                }
                GameObject obj = GameObject.Instantiate(prefab.gameObject);

                foreach (Component component in obj.GetComponents<Component>())
                {
                    GameObject.DestroyImmediate(component);
                }
                aquariumFish.model = obj;
                obj.SetActive(false);
            }

            yield break;
        }
    }
}
