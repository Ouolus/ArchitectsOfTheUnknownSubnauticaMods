using ECCLibrary;
using ECCLibrary.Internal;
using HarmonyLib;
using ProjectAncients.Prefabs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectAncients.Patches
{
    [HarmonyPatch(typeof(MainMenuMusic))]
    public class MainMenuMusic_Patch
    {
        [HarmonyPatch(nameof(MainMenuMusic.Start))]
        [HarmonyPrefix]
        public static void Prefix(MainMenuMusic __instance)
        {
            FMODAsset bloodKelpMusic = ScriptableObject.CreateInstance<FMODAsset>();
            bloodKelpMusic.path = "event:/env/music/blood_kelp_background_music";
            //bloodKelpMusic.id = "{644c6e32-9488-46dc-b223-ae50a312432f}";
            __instance.music = bloodKelpMusic;

            Light[] lights = Object.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.name == "Directional Light")
                {
                    light.transform.forward = Vector3.up;
                }
                else
                {
                    //highlight light
                }
            }
        }
    }
}
