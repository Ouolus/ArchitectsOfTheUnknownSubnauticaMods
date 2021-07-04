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
            var orig = AccessTools.Method(typeof(Builder), nameof(Builder.Update));
            var postfix = new HarmonyMethod(AccessTools.Method(typeof(BuilderPatches), nameof(UpdatePostfix)));
            harmony.Patch(orig, postfix: postfix);

            var orig2 = AccessTools.Method(typeof(Builder), nameof(Builder.TryPlace));
            var postfix2 = new HarmonyMethod(AccessTools.Method(typeof(BuilderPatches), nameof(TryPlacePostfix)));
            harmony.Patch(orig2, postfix: postfix2);

            var orig3 = AccessTools.Method(typeof(Builder), nameof(Builder.End));
            var postfix3 = new HarmonyMethod(AccessTools.Method(typeof(BuilderPatches), nameof(EndPostfix)));
            harmony.Patch(orig3, postfix: postfix3);

            var orig4 = AccessTools.Method(typeof(Builder), nameof(Builder.CreateGhost));
            var postfix4 = new HarmonyMethod(AccessTools.Method(typeof(BuilderPatches), nameof(CreateGhostPostfix)));
            harmony.Patch(orig4, postfix: postfix4);
        }
        
        static readonly string incrementMessage = $"Increment the size ({LanguageUtils.FormatKeyCode(Main.Config.IncrementSize)})";
        static readonly string decrementMessage = $"Decrement the size ({LanguageUtils.FormatKeyCode(Main.Config.DecrementSize)})";
        static readonly string resetMsg = $"Reset the size ({LanguageUtils.FormatKeyCode(KeyCode.T)})";
        static readonly string txt = $"{incrementMessage}\n{decrementMessage}\n{resetMsg}";

        static bool initialized;

        static void UpdatePostfix()
        {
            if (Builder.prefab == null || Builder.ghostModel == null)
                return;
            
            if (!Main.DecorationTechs.Contains(CraftData.GetTechType(Builder.prefab)))
                return;

            ErrorMessage.main.AddHint(txt);

            if (Input.GetKeyDown(Main.Config.DecrementSize) ||
                Input.GetKey(Main.Config.DecrementSize))
            {
                if (Builder.prefab.transform.localScale.x <= .4f)
                    return;
                
                Builder.prefab.transform.localScale *= 0.99f;
                Object.DestroyImmediate(Builder.ghostModel);
                initialized = true;
            }
            else if (Input.GetKeyDown(Main.Config.IncrementSize) ||
                     Input.GetKey(Main.Config.IncrementSize))
            {
                if (Builder.prefab.transform.localScale.x >= 1.3f)
                    return;
                
                Builder.prefab.transform.localScale *= 1.01f;
                Object.DestroyImmediate(Builder.ghostModel);
                initialized = true;
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                Builder.prefab.transform.localScale = Vector3.one;
                Object.DestroyImmediate(Builder.ghostModel);
                initialized = true;

            }
        }

        static void TryPlacePostfix() => initialized = false;

        static void EndPostfix() => initialized = false;

        static void CreateGhostPostfix(ref bool __result)
        {
            if(!Main.DecorationTechs.Contains(CraftData.GetTechType(Builder.prefab)))
                return;

            if(__result && initialized)
                __result = false;
        }
    }
}