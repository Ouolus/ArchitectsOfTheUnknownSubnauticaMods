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
            yield return new WaitForSeconds(29f);
            GameObject roarObj = new GameObject("RocketRoar");
            var src = roarObj.AddComponent<AudioSource>();
            src.volume = ECCLibrary.ECCHelpers.GetECCVolume();
            src.clip = ECCLibrary.ECCAudio.LoadAudioClip("garg_roar-006");
            src.loop = false;
            src.playOnAwake = false;
            src.spatialBlend = 1f;
            src.transform.position = Player.main.transform.position + new Vector3(-60f, -15f, -30f);
            src.minDistance = 20f;
            src.maxDistance = 150f;
            src.Play();
            GameObject.Destroy(roarObj, 19f);
        }
    }
}
