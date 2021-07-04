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
        }
        
        static readonly string incrementMessage = $"Increment the size ({LanguageUtils.FormatKeyCode(Main.Config.IncrementSize)})";
        static readonly string decrementMessage = $"Decrement the size ({LanguageUtils.FormatKeyCode(Main.Config.DecrementSize)})";
        static readonly string resetMsg = $"Reset the size ({LanguageUtils.FormatKeyCode(KeyCode.T)})";
        static readonly string txt = $"{incrementMessage}\n{decrementMessage}\n{resetMsg}";

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
                Builder.CreateGhost();
            }
            else if (Input.GetKeyDown(Main.Config.IncrementSize) ||
                     Input.GetKey(Main.Config.IncrementSize))
            {
                if (Builder.prefab.transform.localScale.x >= 1.3f)
                    return;
                
                Builder.prefab.transform.localScale *= 1.01f;
                Object.DestroyImmediate(Builder.ghostModel);
                Builder.CreateGhost();
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                Builder.prefab.transform.localScale = Vector3.one;
                Object.DestroyImmediate(Builder.ghostModel);
                Builder.CreateGhost();

            }
        }
    }
}