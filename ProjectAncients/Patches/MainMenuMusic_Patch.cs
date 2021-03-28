using ECCLibrary;
using ECCLibrary.Internal;
using HarmonyLib;
using ProjectAncients.Prefabs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ProjectAncients.Mono;

namespace ProjectAncients.Patches
{
    [HarmonyPatch(typeof(MainMenuMusic))]
    public class MainMenuMusic_Patch
    {
        [HarmonyPatch(nameof(MainMenuMusic.Start))]
        [HarmonyPrefix]
        public static void Prefix(MainMenuMusic __instance)
        {
            FMODAsset wreakMusic = ScriptableObject.CreateInstance<FMODAsset>();
            wreakMusic.path = "event:/env/music/wreak_ambience_big_music";
            wreakMusic.id = "{433ab5c7-6190-430a-929a-9b9b39593524}";

            __instance.music = wreakMusic;

            Light[] lights = Object.FindObjectsOfType<Light>();
            Light highlight = lights[0];
            highlight.enabled = true;
            highlight.color = Color.magenta;
            Light sun = lights[1];
            sun.gameObject.AddComponent<MainMenuAtmosphereUpdater>();
        }
    }
}
