using UnityEngine;
using HarmonyLib;
using CreatorKit.Mono;

namespace CreatorKit.Patches
{
    internal class MainMenuMusicPatches
    {
        internal static void Patch(Harmony harmony)
        {
            var target = AccessTools.Method(typeof(uGUI_MainMenu), "Start");
            var postfix = new HarmonyMethod(AccessTools.Method(typeof(MainMenuMusic), nameof(MainMenu_Start_Postfix)));
            harmony.Patch(target, postfix: postfix);
        }
        
        public static void MainMenu_Start_Postfix(uGUI_MainMenu __instance)
        {
            GameObject packLauncher = GameObject.Instantiate(UI.UIAssets.GetPackLauncherPrefab());
            packLauncher.AddComponent<MainMenuPackLauncher>();
            packLauncher.transform.parent = __instance.transform;
            Utility.Utils.GenerateEventSystemIfNeeded();
        }
    }
}
