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
            var postfix = new HarmonyMethod(AccessTools.Method(typeof(BuilderPatches), nameof(CreateGhostPostfix)));
            var prefix = new HarmonyMethod(AccessTools.Method(typeof(BuilderPatches), nameof(CreateGhostPrefix)));
            harmony.Patch(orig, prefix, postfix);

            var orig2 = AccessTools.Method(typeof(Builder), nameof(Builder.TryPlace));
            var postfix2 = new HarmonyMethod(AccessTools.Method(typeof(BuilderPatches), nameof(TryPlacePostfix)));
            harmony.Patch(orig2, postfix: postfix2);

            var orig3 = AccessTools.Method(typeof(Builder), nameof(Builder.End));
            var postfix3 = new HarmonyMethod(AccessTools.Method(typeof(BuilderPatches), nameof(EndPostfix)));
            harmony.Patch(orig3, postfix: postfix3);
        }
        
        static string IncrementMessage => Language.main.GetFormat("BuilderIncrementSizePrompt", LanguageUtils.FormatKeyCode(Main.Config.IncrementSize));
        static string DecrementMessage => Language.main.GetFormat("BuilderDecrementSizePrompt", LanguageUtils.FormatKeyCode(Main.Config.DecrementSize));
        static string ResetMsg => Language.main.GetFormat("BuilderResetSizePrompt", LanguageUtils.FormatKeyCode(KeyCode.T));
        static string Txt => $"{IncrementMessage}\n{DecrementMessage}\n{ResetMsg}";

        static bool initialized;

        static void CreateGhostPrefix()
        {
            if (Builder.prefab == null || Builder.ghostModel == null)
                return;
            
            if (!Main.DecorationTechs.Contains(CraftData.GetTechType(Builder.prefab)))
                return;

            ErrorMessage.main.AddHint(Txt);

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
        
        static void CreateGhostPostfix(ref bool __result)
        {
            if(!Main.DecorationTechs.Contains(CraftData.GetTechType(Builder.prefab)))
                return;

            if(__result && initialized)
                __result = false;
        }

        static void TryPlacePostfix() => initialized = false;

        static void EndPostfix() => initialized = false;
    }
}