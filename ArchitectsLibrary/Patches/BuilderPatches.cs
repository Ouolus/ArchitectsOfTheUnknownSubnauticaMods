using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using ArchitectsLibrary.Utility;
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

            ValidateHintMessage();

            if (Input.GetKeyDown(Main.Config.DecrementSize) ||
                Input.GetKey(Main.Config.DecrementSize))
            {
                if (Builder.prefab.transform.localScale.x <= .4f)
                    return;
                
                Builder.prefab.transform.localScale *= 0.99f;
                Object.DestroyImmediate(Builder.ghostModel);
            }
            else if (Input.GetKeyDown(Main.Config.IncrementSize) ||
                     Input.GetKey(Main.Config.IncrementSize))
            {
                if (Builder.prefab.transform.localScale.x >= 1.3f)
                    return;
                
                Builder.prefab.transform.localScale *= 1.01f;
                Object.DestroyImmediate(Builder.ghostModel);
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                Builder.prefab.transform.localScale = Vector3.one;
                Object.DestroyImmediate(Builder.ghostModel);
            }
        }

        static IEnumerable<CodeInstruction> TryPlaceTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = new(instructions);

            var setScale = AccessTools.Method(typeof(BuilderPatches), nameof(SetScale));
            var rotationSet = AccessTools.PropertySetter(typeof(Transform), nameof(Transform.rotation)); // setter method of the rotation property

            bool found = false;
            
            /*
             * our target is the following:
             * Transform transform = gameObject.transform;
			 * transform.position = Builder.placePosition;
			 * transform.rotation = Builder.placeRotation;
             * Constructable componentInParent3 = gameObject.GetComponentInParent<Constructable>();
             *
             * what we want to do is to add the SetScale() method at the end of our target so we end up having something like this:
             * Transform transform = gameObject.transform;
			 * transform.position = Builder.placePosition;
			 * transform.rotation = Builder.placeRotation;
             * SetScale(transform)
             * Constructable componentInParent3 = gameObject.GetComponentInParent<Constructable>();
             */

            for (int i = 0; i < codes.Count; i++)
            {
                /*
                 * this is our target's IL code:
                 * ldloc.2
                 * callvirt  instance class [UnityEngine.CoreModule]UnityEngine.Transform [UnityEngine.CoreModule]UnityEngine.GameObject::get_transform()
                 * dup
                 * ldsfld    valuetype [UnityEngine.CoreModule]UnityEngine.Vector3 Builder::placePosition
                 * callvirt  instance void [UnityEngine.CoreModule]UnityEngine.Transform::set_position(valuetype [UnityEngine.CoreModule]UnityEngine.Vector3)
                 * ldsfld    valuetype [UnityEngine.CoreModule]UnityEngine.Quaternion Builder::placeRotation
                 * callvirt  instance void [UnityEngine.CoreModule]UnityEngine.Transform::set_rotation(valuetype [UnityEngine.CoreModule]UnityEngine.Quaternion)
                 * ldloc.2
                 * callvirt  instance !!0 [UnityEngine.CoreModule]UnityEngine.GameObject::GetComponentInParent<class Constructable>()
                 * ...
                 *
                 * so since we want to insert our SetScale() method after the 'transform.rotation = Builder.placeRotation;' we're gonna target it.
                 * 
                 * the 'ldloc.2' is the 'GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Builder.prefab);' local variable, so we need to pass it as an argument for the SetScale() so it can modify
                 * the scale.
                 */
                if (codes[i].opcode == OpCodes.Callvirt && Equals(codes[i].operand, rotationSet) && codes[i + 1].opcode == OpCodes.Ldloc_2)
                {
                    found = true;
                    codes.Insert(i + 1, new CodeInstruction(OpCodes.Call, setScale)); // first we call the setScale method
                    codes.Insert(i + 1, new CodeInstruction(OpCodes.Ldloc_2)); // then we insert the transform local variable to the stack at the top of our call
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
            
        }

        static void ValidateHintMessage()
        {
            var incrementMessage = $"Increment the size ({LanguageUtils.FormatKeyCode(Main.Config.IncrementSize)})";
            var decrementMessage = $"Decrement the size ({LanguageUtils.FormatKeyCode(Main.Config.DecrementSize)})";
            var resetMsg = $"Reset the size ({LanguageUtils.FormatKeyCode(KeyCode.T)})";
            var txt = $"{incrementMessage}\n{decrementMessage}\n{resetMsg}";

            var msg = ErrorMessage.main.GetExistingMessage(txt);
            if (msg != null)
            {
                if (msg.timeEnd < Time.time + 2)
                    msg.timeEnd += Time.deltaTime;
                
                return;
            }
            
            ErrorMessage.AddMessage(txt);
        }
    }
}