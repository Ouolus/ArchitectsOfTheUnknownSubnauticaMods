using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using RotA.Mono;
using RotA.Mono.Creatures.GargEssentials;
using UnityEngine;
using Logger = QModManager.Utility.Logger;

namespace RotA.Patches
{
    [HarmonyPatch(typeof(LiveMixin))]
    public class LiveMixin_Patches
    {
        [HarmonyPatch(nameof(LiveMixin.Start))]
        [HarmonyPostfix]
        static void Start_Postfix(LiveMixin __instance)
        {
            var garg = __instance.GetComponent<GargantuanBehaviour>();

            if (garg is not null)
                __instance.invincible = true;
        }
        
        
        [HarmonyPatch(nameof(LiveMixin.Kill))]
        [HarmonyPrefix]
        static bool Kill_Prefix(LiveMixin __instance)
        {
            if (__instance.invincible)
                return false;

            return true;
        }
        
        [HarmonyPatch(nameof(LiveMixin.TakeDamage))]
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> TakeDamage_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = new(instructions);
            MethodInfo architectElectEffect =
                typeof(LiveMixin_Patches).GetMethod(nameof(ArchitectElectEffect),
                    BindingFlags.Public | BindingFlags.Static);

            bool found = false;


            for (int i = 0; i < codes.Count(); i++)
            {
                if (codes[i].opcode == OpCodes.Ldstr &&
                    (string)codes[i].operand == "LiveMixin.TakeDamage.DamageEffect" &&
                    codes[i + 1].opcode == OpCodes.Call)
                {
                    found = true;
                    codes.Insert(i + 2, new CodeInstruction(OpCodes.Ldarg_0));
                    codes.Insert(i + 3, new CodeInstruction(OpCodes.Ldarg_3));
                    codes.Insert(i + 4, new CodeInstruction(OpCodes.Call, architectElectEffect));
                    break;
                }
            }

            if (found is false)
                Logger.Log(Logger.Level.Error, "Cannot find LiveMixin.TakeDamage target location.", showOnScreen: true);
            else
                Logger.Log(Logger.Level.Debug, "LiveMixin.TakeDamage Transpiler Succeeded.");

            return codes.AsEnumerable();
        }

        public static void ArchitectElectEffect(LiveMixin liveMixin, DamageType type)
        {
            if (Time.time > liveMixin.timeLastElecDamageEffect + 2.5f && type == Mod.architectElect &&
                liveMixin.electricalDamageEffect is not null)
            {
                var fixedBounds = liveMixin.gameObject.GetComponent<FixedBounds>();
                Bounds bounds;
                if (fixedBounds is not null)
                    bounds = fixedBounds.bounds;
                else
                    bounds = UWE.Utils.GetEncapsulatedAABB(liveMixin.gameObject);

                var electricFX = Object.Instantiate(liveMixin.electricalDamageEffect);
                var renderers = electricFX.GetComponentsInChildren<Renderer>(true);
                foreach (var renderer in renderers)
                {
                    renderer.material.SetColor("_Color", Color.green);
                }

                var obj = UWE.Utils.InstantiateWrap(electricFX, bounds.center,
                    Quaternion.identity);
                obj.transform.parent = liveMixin.transform;
                obj.transform.localScale = bounds.size * 0.65f;
                liveMixin.timeLastElecDamageEffect = Time.time;
            }
        }
    }
}
