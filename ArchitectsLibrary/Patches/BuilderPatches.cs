using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;
using Logger = QModManager.Utility.Logger;

namespace ArchitectsLibrary.Patches
{
    class BuilderPatches
    {
        internal static void Patch(Harmony harmony)
        {
            var orig = AccessTools.Method(typeof(Builder), nameof(Builder.CreateGhost));
            var prefix = new HarmonyMethod(AccessTools.Method(typeof(BuilderPatches), nameof(CreateGhostPrefix)));
            harmony.Patch(orig, prefix);

            var orig2 = AccessTools.Method(typeof(Builder), nameof(Builder.TryPlace));
            var transpiler = new HarmonyMethod(AccessTools.Method(typeof(BuilderPatches), nameof(TryPlaceTranspiler)));
            harmony.Patch(orig2, transpiler: transpiler);
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

        static IEnumerable<CodeInstruction> TryPlaceTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = new(instructions);

            var setScale = AccessTools.Method(typeof(BuilderPatches), nameof(SetScale));
            var rotationSet = AccessTools.PropertySetter(typeof(Transform), nameof(Transform.rotation));

            bool found = false;

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Callvirt && Equals(codes[i].operand, rotationSet) && codes[i + 1].opcode == OpCodes.Ldloc_2)
                {
                    found = true;
                    codes.Insert(i + 1, new CodeInstruction(OpCodes.Call, setScale));
                    codes.Insert(i + 1, new CodeInstruction(OpCodes.Ldloc_2));
                    break;
                }
            }
            
            if (found)
                Logger.Log(Logger.Level.Debug, "Builder transpiler succeeded");
            else
                Logger.Log(Logger.Level.Error, "Builder transpiler failed.");

            return codes.AsEnumerable();
        }

        static void SetScale(GameObject obj)
        {
            obj.transform.localScale = Builder.ghostModelScale;
        }
    }
}