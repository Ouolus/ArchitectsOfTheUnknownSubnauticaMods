using HarmonyLib;
using UnityEngine;

namespace RotA.Patches
{
    [HarmonyPatch]
    class VoidGhostLeviathansSpawner_Patches
    {
        [HarmonyPrefix] //Prefix because ghost leviathans can technically spawn during the Start method.
        [HarmonyPatch(typeof(VoidGhostLeviathansSpawner), nameof(VoidGhostLeviathansSpawner.Start))]
        public static void VoidGhostLeviathansSpawner_Start_Prefix(VoidGhostLeviathansSpawner __instance) //This method modifies the void ghost leviathans, so the Gargantuan can target them.
        {
            GameObject ghostLeviPrefab = __instance.ghostLeviathanPrefab;
            var ecoTarget = ghostLeviPrefab.EnsureComponent<EcoTarget>();
            ecoTarget.type = EcoTargetType.Leviathan;
            ghostLeviPrefab.GetComponent<Locomotion>().maxAcceleration = 17f;
            CreatureFollowPlayer followPlayer = ghostLeviPrefab.GetComponent<CreatureFollowPlayer>();
            if (followPlayer != null)
            {
                Object.Destroy(followPlayer);
            }
        }

        [HarmonyPrefix] //Prefix because ghost leviathans can technically spawn during the Start method.
        [HarmonyPatch(typeof(GhostLeviatanVoid), nameof(GhostLeviatanVoid.UpdateVoidBehaviour))]
        public static bool GhostLeviathanVoid_UpdateVoidBehaviour_Prefix(GhostLeviatanVoid __instance)
        {
            Player main = Player.main;
            VoidGhostLeviathansSpawner main2 = VoidGhostLeviathansSpawner.main;
            if (!main || Vector3.Distance(main.transform.position, __instance.transform.position) > 300f)
            {
                Object.Destroy(__instance.gameObject);
                return false;
            }
            bool flag = main2 && main2.IsPlayerInVoid();
            __instance.updateBehaviour = flag;
            __instance.AllowCreatureUpdates(__instance.updateBehaviour);
            return false;
        }
    }
}
