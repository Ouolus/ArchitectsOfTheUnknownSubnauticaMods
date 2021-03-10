using ECCLibrary;
using HarmonyLib;
using ProjectAncients.Prefabs;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAncients.Patches
{
    [HarmonyPatch]
    public static class Ping_Patches
    {
        public static readonly List<string> whitePings = new List<string>() { "Precursor_Symbol01", "Precursor_Symbol04", "RuinedGuardian_Ping" };
        static bool ShouldBeWhite(string textureName)
        {
            if (whitePings.Contains(textureName))
            {
                return true;
            }
            return false;
        }

        [HarmonyPrefix] 
        [HarmonyPatch(typeof(uGUI_Pings), nameof(uGUI_Pings.OnColor))]
        public static bool uGUI_Pings_OnColor_Postfix(uGUI_Pings __instance, int id)
        {
            uGUI_Ping uGUI_Ping;
            if (__instance.pings.TryGetValue(id, out uGUI_Ping))
            {
                if (ShouldBeWhite(uGUI_Ping.icon.sprite.texture.name))
                {
                    uGUI_Ping.SetColor(Color.white);
                    return false;
                }
            }
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(uGUI_Ping), nameof(uGUI_Ping.SetIcon))]
        public static void uGUI_Ping_SetIcon_Postfix(uGUI_Ping __instance, Atlas.Sprite sprite)
        {
            if (ShouldBeWhite(sprite.texture.name))
            {
                __instance.SetColor(Color.white);
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(uGUI_PingEntry), nameof(uGUI_PingEntry.SetColor))]
        public static void uGUI_PingEntry_SetColor_Postfix(uGUI_PingEntry __instance)
        {
            __instance.icon.color = Color.white;
            __instance.colorSelectionIndicator.position = __instance.colorSelectors[0].targetGraphic.rectTransform.position;
        }
    }
}
