using ECCLibrary;
using HarmonyLib;
using RotA.Mono.Cinematics;
using Story;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using UWE;
using Logger = QModManager.Utility.Logger;

namespace RotA.Patches
{
    [HarmonyPatch(typeof(StoryGoalCustomEventHandler))]
    class StoryGoalCustomEventHandler_Patches
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(StoryGoalCustomEventHandler.NotifyGoalComplete))]
        static IEnumerable<CodeInstruction> NotifyGoalComplete_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            var trigger = AccessTools.Method(typeof(StoryGoal), nameof(StoryGoal.Trigger));
            var beginRoar = AccessTools.Method(typeof(StoryGoalCustomEventHandler_Patches), nameof(BeginRoar));

            bool found = false;

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Callvirt && (MethodInfo)codes[i].operand == trigger &&
                    codes[i + 1].opcode == OpCodes.Ret)
                {
                    found = true;
                    codes.Insert(i + 1, new(OpCodes.Call, beginRoar));
                    break;
                }
            }

            if (found is false)
                Logger.Log(Logger.Level.Error, "Cannot find StoryGoalCustomEventHandler.NotifyGoalComplete target location.", showOnScreen: true);
            else
                Logger.Log(Logger.Level.Debug, "StoryGoalCustomEventHandler.NotifyGoalComplete Transpiler Succeeded.");

            return codes.AsEnumerable();
        }

        static void BeginRoar()
        {
            CoroutineHost.StartCoroutine(SunbeamGargEventCoroutine());
        }

        static IEnumerator SunbeamGargEventCoroutine()
        {
            yield return new WaitForSeconds(30.3f);
            SunbeamGargController.PlayCinematic();
        }
    }
}