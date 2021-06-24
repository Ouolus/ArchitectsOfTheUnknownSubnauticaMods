using HarmonyLib;
using System.Collections;
using UnityEngine;
using UWE;

namespace RotA.Patches
{
    [HarmonyPatch(typeof(CyclopsDecoyManager))]
    public static class CyclopsDecoyManager_Patches
    {
        [HarmonyPatch(nameof(CyclopsDecoyManager.TryLaunchDecoy))]
        [HarmonyPrefix]
        public static void TryLaunchDecoy_Prefix(CyclopsDecoyManager __instance)
        {
            if (__instance.decoyCount > 0)
            {
                TechType firstDecoyTechType = TechType.None;
                for (int i = __instance.decoyLoadingTube.decoyManager.decoyMax; i >= 1; i--)
                {
                    string slot = string.Format("DecoySlot{0}", i);
                    TechType tt = __instance.decoyLoadingTube.decoySlots.GetTechTypeInSlot(slot);
                    if (tt != TechType.None)
                    {
                        firstDecoyTechType = tt;
                        break;
                    }
                }
                if (firstDecoyTechType == TechType.None)
                {
                    return;
                }
                CoroutineHost.StartCoroutine(SetDecoyPrefab(__instance.decoyLauncher, firstDecoyTechType));
            }
        }

        public static IEnumerator SetDecoyPrefab(CyclopsDecoyLauncher launcherInstance, TechType techType)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(techType);
            yield return task;

            GameObject prefab = GameObject.Instantiate(task.GetResult());
            prefab.SetActive(false);
            launcherInstance.decoyPrefab = prefab;
        }
    }

    [HarmonyPatch(typeof(CyclopsDecoyLauncher))]
    public static class CyclopsDecoyLauncher_Patches
    {
        [HarmonyPatch(nameof(CyclopsDecoyLauncher.LaunchDecoy))]
        [HarmonyPrefix]
        public static bool Prefix(CyclopsDecoyLauncher __instance)
        {
            if (__instance.decoyPrefab.activeSelf)
            {
                return true;
            }
            CyclopsDecoy decoy = Object.Instantiate<GameObject>(__instance.decoyPrefab, __instance.transform.position, Quaternion.identity).GetComponent<CyclopsDecoy>();
            decoy.gameObject.SetActive(true);
            if (decoy)
            {
                decoy.launch = true;
            }
            return false;
        }
    }
}
