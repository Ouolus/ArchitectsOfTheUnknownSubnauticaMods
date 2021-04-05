﻿using ECCLibrary;
using ECCLibrary.Internal;
using HarmonyLib;
using ProjectAncients.Prefabs;
using System.Collections;
using System.Linq;
using UnityEngine;
using UWE;

namespace ProjectAncients.Patches
{
    [HarmonyPatch(typeof(CyclopsDecoyManager))]
    public static class CyclopsDecoyManager_Patches
    {
        [HarmonyPatch(nameof(CyclopsDecoyManager.TryLaunchDecoy))]
        [HarmonyPrefix]
        public static void TryLaunchDecoy_Prefix(CyclopsDecoyManager __instance)
        {
            if(__instance.decoyCount > 0)
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

            launcherInstance.decoyPrefab = task.GetResult();
            launcherInstance.decoyPrefab.SetActive(true);
        }
    }
}
