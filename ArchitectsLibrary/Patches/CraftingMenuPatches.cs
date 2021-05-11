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
#if SN1
            var uGUIType = typeof(uGUI_CraftNode);
            var createIconOrig = AccessTools.Method(typeof(uGUI_CraftNode), nameof(uGUI_CraftNode.CreateIcon));
#else
            var uGUIType = typeof(uGUI_CraftingMenu);
            var createIconOrig = AccessTools.Method(uGUIType, nameof(uGUI_CraftingMenu.CreateIcon));
#endif

            var transpiler = new HarmonyMethod(AccessTools.Method(typeof(CraftingMenuPatches), nameof(Transpiler)));
            harmony.Patch(createIconOrig, transpiler: transpiler);

            var pointerenterOrig = AccessTools.Method(uGUIType, "uGUI_IIconManager.OnPointerEnter");
            var pointerEnterPatch = new HarmonyMethod(AccessTools.Method(typeof(CraftingMenuPatches), nameof(OnPointerEnterPatch)));
            harmony.Patch(pointerenterOrig, pointerEnterPatch);
            
            var pointerExitOrig = AccessTools.Method(uGUIType, "uGUI_IIconManager.OnPointerExit");
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
                Logger.Log(Logger.Level.Debug, "CraftingMenu transpiler succeeded");
            else
                Logger.Log(Logger.Level.Error, "CraftingMenu transpiler failed.");
            
            return codes.AsEnumerable();
        }
#if SN1
        static void CorrectCraftTreeBackgrounds(uGUI_CraftNode craftingMenu, uGUI_ItemIcon icon)
#else
        static void CorrectCraftTreeBackgrounds(uGUI_CraftingMenu craftingMenu, uGUI_ItemIcon icon)
#endif
        {
            if (craftingMenu.id != kPrecursorFabricatorName)
            {
                return;
            }
            
            icon.SetBackgroundSprite(Main.background);
        }

#if SN1
        static bool OnPointerEnterPatch(uGUI_CraftNode __instance, uGUI_ItemIcon icon)
#else
        static bool OnPointerEnterPatch(uGUI_CraftingMenu __instance, uGUI_ItemIcon icon)
#endif
        {
            if (__instance.id != kPrecursorFabricatorName)
                return true;
            
            icon.SetBackgroundSprite(Main.backgroundHovered);
            return false;
        }
        
#if SN1
        static bool OnPointerExitPatch(uGUI_CraftNode __instance, uGUI_ItemIcon icon)
#else
        static bool OnPointerExitPatch(uGUI_CraftingMenu __instance, uGUI_ItemIcon icon)
#endif
        {
            if (__instance.id != kPrecursorFabricatorName)
                return true;
            
            icon.SetBackgroundSprite(Main.background);
            return false;
        }
    }
}