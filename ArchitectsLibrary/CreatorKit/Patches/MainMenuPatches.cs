using UnityEngine;
using UnityEngine.UI;
using HarmonyLib;
using CreatorKit.Mono;

namespace CreatorKit.Patches
{
    [HarmonyPatch(typeof(uGUI_MainMenu))]
    public class MainMenuMusicPatches
    {
        [HarmonyPatch(nameof(uGUI_MainMenu.Start))]
        [HarmonyPostfix]
        public static void MainMenu_Start_Postfix(uGUI_MainMenu __instance)
        {
            GameObject packLauncher = GameObject.Instantiate(UI.UIAssets.GetPackLauncherPrefab());
            packLauncher.AddComponent<MainMenuPackLauncher>();
            packLauncher.transform.parent = __instance.transform;
            Utility.Utils.GenerateEventSystemIfNeeded();
        }
    }
}
