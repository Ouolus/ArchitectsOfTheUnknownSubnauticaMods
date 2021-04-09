using HarmonyLib;
using UnityEngine;
using ProjectAncients.Mono;
using UnityEngine.UI;

namespace ProjectAncients.Patches
{
    [HarmonyPatch(typeof(MainMenuMusic))]
    public class MainMenuMusic_Patch
    {
        [HarmonyPatch(nameof(MainMenuMusic.Start))]
        [HarmonyPrefix]
        public static void MainMenuMusicStart_Prefix(MainMenuMusic __instance)
        {
            FMODAsset wreakMusic = ScriptableObject.CreateInstance<FMODAsset>();
            wreakMusic.path = "event:/env/music/wreak_ambience_big_music";
            wreakMusic.id = "{433ab5c7-6190-430a-929a-9b9b39593524}";

            __instance.music = wreakMusic;

            Light[] lights = Object.FindObjectsOfType<Light>();
            Light highlight = lights[0];
            highlight.enabled = true;
            highlight.intensity = 0.5f;
            highlight.color = Color.magenta;
            Light sun = lights[1];
            sun.intensity = 0f;
            sun.gameObject.AddComponent<MainMenuAtmosphereUpdater>();
        }
    }

    [HarmonyPatch(typeof(uGUI_SceneLoading))]
    public class uGUI_SceneLoading_Patch
    {
        [HarmonyPatch(nameof(uGUI_SceneLoading.Init))]
        [HarmonyPrefix]
		public static void Prefix(uGUI_SceneLoading __instance)
		{
            Sprite loadingScreen = Mod.assetBundle.LoadAsset<Sprite>("GargLoadingScreen");
            Image[] componentsInChildren = __instance.loadingBackground.GetComponentsInChildren<Image>();
			foreach (Image image in componentsInChildren)
			{
				image.sprite = loadingScreen;
			}
		}
	}
}
