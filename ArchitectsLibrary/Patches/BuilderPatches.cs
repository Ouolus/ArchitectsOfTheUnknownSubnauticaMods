using ArchitectsLibrary.Configuration;
using ArchitectsLibrary.MonoBehaviours;
using HarmonyLib;
using UnityEngine;

namespace ArchitectsLibrary.Patches
{
    class BuilderPatches
    {
        internal static void Patch(Harmony harmony)
        {
            var orig = AccessTools.Method(typeof(Builder), nameof(Builder.CreateGhost));
            var prefix = new HarmonyMethod(AccessTools.Method(typeof(BuilderPatches), nameof(CreateGhostPrefix)));
            harmony.Patch(orig, prefix);
        }

        static void CreateGhostPrefix()
        {
            if (Builder.prefab == null || Builder.ghostModel == null)
                return;
            
            if (!Main.DecorationTechs.Contains(CraftData.GetTechType(Builder.prefab)))
                return;

            if (Input.GetKeyDown(Main.Config.DecrementSize) ||
                Input.GetKey(Main.Config.DecrementSize))
            {
                Builder.ghostModelScale *= 0.9f;
            }
            else if (Input.GetKeyDown(Main.Config.IncrementSize) ||
                     Input.GetKey(Main.Config.IncrementSize))
            {
                Builder.ghostModelScale *= 1.2f;
            }
        }
    }
}