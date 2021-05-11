using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using QModManager.Utility;

namespace ArchitectsLibrary.Patches
{
    class CraftingMenuPatches
    {
        const string kPrecursorFabricatorName = "PrecursorFabricator";
        internal static void Patch(Harmony harmony)
        {
            var createIconOrig = AccessTools.Method(typeof(uGUI_CraftingMenu), nameof(uGUI_CraftingMenu.CreateIcon));
            var transpiler = new HarmonyMethod(AccessTools.Method(typeof(CraftingMenuPatches), nameof(Transpiler)));
            harmony.Patch(createIconOrig, transpiler: transpiler);

            var pointerenterOrig = AccessTools.Method(typeof(uGUI_CraftingMenu), "uGUI_IIconManager.OnPointerEnter");
            var pointerEnterPatch = new HarmonyMethod(AccessTools.Method(typeof(CraftingMenuPatches), nameof(OnPointerEnterPatch)));
            harmony.Patch(pointerenterOrig, pointerEnterPatch);
            
            var pointerExitOrig = AccessTools.Method(typeof(uGUI_CraftingMenu), "uGUI_IIconManager.OnPointerExit");
            var pointerExitPatch = new HarmonyMethod(AccessTools.Method(typeof(CraftingMenuPatches), nameof(OnPointerExitPatch)));
            harmony.Patch(pointerExitOrig, pointerExitPatch);
        }

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = new(instructions);

            var setPosition =
                AccessTools.Method(typeof(uGUI_ItemIcon), nameof(uGUI_ItemIcon.SetPosition), new[] { typeof(float), typeof(float) });

            var correctCraftTreeBackgrounds = AccessTools.Method(typeof(CraftingMenuPatches), nameof(CorrectCraftTreeBackgrounds));

            bool found = false;

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Callvirt && (MethodInfo) codes[i].operand == setPosition)
                {
                    found = true;
                    codes.Insert(i + 1, new CodeInstruction(OpCodes.Call, correctCraftTreeBackgrounds));
                    codes.Insert(i + 1, new CodeInstruction(OpCodes.Ldloc_S, 7));
                    codes.Insert(i + 1, new CodeInstruction(OpCodes.Ldarg_0));
                    break;
                }
            }

            if (found)
                Logger.Log(Logger.Level.Info, "Test transpiler succeeded");
            else
                Logger.Log(Logger.Level.Error, "Test transpiler failed.");
            
            return codes.AsEnumerable();
        }

        static void CorrectCraftTreeBackgrounds(uGUI_CraftingMenu craftingMenu, uGUI_ItemIcon icon)
        {
            if (craftingMenu.id != kPrecursorFabricatorName)
            {
                return;
            }
            
            icon.SetBackgroundSprite(Main.background);
        }

        static bool OnPointerEnterPatch(uGUI_CraftingMenu __instance, uGUI_ItemIcon icon)
        {
            if (__instance.id != kPrecursorFabricatorName)
                return true;
            
            icon.SetBackgroundSprite(Main.backgroundHovered);
            return false;
        }
        
        static bool OnPointerExitPatch(uGUI_CraftingMenu __instance, uGUI_ItemIcon icon)
        {
            if (__instance.id != kPrecursorFabricatorName)
                return true;
            
            icon.SetBackgroundSprite(Main.background);
            return false;
        }
    }
}