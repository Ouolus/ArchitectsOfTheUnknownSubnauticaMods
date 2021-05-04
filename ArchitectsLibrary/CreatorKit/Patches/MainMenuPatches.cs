using UnityEngine;
using HarmonyLib;
using CreatorKit.Mono;

namespace CreatorKit.Patches
{
    public class MainMenuMusicPatches
    {
        internal static void Patch(Harmony harmony)
        {
            var target = AccessTools.Method(typeof(uGUI_MainMenu), "Start");
            var postfix = new HarmonyMethod(AccessTools.Method(typeof(MainMenuMusicPatches), nameof(MainMenu_Start_Postfix)));
            harmony.Patch(target, postfix: postfix);
        }
        
        public static void MainMenu_Start_Postfix(uGUI_MainMenu __instance)
        {
            GameObject packLauncherPrefab = UI.UIAssets.GetPackLauncherPrefab();
            if (packLauncherPrefab)
            {
                GameObject packLauncher = GameObject.Instantiate(packLauncherPrefab);
                packLauncher.AddComponent<MainMenuPackLauncher>();
                packLauncher.transform.parent = __instance.transform;
                Utility.Utils.GenerateEventSystemIfNeeded();
            }
        }
    }
}
