using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using ArchitectsLibrary.MonoBehaviours;
using HarmonyLib;
using UnityEngine;
using Logger = QModManager.Utility.Logger;

namespace ArchitectsLibrary.Patches
{
    class BuilderPatches
    {
        internal static void Patch(Harmony harmony)
        {
            var original = AccessTools.Method(typeof(Builder), nameof(Builder.TryPlace));
            var transpiler = new HarmonyMethod(AccessTools.Method(typeof(BuilderPatches), nameof(TryPlaceTranspiler)));

            harmony.Patch(original, transpiler: transpiler);
        }
        
        static IEnumerable<CodeInstruction> TryPlaceTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            var ignoreCollision = AccessTools.Method(typeof(BuilderPatches), nameof(IgnoreCollision));
            
            var found = false;

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldloc_2 && codes[i + 1].opcode == OpCodes.Callvirt &&
                    codes[i + 2].opcode == OpCodes.Dup)
                {
                    found = true;
                    codes[i + 1] = new CodeInstruction(OpCodes.Call, ignoreCollision);
                    break;
                }
            }
            
            if (found)
                Logger.Log(Logger.Level.Debug, "Builder transpiler succeeded");
            else
                Logger.Log(Logger.Level.Error, "Builder transpiler failed.");

            return codes.AsEnumerable();
        }

        static Transform IgnoreCollision(GameObject buildablePrefab)
        {
            var subRoot = Builder.placementTarget.GetComponentInParent<SubRoot>() ??
                          UWE.Utils.GetEntityRoot(Builder.placementTarget)?.GetComponentInParent<SubRoot>();

            GameObject subRootObj = null;

            if (subRoot is not null)
                subRootObj = subRoot.gameObject;

            if (subRootObj != null)
            {
                foreach (var collider in buildablePrefab.GetAllComponentsInChildren<Collider>())
                {
                    foreach (var subRootCollider in subRootObj.GetAllComponentsInChildren<Collider>())
                    {
                        Physics.IgnoreCollision(collider, subRootCollider);
                    }
                }
            }

            return buildablePrefab.transform;
        }
    }
}