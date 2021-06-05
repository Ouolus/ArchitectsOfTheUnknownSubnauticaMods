namespace RotA.Patches
{
    using HarmonyLib;
    using UnityEngine;
    using static MainMenu_Patches;
    
    [HarmonyPatch(typeof(uGUI_OptionsPanel))]
    class uGUI_OptionPanel_Patches
    {
        [HarmonyPatch(nameof(uGUI_OptionsPanel.OnColorGradingChanged))]
        [HarmonyPrefix]
        static void OnColorGradingChangedPrefix(int mode)
        {
            var color = mode switch
            {
                1 => new Color(0.351f, 1.1f, 0.351f, 0),
                2 => new Color(0.351f, 1.3971f, 0.351f, 0),
                _ => new Color(0.351f, 1.08f, 0.351f, 0)
            };
            if (SubTitleRenderer != null)
                SubTitleRenderer.material.SetColor("_EmissionColor", color);
        }
    }
}