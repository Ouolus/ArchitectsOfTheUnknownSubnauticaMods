using HarmonyLib;
using UnityEngine;
using ProjectAncients.Mono;
using UnityEngine.UI;

namespace ProjectAncients.Patches
{
    [HarmonyPatch]
    public class MainMenu_Patches
    {
        [HarmonyPatch(typeof(MainMenuMusic))]
        [HarmonyPatch(nameof(MainMenuMusic.Start))]
        [HarmonyPrefix]
        static void MainMenuMusicStart_Prefix(MainMenuMusic __instance)
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
        
        [HarmonyPatch(typeof(uGUI_MainMenu))]
        [HarmonyPatch(nameof(uGUI_MainMenu.Awake))]
        [HarmonyPostfix]
        static void uGUI_MainMenu_Postfix(uGUI_MainMenu __instance)
        {
            if (!Mod.config.OverrideMainMenu)
            {
                return;
            }
            GameObject subtitlePrefab = Mod.assetBundle.LoadAsset<GameObject>("SubTitle_Prefab");
            const float subtitleScale = 1f;
            if(subtitlePrefab is not null)
            {
                GameObject subtitle = GameObject.Instantiate(subtitlePrefab);
                subtitle.transform.position = new Vector3(-11.635f - (subtitleScale * 0.5f), 0f, 16f);
                subtitle.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                subtitle.transform.localScale = Vector3.one * subtitleScale;
            }

            var lights = Object.FindObjectsOfType<Light>();
            var highlight = lights[0];
            highlight.enabled = true;
            highlight.intensity = 0.5f;
            highlight.color = Color.magenta;
            var sun = lights[1];
            sun.intensity = 0f;
            sun.gameObject.AddComponent<MainMenuAtmosphereUpdater>();

            //GameObject.Destroy(__instance.gameObject);
        }
        
        [HarmonyPatch(typeof(uGUI_SceneLoading), nameof(uGUI_SceneLoading.Begin))]
        [HarmonyPatch(typeof(uGUI_SceneLoading), nameof(uGUI_SceneLoading.BeginAsyncSceneLoad))]
        [HarmonyPatch(typeof(uGUI_SceneLoading), nameof(uGUI_SceneLoading.DelayedBegin))]
        [HarmonyPatch(typeof(uGUI_SceneLoading), nameof(uGUI_SceneLoading.ShowLoadingScreen))]
        [HarmonyPostfix]
        static void uGUI_SceneLoading_Postfix(uGUI_SceneLoading __instance)
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
