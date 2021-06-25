using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace RotA.Patches
{
    [HarmonyPatch(typeof(GotoConsoleCommand))]
    public static class GotoCommand_Patches
    {
        [HarmonyPatch(nameof(GotoConsoleCommand.Awake))]
        [HarmonyPostfix]
        public static void Awake_Patch(GotoConsoleCommand __instance)
        {
            AddTeleportPosition(ref __instance, "gotocommandsaretemporarilyhidden", new Vector3(0, 0, 0));
        }

        static void AddTeleportPosition(ref GotoConsoleCommand gotoCmd, string name, Vector3 pos)
        {
            var locations = new List<TeleportPosition>(gotoCmd.data.locations);
            locations.Add(new TeleportPosition() { name = name, position = pos });
            gotoCmd.data.locations = locations.ToArray();
        }
    }
}
