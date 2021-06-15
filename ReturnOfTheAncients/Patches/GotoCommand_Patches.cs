using HarmonyLib;
using RotA.Prefabs;
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
            AddTeleportPosition(ref __instance, "lostriveroutpost", new Vector3(-730, -759, -224));
            AddTeleportPosition(ref __instance, "supplycache", new Vector3(-16, -172, -1133));
            AddTeleportPosition(ref __instance, "researchbase", new Vector3(-852, -189, -579));
            AddTeleportPosition(ref __instance, "kooshzonebase", new Vector3(1483, -456, 1508));
            AddTeleportPosition(ref __instance, "destroyedguardian", new Vector3(367, -335f, -1750));
            AddTeleportPosition(ref __instance, "voidbase", new Vector3(372, -395, -1801));
        }

        static void AddTeleportPosition(ref GotoConsoleCommand gotoCmd, string name, Vector3 pos)
        {
            var locations = new List<TeleportPosition>(gotoCmd.data.locations);
            locations.Add(new TeleportPosition() { name = name, position = pos });
            gotoCmd.data.locations = locations.ToArray();
        }
    }
}
