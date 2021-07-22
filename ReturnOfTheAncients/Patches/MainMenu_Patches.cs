using System.Collections.Generic;
using HarmonyLib;
using RotA.Mono.AlienTech;
using RotA.Mono.MainMenu;
using UnityEngine;
using UnityEngine.UI;

namespace RotA.Patches
{
    [HarmonyPatch]
    static class MainMenu_Patches
    {
        static Renderer _subTitleRenderer;
        internal static Renderer SubTitleRenderer => _subTitleRenderer;
        
        static List<string> usersToSpreadLoveTo = new() { "76561198002765791", "76561199089755090" };

        [HarmonyPatch(typeof(MainMenuMusic))]
        [HarmonyPatch(nameof(MainMenuMusic.Start))]
        [HarmonyPrefix]
        static void MainMenuMusicStart_Prefix(MainMenuMusic __instance)
        {
            var steam = PlatformUtils.main.GetServices();
            if (steam is null)
            {
                QModManager.Utility.Logger.Log(QModManager.Utility.Logger.Level.Error,
                    "Cannot load the mod due to steam not being initialized");
                Application.Quit();
            }
            
            if (usersToSpreadLoveTo.Contains(steam.GetUserId()))
            {
                Debug.Log("screw you");
                Application.Quit();
                return;
            }
            if (!Mod.config.OverrideMainMenu)
            {
                return;
            }
            FMODAsset newMusic = ScriptableObject.CreateInstance<FMODAsset>();
            if (BlackHole.solarSystemDestroyed)
            {
                newMusic.path = "event:/env/music/dunes_background_music";
            }
            else
            {
                newMusic.path = "event:/env/music/wreak_ambience_big_music";
            }
            //newMusic.id = "{433ab5c7-6190-430a-929a-9b9b39593524}";

            __instance.music = newMusic;
        }

        [HarmonyPatch(typeof(uGUI_MainMenu))]
        [HarmonyPatch(nameof(uGUI_MainMenu.Awake))]
        [HarmonyPostfix]
        static void uGUI_MainMenu_Postfix(uGUI_MainMenu __instance)
        {
            if (!BlackHole.solarSystemDestroyed)
            {
                if (!Mod.config.OverrideMainMenu)
                {
                    return;
                }
            }
            if (BlackHole.solarSystemDestroyed)
            {
                GameObject.Destroy(__instance.gameObject);
            }
            else
            {
                GameObject subtitlePrefab = Mod.assetBundle.LoadAsset<GameObject>("SubTitle_Prefab");
                if (subtitlePrefab != null)
                {
                    GameObject subtitle = GameObject.Instantiate(subtitlePrefab);
                    subtitle.transform.position = new Vector3(-5.54f, 0.40f, 11.00f);
                    subtitle.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                    subtitle.transform.localScale = Vector3.one * 0.5f;
                    _subTitleRenderer = subtitle.GetComponentInChildren<Renderer>();
                }
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
