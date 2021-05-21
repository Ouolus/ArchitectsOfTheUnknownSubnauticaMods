using HarmonyLib;
using ProjectAncients.Prefabs;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UWE;

namespace ProjectAncients.Patches
{
    [HarmonyPatch(typeof(LaunchRocket))]
    public static class LaunchRocket_Patches
    {
        [HarmonyPatch(nameof(LaunchRocket.HideCrashedShip))]
        [HarmonyPostfix]
        public static void HideCrashedShip_Patch()
        {
            CoroutineHost.StartCoroutine(PlayGargRoarDelayed());
        }

        static IEnumerator PlayGargRoarDelayed()
        {
            yield return new WaitForSeconds(5f);
            GameObject roarObj = new GameObject("RocketRoar");
            var src = roarObj.AddComponent<AudioSource>();
            src.volume = ECCLibrary.ECCHelpers.GetECCVolume();
            src.clip = ECCLibrary.ECCAudio.LoadAudioClip("garg_roar-006");
            src.loop = false;
            src.playOnAwake = false;
            src.Play();
            GameObject.Destroy(roarObj, 19f);
        }
    }
}
