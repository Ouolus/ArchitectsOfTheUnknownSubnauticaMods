using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using QModManager.Utility;

namespace RotA.Patches
{
    [HarmonyPatch(typeof(uGUI_ItemSelector), nameof(uGUI_ItemSelector.CreateIcons))]
    internal static class uGUI_ItemSelector_Patches
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            bool found = false;

            var setBackGroundColor = AccessTools.Method(typeof(uGUI_ItemIcon), nameof(uGUI_ItemIcon.SetBackgroundColors));
            var setForeGroundColor = AccessTools.Method(typeof(uGUI_ItemIcon), nameof(uGUI_ItemIcon.SetForegroundColors));

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Callvirt && Equals(codes[i].operand, setForeGroundColor))
                {
                    found = true;
                    codes[i].operand = setBackGroundColor;
                    break;
                }
            }
            
            if (found)
                Logger.Log(Logger.Level.Debug, "uGUI_ItemSelector Transpiler succeeded");
            else
                Logger.Log(Logger.Level.Error, "Cannot find uGUI_ItemSelector.CreateIcons target location.");

            return codes.AsEnumerable();
        }
    }
}