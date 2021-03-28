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
            FMODAsset dunesMusic = ScriptableObject.CreateInstance<FMODAsset>();
            dunesMusic.path = "event:/env/music/wreak_ambience_big_music";
            dunesMusic.id = "{433ab5c7-6190-430a-929a-9b9b39593524}";
            //bloodKelpMusic.id = "{644c6e32-9488-46dc-b223-ae50a312432f}";
            __instance.music = dunesMusic;

            Light[] lights = Object.FindObjectsOfType<Light>();
            Light highlight = lights[0];
            highlight.enabled = false;
            Light sun = lights[1];
            sun.gameObject.AddComponent<MainMenuAtmosphereUpdater>();
        }
    }
}
