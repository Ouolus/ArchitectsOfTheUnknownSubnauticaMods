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
            if (!Mod.config.OverrideMainMenu)
            {
                return;
            }
            FMODAsset wreakMusic = ScriptableObject.CreateInstance<FMODAsset>();
            wreakMusic.path = "event:/env/music/wreak_ambience_big_music";
            wreakMusic.id = "{433ab5c7-6190-430a-929a-9b9b39593524}";

            __instance.music = wreakMusic;
        }
    }

    [HarmonyPatch(typeof(uGUI_MainMenu))]
    public class uGUI_MainMenu_Patch
    {
        [HarmonyPatch(nameof(uGUI_MainMenu.Awake))]
        [HarmonyPostfix]
        public static void uGUI_MainMenu_Postfix()
        {
            if (!Mod.config.OverrideMainMenu)
            {
                return;
            }
            GameObject subtitlePrefab = Mod.assetBundle.LoadAsset<GameObject>("SubTitle_Prefab");
            if(subtitlePrefab is not null)
            {
                GameObject subtitle = GameObject.Instantiate(subtitlePrefab);
                subtitle.transform.position = new Vector3(-11.635f, 0f, 20f);
                subtitle.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }

            var lights = Object.FindObjectsOfType<Light>();
            var highlight = lights[0];
            highlight.enabled = true;
            highlight.intensity = 0.5f;
            highlight.color = Color.magenta;
            var sun = lights[1];
            sun.intensity = 0f;
            sun.gameObject.AddComponent<MainMenuAtmosphereUpdater>();
        }
    }

    [HarmonyPatch(typeof(uGUI_SceneLoading))]
    public class uGUI_SceneLoading_Patch
    {
        [HarmonyPatch(nameof(uGUI_SceneLoading.Begin))]
        [HarmonyPostfix]
		public static void Postfix1(uGUI_SceneLoading __instance)
		{
            TryOverrideLoadingScreen(__instance);
        }

        [HarmonyPatch(nameof(uGUI_SceneLoading.BeginAsyncSceneLoad))]
        [HarmonyPostfix]
        public static void Postfix2(uGUI_SceneLoading __instance)
        {
            TryOverrideLoadingScreen(__instance);
        }

        [HarmonyPatch(nameof(uGUI_SceneLoading.DelayedBegin))]
        [HarmonyPostfix]
        public static void Postfix3(uGUI_SceneLoading __instance)
        {
            TryOverrideLoadingScreen(__instance);
        }

        [HarmonyPatch(nameof(uGUI_SceneLoading.ShowLoadingScreen))]
        [HarmonyPostfix]
        public static void Postfix4(uGUI_SceneLoading __instance)
        {
            TryOverrideLoadingScreen(__instance);
        }

        static void TryOverrideLoadingScreen(uGUI_SceneLoading sceneLoading)
        {
            if (Mod.config.OverrideLoadingScreen == false)
            {
                return;
            }
            Sprite loadingScreen = Mod.assetBundle.LoadAsset<Sprite>("GargLoadingScreen");
            Image[] componentsInChildren = sceneLoading.loadingBackground.GetComponentsInChildren<Image>();
            foreach (Image image in componentsInChildren)
            {
                image.sprite = loadingScreen;
            }
        }
	}
}
