using UnityEngine;
using HarmonyLib;
using CreatorKit.Mono;

namespace CreatorKit.Patches
{
    [HarmonyPatch(typeof(MainMenuMusic))]
    public class MainMenuMusicPatches
    {
        [HarmonyPatch(nameof(MainMenuMusic.Start))]
        [HarmonyPostfix]
        public static void MainMenuMusic_Start_Postfix()
        {
            GameObject.Instantiate(UI.UIAssets.GetPackLauncherPrefab()).AddComponent<MainMenuPackLauncher>();
        }
    }
}
