using System.Collections;
using ArchitectsLibrary.MonoBehaviours;
using HarmonyLib;
using UnityEngine;
using UWE;

namespace ArchitectsLibrary.Patches
{
    internal class CraftDataPatches
    {
        internal static void Patch(Harmony harmony)
        {
            var orig = AccessTools.Method(typeof(PrefabDatabase), nameof(PrefabDatabase.LoadPrefabDatabase));
            var postfix = new HarmonyMethod(AccessTools.Method(typeof(CraftDataPatches), nameof(PreparePrefabIDCachePostfix)));
            harmony.Patch(orig, postfix: postfix);
        }

        static void PreparePrefabIDCachePostfix()
        {
            CoroutineHost.StartCoroutine(FixIonCubeCraftingCoroutine());
        }

        static IEnumerator FixIonCubeCraftingCoroutine()
        {
            yield return null;
            var task = CraftData.GetPrefabForTechTypeAsync(TechType.PrecursorIonCrystal);
            yield return task;
            var prefab = task.GetResult();
            prefab.EnsureComponent<PrecursorIonStorage>()._capacity = 1000000;
            Main.IonCubeCraftModelFix(prefab);
        }
    }
}