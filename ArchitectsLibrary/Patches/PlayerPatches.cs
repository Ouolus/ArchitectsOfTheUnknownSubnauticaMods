using System.Collections;
using HarmonyLib;
using UnityEngine;

namespace ArchitectsLibrary.Patches
{
    internal class PlayerPatches
    {
        internal static void Patch(Harmony harmony)
        {
            var orig = AccessTools.Method(typeof(Player), nameof(Player.Awake));
            var postfix = new HarmonyMethod(AccessTools.Method(typeof(PlayerPatches), nameof(AwakePostfix)));
            harmony.Patch(orig, postfix: postfix);
        }

        static void AwakePostfix(Player __instance)
        {
            __instance.StartCoroutine(FixIonCubeCraftingCoroutine());
        }
        
        static IEnumerator FixIonCubeCraftingCoroutine()
        {
            yield return new WaitForSeconds(2f);
            
            var task = CraftData.GetPrefabForTechTypeAsync(TechType.PrecursorIonCrystal);
            yield return task;
            var prefab = task.GetResult();
            prefab.EnsureComponent<Battery>()._capacity = 1000000;
            Main.IonCubeCraftModelFix(prefab);
        }
    }
}